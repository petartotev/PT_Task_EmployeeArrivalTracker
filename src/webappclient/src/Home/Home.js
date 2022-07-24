import React from 'react';
import { useEffect, useState } from "react";
import "./Home.css";
import settings from "../settings.json";

import Filter from '../Filter/Filter'
import Pagination from '../Pagination/Pagination';
import Table from '../Table/Table';

const Home = () => {
    const defaultPageArrivals = {
        currentPage: 1,
        itemsPerPage: 50,
        totalPages: 1,
        totalItems: 1,
        items: []
    };
    const [pageArrivals, setPageArrivals] = useState(defaultPageArrivals);

    const defaultFilterValues = {
        fromDate: getDateToday(),
        toDate: getDateToday(),
        order: "DESC",
        pageCount: 50
    }
    const [filterSelection, setFilterSelection] = useState(defaultFilterValues);

    useEffect(() => {
        console.log('useEffect will fetch data from server!');
        fetchFromServer(settings.webserver_url_arrivals);
    }, []);

    useEffect(() => {
        fetchDataWithSkipTake(0, filterSelection.pageCount);
    }, [filterSelection])

    function getDateToday() {
        var today = new Date();
        var dd = String(today.getDate()).padStart(2, '0');
        var mm = String(today.getMonth() + 1).padStart(2, '0');
        var yyyy = today.getFullYear();
        return yyyy + "-" + mm + "-" + dd;
    }

    function onFilterChangedFromChildFilter(filterData) {
        console.log("Filter data to be updated is: " + filterData.fromDate + " " + filterData.toDate + " " + filterData.order + " " + filterData.pageCount);
        setFilterSelection(filterData);
    }

    function onPageChangedFromChildPagination(direction) {
        if (direction === 'first') {
            fetchDataWithSkipTake(0, pageArrivals.itemsPerPage);
        }
        else if (direction === 'previous') {
            fetchDataWithSkipTake((pageArrivals.currentPage - 2) * pageArrivals.itemsPerPage, pageArrivals.itemsPerPage);
        }
        else if (direction === 'next') {
            fetchDataWithSkipTake((pageArrivals.currentPage) * pageArrivals.itemsPerPage, pageArrivals.itemsPerPage);
        }
        else if (direction === 'last') {
            fetchDataWithSkipTake((pageArrivals.totalPages - 1) * pageArrivals.itemsPerPage, pageArrivals.itemsPerPage);
        }
    }

    function fetchDataWithSkipTake(skip, take) {
        let url = settings.webserver_url_arrivals;
        url += "?fromDate=";
        let dateFrom = (filterSelection.fromDate === null || filterSelection.fromDate === undefined) ? getDateToday() : filterSelection.fromDate;
        url += dateFrom;
        url += "&toDate=";
        let dateTo = (filterSelection.toDate === null || filterSelection.toDate === undefined) ? getDateToday() : filterSelection.toDate;
        url += dateTo;
        url += "&order=";
        let order = (filterSelection.order === null || filterSelection.order === undefined) ? "DESC" : filterSelection.order;
        url += order;
        url += `&skip=${skip}&take=${take}`;

        fetchFromServer(url);
    }

    function fetchFromServer(url) {
        fetch(url)
        .then(response => {
            if (response.status === 200) {
                return response.json();
            } else {
                throw new Error('useEffect did not manage to fetch data from server!');
            }
        })
        .then(data => {
            if (data !== null) {
                console.log('useEffect has fetched the following data from server:');
                console.log(data);
                setPageArrivals(data);
            } else {
                setPageArrivals(defaultPageArrivals);
            }
            console.log(data.items);
        })
        .catch((err) => {
            console.log(err);
            setPageArrivals(defaultPageArrivals);
        })
        .finally(() => {
        });
    }

    return (
        <div>
            <div className="mt-3 mb-3"><Filter parentFunction={onFilterChangedFromChildFilter} defaultFilter={defaultFilterValues}></Filter></div>
            <div><Pagination page={pageArrivals} parentFunction={onPageChangedFromChildPagination}></Pagination></div>
            <div><Table arrivals={pageArrivals.items}></Table></div>
        </div>
    )
}

export default Home;
