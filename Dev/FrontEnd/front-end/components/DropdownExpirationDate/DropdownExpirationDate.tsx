import { useState, useEffect } from "react";
import Dropdown from 'react-bootstrap/Dropdown';

const ExpirationDateDropdown = () => {
  const [selectedMonth, setSelectedMonth] = useState('');
  const [selectedYear, setSelectedYear] = useState('');

  const months = [
    '01', '02', '03', '04', '05', '06', '07', '08', '09', '10', '11', '12'
  ];

  const   
 years = [];
  const currentYear = new Date().getFullYear();
  for (let i = currentYear; i <= currentYear + 10; i++) {
    years.push(i);
  }

  const handleMonthChange = (event:any) => {
    setSelectedMonth(event.target.value);
  };

  const handleYearChange = (event:any) => {
    setSelectedYear(event.target.value);   

  };

  return (
    <div className="d-flex">   

      <Dropdown>
        <Dropdown.Toggle variant="secondary" id="dropdown-basic">
          {selectedMonth}
        </Dropdown.Toggle>
        <Dropdown.Menu>
          <Dropdown.Item onClick={handleMonthChange} value="">Month</Dropdown.Item>
          {months.map((month) => (
            <Dropdown.Item key={month} onClick={handleMonthChange} value={month}>
              {month}
            </Dropdown.Item>
          ))}
        </Dropdown.Menu>
      </Dropdown>
      <Dropdown>
        <Dropdown.Toggle variant="secondary" id="dropdown-basic">
          {selectedYear}
        </Dropdown.Toggle>
        <Dropdown.Menu>
          <Dropdown.Item onClick={handleYearChange} value="">Year</Dropdown.Item>
          {years.map((year) => (
            <Dropdown.Item key={year} onClick={handleYearChange} value={year}>
              {year}
            </Dropdown.Item>
          ))}
        </Dropdown.Menu>
      </Dropdown>
    </div>   

  );
};

export default ExpirationDateDropdown;