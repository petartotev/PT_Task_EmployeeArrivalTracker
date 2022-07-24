import React from 'react';
import './Pagination.css';
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

const Pagination = (props) => {
    function isButtonFirstOnEdge() {
        return props.page.currentPage <= 1;
    }

    function isButtonPreviousOnEdge() {
        return props.page.totalPages <= 1 || props.page.currentPage <= 1;
    }

    function isButtonNextOnEdge() {
        return props.page.totalPages <= 1 || props.page.currentPage + 1 > props.page.totalPages;
    }

    function isButtonLastOnEdge() {
        return props.page.currentPage === props.page.totalPages || props.page.totalPages === 0;
    }

    function buttonFirstClicked() {
        props.parentFunction('first');
    }

    function buttonPreviousClicked() {
        props.parentFunction('previous');
    }

    function buttonNextClicked() {
        props.parentFunction('next');
    }

    function buttonLastClicked() {
        props.parentFunction('last');
    }

    return (
        <div className="row m-3">
            <div className="col-md-4"></div>
            <div className="col-md-4 d-flex justify-content-evenly">
                <button className="btn btn-dark text-light rounded-pill" onClick={buttonFirstClicked} disabled={isButtonFirstOnEdge()}><FontAwesomeIcon icon="fa-solid fa-angles-left" size="lg" /></button>
                <button className="btn btn-dark text-light rounded-pill" onClick={buttonPreviousClicked} disabled={isButtonPreviousOnEdge()}><FontAwesomeIcon icon="fa-solid fa-chevron-left" size="lg" /></button>
                <p className="h2">{props.page.currentPage}/{props.page.totalPages}</p>
                <button className="btn btn-dark text-light rounded-pill" onClick={buttonNextClicked} disabled={isButtonNextOnEdge()}><FontAwesomeIcon icon="fa-solid fa-chevron-right" size="lg" /></button>
                <button className="btn btn-dark text-light rounded-pill" onClick={buttonLastClicked} disabled={isButtonLastOnEdge()}><FontAwesomeIcon icon="fa-solid fa-angles-right" size="lg" /></button>
            </div>
        </div>
    );
}

export default Pagination;