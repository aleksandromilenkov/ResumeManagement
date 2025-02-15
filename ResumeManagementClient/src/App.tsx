import React, {lazy, Suspense} from "react";
import { useTheme } from "./context/themeContext";
import Navbar from "./components/Navbar/Navbar";
import { Navigate, Route, Routes } from "react-router-dom";
import Spinner from "./utils/Spinner";

// Imports with lazy loading
const Home = lazy(()=> import("./pages/home/HomePage"));

function App() {
  const { darkMode } = useTheme();
  const appStyles = darkMode ? "app dark" : "app";
  return (
    <div className={appStyles}>
      <Navbar />
      <div className="wrapper">
        <Suspense fallback={<Spinner/>}>
          <Routes>
            <Route index element={<Navigate replace to="home" />} />
            <Route path="/home" element={<Home/>}/>
          </Routes>
        </Suspense>
      </div>
    </div>
  );
}

export default App;
