import React from 'react';
import './Pagination.css';
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

const Pagination = (props) => {
    function isEdgeConditionFirstButton() {
        return props.page.currentPage <= 1;
    }

    function isEdgeConditionPreviousButton() {
        return props.page.totalPages <= 1 || props.page.currentPage <= 1;
    }

    function isEdgeConditionNextButton() {
        return props.page.totalPages <= 1 || props.page.currentPage + 1 >= props.page.totalPages;
    }

    function isEdgeConditionLastButton() {
        return props.page.currentPage === props.page.totalPages;
    }

    function buttonFirstClicked() {
        console.log("Button clicked");
        props.parentFunction('first');
    }

    function buttonPreviousClicked() {
        console.log("Button clicked");
        props.parentFunction('previous');
    }

    function buttonNextClicked() {
        console.log("Button clicked");
        props.parentFunction('next');
    }

    function buttonLastClicked() {
        console.log("Button clicked");
        props.parentFunction('last');
    }

    return (
        <div className="row justify-content-center m-3">
            <div className="col-md-4 d-flex justify-content-evenly">
                <button className="btn btn-dark text-light rounded-pill" onClick={buttonFirstClicked} disabled={isEdgeConditionFirstButton()}><FontAwesomeIcon icon="fa-solid fa-angles-left" size="lg" /></button>
                <button className="btn btn-dark text-light rounded-pill" onClick={buttonPreviousClicked} disabled={isEdgeConditionPreviousButton()}><FontAwesomeIcon icon="fa-solid fa-chevron-left" size="lg" /></button>
                <p className="h2">{props.page.currentPage}/{props.page.totalPages}</p>
                <button className="btn btn-dark text-light rounded-pill" onClick={buttonNextClicked} disabled={isEdgeConditionNextButton()}><FontAwesomeIcon icon="fa-solid fa-chevron-right" size="lg" /></button>
                <button className="btn btn-dark text-light rounded-pill" onClick={buttonLastClicked} disabled={isEdgeConditionLastButton()}><FontAwesomeIcon icon="fa-solid fa-angles-right" size="lg" /></button>
            </div>
        </div>
    );
}

export default Pagination;