import { useContext, useEffect, useState } from "react";
import { AuthContext } from "../Contexts/AuthContext";

export const NavBar = () => {
  const user = useContext(AuthContext);

  const [pathname, setPathname] = useState<string>("");

  useEffect(() => {
    setPathname(window.location.pathname);
  }, []);

  return (
    <div className="flex justify-center text-white gap-5 uppercase font-semibold">
      <a href="/" className="flex flex-col">
        <div
          className={`bg-red-400 w-full h-[4px] ${
            pathname === "/" ? "visible" : " invisible"
          }`}
        ></div>
        <span
          className={`text-pretty text-lg hover:text-red-400 cursor-pointer py-5 ${
            pathname === "/" ? "text-red-400" : ""
          } `}
        >
          Home
        </span>
      </a>
      <a href="servers" className="flex flex-col">
        <div
          className={`bg-red-400 w-full h-[4px] ${
            pathname === "/servers" ? "visible" : " invisible"
          }`}
        ></div>
        <span
          className={`text-pretty text-lg hover:text-red-400 cursor-pointer py-5 ${
            pathname === "/servers" ? "text-red-400" : ""
          } `}
        >
          Servers
        </span>
      </a>
      {user.isLoggedIn && (
        <a href="account" className="flex flex-col">
          <div
            className={`bg-red-400 w-full h-[4px] ${
              pathname === "/account" ? "visible" : " invisible"
            }`}
          ></div>
          <span
            className={`text-pretty text-lg hover:text-red-400 cursor-pointer py-5 ${
              pathname === "/account" ? "text-red-400" : ""
            } `}
          >
            Account
          </span>
        </a>
      )}
      <a href="admin" className="flex flex-col">
        <div
          className={`bg-red-400 w-full h-[4px] ${
            pathname === "/admin" ? "visible" : " invisible"
          }`}
        ></div>
        <span
          className={`text-pretty text-lg hover:text-red-400 cursor-pointer py-5 ${
            pathname === "/admin" ? "text-red-400" : ""
          } `}
        >
          Admin Panel
        </span>
      </a>
    </div>
  );
};
