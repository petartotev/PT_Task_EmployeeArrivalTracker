import React from 'react';
import './App.css';
import {BrowserRouter as Router, Routes, Route } from "react-router-dom";

import Home from "../Home/Home";
import Header from "../Header/Header";
import Footer from "../Footer/Footer";

function App() {
  return (
    <Router>
      <Header title="Title from App's Router!" subtitle="Subtitle passed as props from App's Router!" />
      <div className="container">
        {/* <Filter allThings={allThings} /> */}
        <Routes>
          <Route exact path="/" element={<Home propboo="BooFoo" />} />
        </Routes>
        <Footer title="Title by App's Router!" subtitle="Subtitle passed as props by App's Router!" />
      </div>
    </Router>
  );
}

export default App;
