import React from "react";
import "./navbar.scss";
import { Link } from "react-router-dom";
import { Menu, LightMode, DarkMode } from "@mui/icons-material";
import { ToggleButton } from "@mui/material";
import { useTheme } from "../../context/themeContext";
type Props = {};
const links = [
  {
    href: "/",
    label: "Home",
  },
  {
    href: "/companies",
    label: "Companies",
  },
  {
    href: "/jobs",
    label: "Jobs",
  },
  {
    href: "/candidates",
    label: "Candidates",
  },
];
const Navbar = (props: Props) => {
  const {darkMode, toggleDarkMode} = useTheme();
  return (
    <div className="navbar">
      <div className="brand">
        <span>Resume Management</span>
      </div>
      <div className="menu">
        <ul>
          {links.map((item, idx) => (
            <li key={idx}>
              <Link to={item.href}>{item.label}</Link>
            </li>
          ))}
        </ul>
      </div>
      <div className="hamburger">
          <Menu />
      </div>
      <div className="toggle">
        <ToggleButton
          value="check"
          selected={darkMode}
          onChange={toggleDarkMode}
          style={{ color: darkMode ? "white" : "black" }}
        >
          {darkMode ? <LightMode/> : <DarkMode/>}
        </ToggleButton>
      </div>
    </div>
  );
};

export default Navbar;
