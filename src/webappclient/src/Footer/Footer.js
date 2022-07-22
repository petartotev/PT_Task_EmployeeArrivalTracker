import React from 'react';
import './Footer.css';
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

const Footer = () => {
    return (
        <footer className="footer mt-auto bg-dark text-white">
            <div className="row justify-content-evenly">
                <div className="col-md-2">
                    <div className="row justify-content-evenly">
                        <div className="col-md-2 pt-2 d-flex justify-content-center">
                            <p><a href="https://www.linkedin.com/in/petartotev" className="link-light" target="_blank" rel="noopener noreferrer"><FontAwesomeIcon icon="fab fa-linkedin" size="2x" /></a></p>
                        </div>
                        <div className="col-md-2 pt-2 d-flex justify-content-center">
                            <p><a href="https://www.github.com/petartotev" className="link-light" target="_blank" rel="noopener noreferrer"><FontAwesomeIcon icon="fab fa-github" size="2x" /></a></p>
                        </div>
                        <div className="col-md-2 pt-2 d-flex justify-content-center">
                            <p><a href="https://www.gitlab.com/users/petartotev" className="link-light" target="_blank" rel="noopener noreferrer"><FontAwesomeIcon icon="fab fa-gitlab" size="2x" /></a></p>
                        </div>
                    </div>
                </div>
            </div>
        </footer>
    );
}

export default Footer;