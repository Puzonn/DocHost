import { useEffect, useState, type FormEvent } from "react";
import type { ContainerOption } from "../Types/Types";

export const ContainerCreator = () => {
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [containerOptions, setContainerOptions] = useState<ContainerOption[]>(
    []
  );

  useEffect(() => {
    fetch("http://localhost:5252/api/container/options").then((r) => {
      r.json().then((e) => {
        setContainerOptions(e);
      });
    });
  }, []);

  const handleCreationSubmit = (e: FormEvent) => {
    e.preventDefault();

    if (isLoading) {
      return;
    }

    const { name, server_port, type, ftp_port } = e.target as any;

    setIsLoading(true);

    fetch("http://localhost:5252/api/container/create", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        name: name.value,
        serverPort: server_port.value,
        type: type.value,
        ftpport: ftp_port.value,
      }),
    })
      .then(() => {
        setIsLoading(false);
      })
      .catch(() => {
        setIsLoading(false);
      });
  };

  return (
    <div className="flex flex-col items-center ">
      <form onSubmit={handleCreationSubmit}>
        <div
          className="flex flex-col gap-4 justify-center w-fit p-5 text-white"
          style={{ backgroundColor: "#262626" }}
        >
          <div
            className="flex p-2 px-4 rounded-md gap-8"
            style={{ backgroundColor: "#212121" }}
          >
            <span>Type: </span>
            <select name="type">
              {containerOptions.map((container, index) => {
                return (
                  <option
                    className="text-black"
                    value={container.containerName}
                    key={`container_option_${index}`}
                  >
                    {container.containerName}
                  </option>
                );
              })}
            </select>
          </div>

          <input
            name="name"
            type="text"
            placeholder="Container Name"
            style={{ backgroundColor: "#212121" }}
            className="px-5 outline-none p-2 rounded-md"
          />
          <input
            name="server_port"
            type="number"
            placeholder="Server Port"
            style={{ backgroundColor: "#212121" }}
            className="px-5 outline-none p-2 rounded-md"
          />
          <input
            name="ftp_port"
            type="number"
            placeholder="Ftp Port"
            style={{ backgroundColor: "#212121" }}
            className="px-5 outline-none p-2 rounded-md"
          />
          <button
            type="submit"
            style={{ backgroundColor: "#d50c2d" }}
            className="p-2 rounded-md px-4 cursor-pointer hover:scale-[103%] font-bold flex justify-center items-center"
          >
            {isLoading ? (
              <div className="flex items-center justify-center">
                <div className="h-[30px] w-[30px] border-4 border-white border-t-transparent rounded-full animate-spin"></div>
              </div>
            ) : (
              <span className="flex items-center justify-center h-[30px] w-[30px] ">
                Create
              </span>
            )}
          </button>
        </div>
      </form>
    </div>
  );
};
