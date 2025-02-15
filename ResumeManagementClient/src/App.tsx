import React from "react";
import { useTheme } from "./context/themeContext";
import Navbar from "./components/Navbar/Navbar";
import { Navigate, Route, Routes } from "react-router-dom";
import Home from "./pages/home/HomePage";
function App() {
  const { darkMode } = useTheme();
  const appStyles = darkMode ? "app dark" : "app";
  return (
    <div className={appStyles}>
      <Navbar />
      <div className="wrapper">
        <Routes>
          <Route index element={<Navigate replace to="home" />} />
          <Route path="/home" element={<Home/>}/>
        </Routes>
      </div>
    </div>
  );
}

export default App;
