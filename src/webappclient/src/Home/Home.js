import React from 'react';
import "./Home.css";
import { useEffect, useState } from "react";
import settings from "../settings.json";

import Filter from '../Filter/Filter'
import Pagination from '../Pagination/Pagination';
import Table from '../Table/Table';

const Home = () => {
    const [pageArrivals, setPageArrivals] = useState({
        currentPage: 1,
        itemsPerPage: 50,
        totalPages: 1,
        totalItems: 1,
        items: []
    });

    const [filterSelection, setFilterSelection] = useState({
        fromDate: getDate(),
        toDate: getDate(),
        order: "DESC",
        pageCount: 50
    });

    useEffect(() => {
        console.log('useEffect!');
        fetch(settings.webserver_url_arrivals)
            .then(response => response.json())
            .then(data => {
                setPageArrivals(data);
            })
            .catch((err) => {
                console.log(err);
            })
            .finally(() => {
            });
    }, []);

    useEffect(() => {
        fetchData(0, filterSelection.pageCount);
    }, [filterSelection])

    function getDate() {
        var today = new Date();
        var dd = String(today.getDate()).padStart(2, '0');
        var mm = String(today.getMonth() + 1).padStart(2, '0');
        var yyyy = today.getFullYear();
        return yyyy + "-" + mm + "-" + dd;
    }

    function fetchData(skip, take) {
        let url = settings.webserver_url_arrivals;
        url += "?fromDate=";
        let dateFrom = (filterSelection.fromDate === null || filterSelection.fromDate === undefined) ? getDate() : filterSelection.fromDate;
        url += dateFrom;
        url += "&toDate=";
        let dateTo = (filterSelection.toDate === null || filterSelection.toDate === undefined) ? getDate() : filterSelection.toDate;
        url += dateTo;
        url += "&order=";
        let order = (filterSelection.order === null || filterSelection.order === undefined) ? "DESC" : filterSelection.order;
        url += order;
        url += `&skip=${skip}&take=${take}`;

        fetch(url)
            .then(response => response.json())
            .then(data => {
                setPageArrivals(data);
                console.log(data.items);
            })
            .catch((err) => {
                console.log(err);
            })
            .finally(() => {
            });
    }

    function onPageChangedFromChild(direction) {
        if (direction === 'first') {
            fetchData(0, pageArrivals.itemsPerPage);
        }
        else if (direction === 'previous') {
            fetchData((pageArrivals.currentPage - 2) * pageArrivals.itemsPerPage, pageArrivals.itemsPerPage);
        }
        else if (direction === 'next') {
            fetchData((pageArrivals.currentPage) * pageArrivals.itemsPerPage, pageArrivals.itemsPerPage);
        }
        else if (direction === 'last') {
            fetchData((pageArrivals.totalPages - 1) * pageArrivals.itemsPerPage, pageArrivals.itemsPerPage);
        }
    }

    function onFilterChangedFromChild(filterData) {
        console.log("Filter data to be updated is: " + filterData.fromDate + " " + filterData.toDate + " " + filterData.order + " " + filterData.pageCount);
        setFilterSelection(filterData);
    }

    return (
        <div className="">
            <div className="mt-3 mb-3"><Filter parentFunction={onFilterChangedFromChild}></Filter></div>
            <div><Pagination page={pageArrivals} parentFunction={onPageChangedFromChild}></Pagination></div>
            <div><Table arrivals={pageArrivals.items}></Table></div>
        </div>
    )
}

export default Home;
