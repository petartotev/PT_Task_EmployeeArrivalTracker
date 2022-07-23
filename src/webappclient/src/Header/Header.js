import React from 'react';
import "./Header.css";
import image from './busstop_image_white.png';

const Header = () => {
    return (
    <div>
        <nav className="navbar navbar-expand-lg navbar-dark bg-dark text-white">
          <div className="container-fluid">
            <a className="navbar-brand" href="/"><img src={image} className="anim-logo" alt="logo"></img></a>
            <a className="navbar-brand" href="/"><p className="display-6">Employee Arrival Tracker!</p></a>
            <div className="collapse navbar-collapse" id="navbarSupportedContent">
              <ul className="navbar-nav me-auto mb-2 mb-lg-0">
              </ul>
            </div>
          </div>
        </nav>
    </div>
    );
}

export default Header;