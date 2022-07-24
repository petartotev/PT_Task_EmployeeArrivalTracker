import React from 'react';
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import './App.css';

import Home from "../Home/Home";
import Header from "../Header/Header";
import Footer from "../Footer/Footer";

const App = () => {
  return (
    <Router>
      <Header title="Arrival Tracker!" subtitle="CCTV your employees!" />
      <div className="container">
        <Routes>
          <Route exact path="/" element={<Home propboo="BooFoo" />} />
        </Routes>
        <Footer />
      </div>
    </Router>
  );
}

export default App;
