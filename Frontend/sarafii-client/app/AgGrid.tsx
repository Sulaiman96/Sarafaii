"use client";

import React, { useMemo } from "react";
import { useState, useEffect } from "react";
import { AgGridReact } from "ag-grid-react";
import "ag-grid-community/styles/ag-grid.css";
import "ag-grid-community/styles/ag-theme-alpine.css";

const AgGrid = () => {
  const [columnDefs] = useState([
    {
      field: "amount",
      valueFormatter: (p: { value: number }) =>
        "£" + p.value.toFixed(2).toString(),
    },
    {
      field: "date",
      valueFormatter: (p: { value: string }) => p.value.split("T")[0],
    },
    {
      field: "currency",
    },
    {
      field: "rate",
      valueFormatter: (p: { value: number }) => p.value.toFixed(5),
    },
    { headerName: "From Customer", field: "fromCustomer.fullName" },
    { headerName: "To Customer", field: "toCustomer.fullName" },
    { headerName: "Has Been Collected", field: "hasBeenCollected" },
  ]);

  const defaultColDef = useMemo(() => {
    return {
      flex: 1,
    };
  }, []);

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
        defaultColDef={defaultColDef}
      ></AgGridReact>
    </div>
  );
};

export default AgGrid;
