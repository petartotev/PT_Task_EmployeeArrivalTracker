import React from 'react';
import './Table.css';

const Table = ({ arrivals }) => {
    function getDayOfWeekAbbreviationFromNumber(number) {
        let abbr;
        switch (number) {
            case 0:
                abbr = 'Sun';
                break;
            case 1:
                abbr = 'Mon';
                break;
            case 2:
                abbr = 'Fri';
                break;
            case 3:
                abbr = 'Wed';
                break;
            case 4:
                abbr = 'Thu';
                break;
            case 5:
                abbr = 'Fri';
                break;
            case 6:
                abbr = 'Sat';
                break;
            default:
                abbr = 'N/A';
        }
        return abbr;
    }

    function getAgeFromDateBirth(dateString) {
        let today = new Date();
        let birthDate = new Date(dateString);
        let age = today.getFullYear() - birthDate.getFullYear();
        let month = today.getMonth() - birthDate.getMonth();
        if (month < 0 || (month === 0 && today.getDate() < birthDate.getDate())) {
            age--;
        }
        return age;
    }

    return (
        <div>
            <table className="table table-secondary table-striped">
                <thead>
                    <tr>
                        <th scope="col">Date</th>
                        <th scope="col">Day</th>
                        <th scope="col">Time</th>
                        <th scope="col">Name</th>
                        <th scope="col">Email</th>
                        <th scope="col">Age</th>
                        <th scope="col">Role</th>
                    </tr>
                </thead>
                <tbody>
                    {arrivals.map(arrival => (
                        <tr key={arrival.id}>
                            <td><b>{new Date(arrival.dateArrival).getFullYear()}-{(new Date(arrival.dateArrival).getMonth() + 1).toString().padStart(2, '0')}-{new Date(arrival.dateArrival).getDate()}</b></td>
                            <td><b className="text-primary">{getDayOfWeekAbbreviationFromNumber(new Date(arrival.dateArrival).getDay())}</b></td>
                            <td><b>{(new Date(arrival.dateArrival).getHours()).toString().padStart(2, '0')}:{(new Date(arrival.dateArrival).getMinutes()).toString().padStart(2, '0')}:{(new Date(arrival.dateArrival).getSeconds()).toString().padStart(2, '0')}</b></td>
                            <td>{arrival.employee.firstName} {arrival.employee.lastName}</td>
                            <td>{arrival.employee.email}</td>
                            <td>{getAgeFromDateBirth(arrival.employee.dateBirth)}</td>
                            <td>{arrival.employee.role.name}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}

export default Table;