import { useEffect, useState } from "react";
import type { Command } from "./Types/Types";
import * as signalR from "@microsoft/signalr";
import { useSearchParams } from "react-router-dom";

export const Console = () => {
  const [searchParams] = useSearchParams();
  const containerId = searchParams.get("containerid");
  const [inputs, setInputs] = useState<Command[]>([]);
  const connection = new signalR.HubConnectionBuilder()
    .withUrl(`http://localhost:5252/hubs/console?containerId=${containerId}`)
    .build();

  useEffect(() => {
    connection.on("ReceiveLog", (e) => {
      const command: Command = {
        IsServer: true,
        Content: (e as string)
          .replace(/[\x00-\x1F\x7F-\x9F]/g, "")
          .substring(1),
      }; /* Remove wierd ascii shit */
      handleSubmitCommand(command);
    });
    connection.start();
  }, []);

  const handleSubmitCommand = (command: Command) => {
    setInputs((prevInputs) => [...prevInputs, command]);
  };

  useEffect(() => {
    const textConsole = document.getElementById("console-output");
    if (textConsole) {
      textConsole.scrollTop = textConsole.scrollHeight;
    }
  }, [inputs]);

  return (
    <>
      <div className="p-2">
        <div
          id="console-output"
          className="w-full max-h-[50vh] border-2 border-red-600 bg-black overflow-y-scroll overflow-x-hidden whitespace-pre-line"
        >
          {inputs.map((command, index) => {
            return (
              <p
                key={index}
                className={` pl-2 ${
                  command.IsServer ? "text-red-900" : "text-white"
                } `}
              >
                {command.Content}
              </p>
            );
          })}
        </div>
        <div className="relative">
          <input
            onChange={(e) => {
              if (e.target.value[0] !== ">") {
                e.target.value = ">" + e.target.value;
              }
            }}
            onKeyDown={(e) => {
              if (e.key == "Enter") {
                const command: Command = {
                  Content: e.currentTarget.value.substring(1),
                  IsServer: false,
                };

                handleSubmitCommand(command);

                fetch(
                  `http://localhost:5252/api/status/send-input?command=${command.Content}&id=${containerId}`,
                  { method: "POST" }
                );

                e.currentTarget.value = "";
              }
            }}
            type="text"
            placeholder=">"
            className="outline-none placeholder:text-emerald-800 placeholder:opacity-20 bg-gray-900 w-full text-green-800 border-2
             border-gray-500 pl-2"
          />
        </div>
      </div>
      <div className="p-2">
        <div className="w-full h-90 bg-gray-800 border-2 border-white flex text-gray-200">
          <div className="w-[15%] h-full border-r-2 border-white flex flex-col gap-5 justify-top text-2xl p-4 overflow-y-scroll">
            <span>server.proporties</span>
            <span>whitelist</span>
          </div>
          <div className="w-full h-full border-r-2 border-white flex flex-col justify-top p-4 overflow-y-scroll"></div>
        </div>
      </div>
    </>
  );
};
