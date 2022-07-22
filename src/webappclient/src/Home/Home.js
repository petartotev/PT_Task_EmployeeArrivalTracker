import React from 'react';
import "./Home.css";
import { useEffect, useState } from "react";
import settings from "../settings.json";

import Table from '../Table/Table';
import Pagination from '../Pagination/Pagination';

const Home = () => {
    const [pageArrivals, setPageArrivals] = useState({
        currentPage: 1,
        itemsPerPage: 1,
        totalPages: 1,
        totalItems: 1,
        items: [],
    });

    useEffect(() => {
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

    function fetchData(skip, take) {
        let url = settings.webserver_url_arrivals;
        url += `?skip=${skip}&take=${take}`
        fetch(url)
            .then(response => response.json())
            .then(data => {
                setPageArrivals(data);
            })
    }

    function pageChangedFromChild(direction) {
        console.log(direction);
        if (direction === 'first') {
            console.log('first');
            fetchData(0, pageArrivals.itemsPerPage);
        }
        else if (direction === 'previous') {
            console.log('previous');
            fetchData((pageArrivals.currentPage - 2) * pageArrivals.itemsPerPage, pageArrivals.itemsPerPage);
        }
        else if (direction === 'next') {
            console.log('next');
            fetchData((pageArrivals.currentPage) * pageArrivals.itemsPerPage, pageArrivals.itemsPerPage);
        }
        else if (direction === 'last') {
            console.log('last');
            fetchData((pageArrivals.totalPages - 1) * pageArrivals.itemsPerPage, pageArrivals.itemsPerPage);
        }
    }

    return (
        <div className="m-3">
            <div>
                <Pagination page={pageArrivals} parentFunction={pageChangedFromChild}></Pagination>
            </div>
            <div>
                <Table arrivals={pageArrivals.items}></Table>
            </div>
        </div>
    )
}

export default Home;
