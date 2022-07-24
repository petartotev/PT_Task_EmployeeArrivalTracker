import React from 'react';
import "./Header.css";
import image from './busstop_image_white.png';

const Header = (props) => {
  return (
    <div>
      <nav className="navbar navbar-expand-lg navbar-dark bg-dark text-white">
        <div className="container-fluid">
          <a className="navbar-brand" href="/"><img src={image} className="anim-logo" alt="logo"></img></a>
          <a className="navbar-brand" href="/">
            <p className="display-6">{props.title}*</p>
            <p className="h6 text-primary">*{props.subtitle}</p>
          </a>
          <div className="collapse navbar-collapse" id="navbarSupportedContent">
          </div>
        </div>
      </nav>
    </div>
  );
}

export default Header;