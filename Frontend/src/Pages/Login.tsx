import { type FormEvent } from "react";

export const Login = () => {
  const handleLoginSubmit = (e: FormEvent) => {
    e.preventDefault();

    const { username, password } = e.target as any;

    fetch("http://localhost:5252/api/auth/login", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      credentials: "include",
      body: JSON.stringify({
        username: username.value,
        password: password.value,
      }),
    }).then((e) => {
      if (e.ok) {
        window.location.href = "/";
      }
    });
  };

  return (
    <div className="text-white flex flex-col items-center">
      <form onSubmit={handleLoginSubmit} className="flex flex-col gap-4">
        <input
          name="username"
          type="text"
          placeholder="Username"
          style={{ backgroundColor: "#212121" }}
          className="px-5 outline-none p-2 rounded-md"
        />
        <input
          name="password"
          type="password"
          placeholder="Password"
          style={{ backgroundColor: "#212121" }}
          className="px-5 outline-none p-2 rounded-md"
        />

        <button
          type="submit"
          style={{ backgroundColor: "#d50c2d" }}
          className="p-2 rounded-md px-4 cursor-pointer hover:scale-[103%] font-bold flex justify-center items-center"
        >
          <span className="flex items-center justify-center h-[30px] w-[30px] ">
            Login
          </span>
        </button>
      </form>
    </div>
  );
};
