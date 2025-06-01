import { useEffect, useState } from "react";
import type { User } from "../Types/Types";

export const UserList = () => {
  const [users, setUsers] = useState<User[]>([]);

  useEffect(() => {
    fetch("http://localhost:5252/api/user/get-all", {
      credentials: "include",
    }).then((r) => {
      r.json().then((e) => {
        setUsers(e);
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
            <th className="px-4 py-2 text-left text-sm font-semibold">Id</th>
            <th className="px-4 py-2 text-left text-sm font-semibold">
              Username
            </th>
            <th className="px-4 py-2 text-left text-sm font-semibold">Role</th>
            <th className="px-4 py-2 text-left text-sm font-semibold">
              Created
            </th>
            <th className="px-4 py-2 text-left text-sm font-semibold"></th>
          </tr>
        </thead>
        <tbody className="divide-y divide-gray-800">
          {users.map((user) => (
            <tr key={user.userId} className="hover:bg-gray-800">
              <td className="px-4 py-2 text-sm hover:underline cursor-pointer">
                {user.userId}
              </td>
              <td
                onClick={(e) =>
                  navigator.clipboard.writeText(e.currentTarget.textContent!)
                }
                className="px-4 py-2 text-sm cursor-pointer hover:underline"
              >
                {user.username}
              </td>
              <td className="px-4 py-2 text-sm text-blue-400">{user.role}</td>
              <td className="px-4 py-2 text-sm text-gray-400">
                {new Date(user.createdAt!).toLocaleString()}
              </td>
              <td className="px-4 py-2 text-sm">
                <button
                  //   onClick={(e) => handleActionsClick(e, server)}
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
    </div>
  );
};
