import React from 'react';
import "./Filter.css";
import { useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

const Filter = (props) => {
    const [filterForm, setFilterForm] = useState({
        fromDate: getDate(),
        toDate: getDate(),
        order: "DESC"
    });

    function getDate() {
        var today = new Date();
        var dd = String(today.getDate()).padStart(2, '0');
        var mm = String(today.getMonth() + 1).padStart(2, '0');
        var yyyy = today.getFullYear();
        return yyyy + "-" + mm + "-" + dd;
    }

    const onChange = (e) => {
        setFilterForm({ ...filterForm, [e.target.id]: e.target.value });
    };

    const onSubmit = (e) => {
        e.preventDefault();
        console.log("Submit! Here is the object to submit:");
        props.parentFunction(filterForm);
    };

    return (
        <div>
            <div className="d-flex justify-content-center mb-3">
                <button className="btn rounded-pill btn-bg btn-primary ms-1 me-1" type="button" data-bs-toggle="collapse" data-bs-target="#navbarToggleExternalContent" aria-controls="navbarToggleExternalContent" aria-expanded="false" aria-label="Toggle navigation">
                    &nbsp;<FontAwesomeIcon icon="search" size="2x" />&nbsp;
                </button>
            </div>
            <div className="collapse bg-dark" id="navbarToggleExternalContent">
                <div className="container-fluid bg-secondary d-flex justify-content-center">
                    <form className="mt-2">
                        <div className="form-group mt-3 mb-3">
                            <label htmlFor="fromDate">From&nbsp;</label>
                            <input type="date" value={filterForm.fromDate} onChange={onChange} id="fromDate" name="fromDate" className="rounded-pill ps-2 pe-2" />
                            <label htmlFor="toDate">&nbsp;To&nbsp;</label>
                            <input type="date" value={filterForm.toDate} onChange={onChange} id="toDate" name="toDate" className="rounded-pill ps-2 pe-2" />
                            <label htmlFor="order">&nbsp;Order&nbsp;</label>
                            <select value={filterForm.order} onChange={onChange} id="order" name="order" className="rounded-pill ps-2 pe-2">
                                <option>DESC</option>
                                <option>ASC</option>
                            </select>
                        </div>
                        <div className="d-flex justify-content-center mb-3">
                            <button className="btn btn-bg btn-primary rounded-pill" disabled={!filterForm.fromDate || !filterForm.toDate || !filterForm.order} onClick={onSubmit}>
                                &nbsp;<FontAwesomeIcon icon="rocket" size="2x" />&nbsp;
                            </button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    )
}

export default Filter;