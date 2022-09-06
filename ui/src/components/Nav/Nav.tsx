import React from 'react';
import { Link } from 'react-router-dom';
import "./Nav.css";

const Nav = () => {
    return (
        <div className="nav-bar">
            <Link to="/about" className="link">About</Link>
            <Link to="/" className="link">Browse</Link>
            <Link to="/user" className="link">User</Link>
        </div>
    );
}

export default Nav;