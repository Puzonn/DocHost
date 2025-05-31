import { createRoot } from "react-dom/client";
import "./index.css";
import App from "./Pages/App.tsx";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import { Console } from "./Pages/Console.tsx";
import { Admin } from "./Pages/Admin.tsx";
import { Login } from "./Pages/Login.tsx";
import { AuthProvider } from "./Providers/AuthProvider.tsx";

createRoot(document.getElementById("root")!).render(
  <BrowserRouter>
    <AuthProvider>
      <Routes>
        <Route path="/" element={<App />} />
        <Route path="/console" element={<Console />} />
        <Route path="/admin" element={<Admin />} />
        <Route path="/login" element={<Login />} />
      </Routes>
    </AuthProvider>
  </BrowserRouter>
);
