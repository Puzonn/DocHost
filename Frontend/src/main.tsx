import { createRoot } from "react-dom/client";
import "./index.css";
import App from "./App.tsx";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import { Console } from "./Console.tsx";
import { Admin } from "./Admin.tsx";

createRoot(document.getElementById("root")!).render(
  <BrowserRouter>
    <Routes>
      <Route path="/" element={<App />} />
      <Route path="/console" element={<Console />} />
      <Route path="/admin" element={<Admin />} />
    </Routes>
  </BrowserRouter>
);
