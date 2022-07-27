import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import reportWebVitals from './reportWebVitals';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.js';
import { library } from '@fortawesome/fontawesome-svg-core';
import { fab, faFacebook, faLinkedin, faGithub, faGitlab } from '@fortawesome/free-brands-svg-icons';
import { faEnvelope, faRocket, faBell, faBurger, faSearch, faUser, faPaperPlane, faGlobe, faSort, faChevronLeft, faChevronRight, faAnglesLeft, faAnglesRight, faArrowUp } from '@fortawesome/free-solid-svg-icons';

import App from './App/App';

library.add(faFacebook, faLinkedin, faGithub, faGitlab, fab);
library.add(faEnvelope, faRocket, faBell, faBurger, faSearch, faUser, faPaperPlane, faGlobe, faSort, faChevronLeft, faChevronRight, faAnglesLeft, faAnglesRight, faArrowUp);

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
