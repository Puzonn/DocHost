import { useEffect, useRef, useState } from "react";
import type { ContainerStatus } from "../Types/Types";

export const ContainerList = () => {
  const [containers, setContainers] = useState<ContainerStatus[]>([]);
  const [menuInfo, setMenuInfo] = useState<{
    containerId: string;
    top: number;
    left: number;
  } | null>(null);
  const menuRef = useRef<HTMLDivElement | null>(null);

  useEffect(() => {
    fetch("http://localhost:5252/api/status")
      .then((e) => e.json())
      .then(setContainers);
  }, []);

  useEffect(() => {
    const handleClickOutside = (e: MouseEvent) => {
      if (menuRef.current && !menuRef.current.contains(e.target as Node)) {
        setMenuInfo(null);
      }
    };
    document.addEventListener("mousedown", handleClickOutside);
    return () => document.removeEventListener("mousedown", handleClickOutside);
  }, []);

  const handleActionsClick = (
    e: React.MouseEvent<HTMLButtonElement>,
    containerId: string
  ) => {
    const rect = e.currentTarget.getBoundingClientRect();
    setMenuInfo((prev) =>
      prev?.containerId === containerId
        ? null
        : {
            containerId,
            top: rect.bottom + window.scrollY + 10,
            left: rect.left + window.scrollX,
          }
    );
  };

  return (
    <div
      className="overflow-x-auto rounded-xl shadow border border-gray-500 flex justify-center items-center h-full w-screen"
      style={{ backgroundColor: "#3d3b3b" }}
    >
      <table className="min-w-full divide-y divide-gray-700 bg-gray-950 text-gray-100">
        <thead className="bg-gray-800 text-gray-300">
          <tr>
            <th className="px-4 py-2 text-left text-sm font-semibold">Name</th>
            <th className="px-4 py-2 text-left text-sm font-semibold">
              Container ID
            </th>
            <th className="px-4 py-2 text-left text-sm font-semibold">
              Status
            </th>
            <th className="px-4 py-2 text-left text-sm font-semibold">State</th>
            <th className="px-4 py-2 text-left text-sm font-semibold">
              Created
            </th>
            <th className="px-4 py-2 text-left text-sm font-semibold">Ports</th>
            <th className="px-4 py-2 text-left text-sm font-semibold"></th>
          </tr>
        </thead>
        <tbody className="divide-y divide-gray-800">
          {containers.map((container) => (
            <tr key={container.id} className="hover:bg-gray-800">
              <td className="px-4 py-2 text-sm">{container.name}</td>
              <td
                onClick={(e) =>
                  navigator.clipboard.writeText(e.currentTarget.textContent!)
                }
                className="px-4 py-2 text-sm cursor-pointer hover:underline"
              >
                {container.id}
              </td>
              <td className="px-4 py-2 text-sm text-blue-400">
                {container.status}
              </td>
              <td className="px-4 py-2 text-sm">{container.state}</td>
              <td className="px-4 py-2 text-sm text-gray-400">
                {new Date(container.createdAt).toLocaleString()}
              </td>
              <td className="px-4 py-2 text-sm">
                {container.ports.map((port, idx) => (
                  <div key={idx}>
                    {port.ip} {port.privatePort} {port.publicPort} {port.type}
                  </div>
                ))}
              </td>
              <td className="px-4 py-2 text-sm">
                <button
                  onClick={(e) => handleActionsClick(e, container.id)}
                  style={{ backgroundColor: "#3d3b3b" }}
                  className="p-1 px-2 rounded-md font-semibold hover:scale-[103%] cursor-pointer"
                >
                  Actions
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      {menuInfo && (
        <div
          ref={menuRef}
          className="fixed w-40 rounded-md shadow-lg z-50"
          style={{
            top: menuInfo.top,
            left: menuInfo.left,
            backgroundColor: "#3d3b3b",
          }}
        >
          <ul className="py-1 text-sm">
            <li
              onClick={() => {
                console.log("Delete container:", menuInfo.containerId);
                setMenuInfo(null);
              }}
              className="px-4 py-2cursor-pointer text-red-600"
            >
              Start
            </li>
            <li
              onClick={() => {
                console.log("Delete container:", menuInfo.containerId);
                setMenuInfo(null);
              }}
              className="px-4 py-2cursor-pointer text-red-600"
            >
              Delete
            </li>
          </ul>
        </div>
      )}
    </div>
  );
};
