import { useEffect, useState } from "react";
import type { Command } from "./Types/Types";

export const Console = () => {
  const [inputs, setInputs] = useState<Command[]>([]);

  const handleSubmitCommand = (command: Command) => {
    setInputs([...inputs, command]);
    fetch(
      `http://localhost:5252/api/status/send-input?command=${command.Content}`,
      { method: "POST" }
    ).then((e) => {
      return fetch("http://localhost:5252/api/status/get-console");
    }).then((res)=>{res.text()}).then((logs))

    ;
  };

  useEffect(() => {
    fetch("http://localhost:5252/api/status/get-console").then((e) => {
      e.text().then((r) => {
        const command: Command = { Content: r, IsServer: true };
        handleSubmitCommand(command);
      });
    });
  }, []);

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
    </>
  );
};
