"use client";

import React from "react";
import { useState, useEffect } from "react";
import { AgGridReact } from "ag-grid-react";
import "ag-grid-community/styles/ag-grid.css";
import "ag-grid-community/styles/ag-theme-alpine.css";

const AgGrid = () => {
  const [columnDefs] = useState([
    { headerName: "Amount", field: "amount" },
    { headerName: "Date", field: "date" },
    { headerName: "Currency", field: "currency" },
    { headerName: "Rate", field: "rate" },
    { headerName: "From Customer", field: "fromCustomer.fullName" },
    { headerName: "To Customer", field: "toCustomer.fullName" },
    { headerName: "Has Been Collected", field: "hasBeenCollected" },
  ]);

  const [rowData, setRowData] = useState([]);

  useEffect(() => {
    fetch("http://localhost:5020/api/Ledger")
      .then((response) => response.json())
      .then((data) => setRowData(data));
  }, []);

  return (
    <div className="ag-theme-alpine" style={{ height: "600px" }}>
      <AgGridReact
        id="staff_grid"
        rowData={rowData}
        columnDefs={columnDefs}
        style={{ height: "120%", width: "100%" }}
      ></AgGridReact>
    </div>
  );
};

export default AgGrid;
