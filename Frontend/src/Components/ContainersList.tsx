import { useEffect, useState } from "react";
import type { ContainerStatus } from "../Types/Types";

export const ContainerList = () => {
  const [containers, setContainers] = useState<ContainerStatus[]>([]);

  useEffect(() => {
    fetch("http://localhost:5252/api/status").then((e) => {
      e.json().then((r) => {
        setContainers(r);
      });
    });
  }, []);

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
          </tr>
        </thead>
        <tbody className="divide-y divide-gray-800">
          {containers.map((container) => (
            <tr key={container.id} className="hover:bg-gray-800">
              <td className="px-4 py-2 text-sm">{container.name}</td>
              <td
                onClick={(e) => {
                  navigator.clipboard.writeText(e.currentTarget.textContent!);
                }}
                className="px-4 py-2 text-sm"
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
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};
