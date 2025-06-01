import { createRoot } from "react-dom/client";
import "./index.css";
import App from "./Pages/App.tsx";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import { Console } from "./Pages/Console.tsx";
import { Admin } from "./Pages/Admin.tsx";
import { Login } from "./Pages/Login.tsx";
import { AuthProvider } from "./Providers/AuthProvider.tsx";
import type { ReactNode } from "react";
import { NavBar } from "./Components/NavBar.tsx";

const RenderWithLayout = (children: ReactNode) => {
  return (
    <div>
      <NavBar></NavBar>
      {children}
    </div>
  );
};

createRoot(document.getElementById("root")!).render(
  <BrowserRouter>
    <AuthProvider>
      <Routes>
        <Route path="/" element={RenderWithLayout(<App />)} />
        <Route path="/console" element={RenderWithLayout(<Console />)} />
        <Route path="/admin" element={RenderWithLayout(<Admin />)} />
        <Route path="/login" element={RenderWithLayout(<Login />)} />
      </Routes>
    </AuthProvider>
  </BrowserRouter>
);
