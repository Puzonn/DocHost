import { useEffect, useState } from "react";

function App() {
  useEffect(() => {
    fetch("http://localhost:5252/api/status/get-all").then((e) => {
      e.json().then((e) => {});
    });
  }, []);

  return (
    <div className="flex justify-center items-center h-full w-screen">
    </div>
  );
}

export default App;
