import React, { useState } from "react";
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
  const [menuIsOpen, setMenuIsOpen] = useState(false);
  const closeMenu = () => setMenuIsOpen(prev=>!prev);
  return (
    <div className="navbar">
      <div className="brand">
        <span>Resume Management</span>
      </div>
      <div className={menuIsOpen ? "menu" : "menu closed"}>
        <ul>
          {links.map((item, idx) => (
            <li key={idx}>
              <Link to={item.href} onClick={closeMenu}>{item.label}</Link>
            </li>
          ))}
        </ul>
      </div>
      <div className="hamburger">
          <Menu  onClick={closeMenu}/>
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
