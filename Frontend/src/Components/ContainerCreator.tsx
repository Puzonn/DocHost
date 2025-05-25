import { useEffect, useState, type FormEvent } from "react";
import type { ContainerOption } from "../Types/Types";

export const ContainerCreator = () => {
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
    const { name, port, type } = e.target as any;

    fetch(
      `http://localhost:5252/api/container/create?name=${name.value}&port=${port.value}&type=${type.value}`,
      {
        method: "POST",
      }
    );
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
            name="port"
            type="number"
            placeholder="Container Port"
            style={{ backgroundColor: "#212121" }}
            className="px-5 outline-none p-2 rounded-md"
          />
          <button
            type="submit"
            style={{ backgroundColor: "#d50c2d" }}
            className="p-2 rounded-md px-4 cursor-pointer hover:scale-[103%] font-bold"
          >
            Create
          </button>
        </div>
      </form>
    </div>
  );
};
