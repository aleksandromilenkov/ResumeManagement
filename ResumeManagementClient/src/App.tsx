import React from "react";
import { useTheme } from "./context/themeContext";
import Navbar from "./components/Navbar/Navbar";
function App() {
  const { darkMode } = useTheme();
  const appStyles = darkMode ? "app dark" : "app";
  return (
    <div className={appStyles}>
      <Navbar />
      <div className="wrapper">Routes</div>
    </div>
  );
}

export default App;
