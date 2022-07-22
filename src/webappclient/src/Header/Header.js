import React from 'react';
import "./Header.css";
import image from './busstop_image_white.png';
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

const Header = () => {
    const GetDate = () => {
        var today = new Date();
        var dd = String(today.getDate()).padStart(2, '0');
        var mm = String(today.getMonth() + 1).padStart(2, '0');
        var yyyy = today.getFullYear();
        return yyyy + "-" + mm + "-" + dd;
    }

    return (
    <div>
        <nav className="navbar navbar-expand-lg navbar-dark bg-dark text-white">
          <div className="container-fluid">
            <a className="navbar-brand" href="/"><img src={image} className="anim-logo" alt="logo"></img></a>
            <a className="navbar-brand" href="/"><p className="display-6">Employee Arrival Tracker!</p></a>
            <div className="collapse navbar-collapse" id="navbarSupportedContent">
              <ul className="navbar-nav me-auto mb-2 mb-lg-0">
              </ul>
              <div className="d-flex">
                <button className="btn rounded-pill btn-bg btn-dark ms-1 me-1" type="button" data-bs-toggle="collapse" data-bs-target="#navbarToggleExternalContent" aria-controls="navbarToggleExternalContent" aria-expanded="false" aria-label="Toggle navigation">
                    <FontAwesomeIcon icon="search" size="2x" />
                </button>
              </div>
            </div>
          </div>
        </nav>
        <div className="collapse bg-dark" id="navbarToggleExternalContent">
            <div className="container-fluid bg-secondary">
                <div className="container">
                <div className="row">
                        <div className="col-md-12 d-flex justify-content-center">
                            <div className="pt-3 pb-3 ps-1 pe-1">
                                from <input type="date" id="birthday" name="birthday" defaultValue={GetDate()} className="rounded-pill ps-2 pe-2" />
                            </div>
                            <div className="pt-3 pb-3 ps-1 pe-1">
                                to <input type="date" id="birthday" name="birthday" defaultValue={GetDate()} className="rounded-pill ps-2 pe-2" />
                            </div>
                            <div className="pt-3 pb-3 ps-1 pe-1">
                                order by <p className="dropdown-toggle btn btn-sm rounded-pill btn-light" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                                    <FontAwesomeIcon icon="sort" size="lg" />&nbsp;
                                </p>
                                <ul className="dropdown-menu" aria-labelledby="navbarDropdown">
                                  <li><p className="dropdown-item" href="#">ASC</p></li>
                                  <li><p className="dropdown-item" href="#">DESC</p></li>
                                </ul>
                            </div>
                            <div className="pt-3 pb-3 ps-1 pe-1">
                                <p className="dropdown-toggle btn btn-sm rounded-pill btn-light" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">Category</p>
                                <ul className="dropdown-menu" aria-labelledby="navbarDropdown">
                                  <li><p className="dropdown-item">Birds</p></li>
                                  <li><p className="dropdown-item">Networking</p></li>
                                  <li><p className="dropdown-item">Raspberry Pi</p></li>
                                  <li><p className="dropdown-item">Software</p></li>
                                  <li><p className="dropdown-item">Hiking</p></li>
                                </ul>
                            </div>
                            <div className="pt-3 pb-3 ps-1 pe-1">
                                <p className="dropdown-toggle btn btn-sm rounded-pill btn-light" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">Author</p>
                                <ul className="dropdown-menu" aria-labelledby="navbarDropdown">
                                  <li><p className="dropdown-item">Petar Totev</p></li>
                                  <li><p className="dropdown-item">Ivaylo Ivanov</p></li>
                                  <li><p className="dropdown-item">Joli Cenkova</p></li>
                                </ul>
                            </div>
                            <div className="pt-3 pb-3 ps-1 pe-1">
                                <p className="dropdown-toggle btn btn-sm rounded-pill btn-light" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                                    <FontAwesomeIcon icon="globe" size="lg" />
                                </p>
                                <ul className="dropdown-menu" aria-labelledby="navbarDropdown">
                                  <li><p className="dropdown-item">English</p></li>
                                  <li><p className="dropdown-item">Bulgarian</p></li>
                                </ul>
                            </div>
                            <div className="pt-3 pb-3 ps-1 pe-1">
                                <button className="btn rounded-pill btn-secondary" type="submit"><FontAwesomeIcon icon="paper-plane" size="lg" /></button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    );
}

export default Header;