"use client";
import React, { useRef, useState } from "react";
import Select from "react-select";
import * as Dialog from "@radix-ui/react-dialog";
import { Cross2Icon } from "@radix-ui/react-icons";
import { PlusIcon } from "@radix-ui/react-icons";
import "react-datepicker/dist/react-datepicker.css";
import "./LedgerEntryDialog.css";
import { Button } from "@radix-ui/themes";
import DatePicker from "react-datepicker";

const LedgerEntryDialog = () => {
  const [selectedDate, setSelectedDate] = useState(new Date());
  const [currency, setCurrency] = useState("");
  const [fromCustomer, setFromCustomer] = useState("");
  const [toCustomer, setToCustomer] = useState("");
  const [isCollected, setIsCollected] = useState(false);

  const handleDialogOpen = (isOpen: boolean) => {
    if (isOpen) {
      setSelectedDate(new Date());
      setCurrency("");
      setFromCustomer("");
      setToCustomer("");
      setIsCollected(false);
    }
  };

  const customSelectStyles = {
    control: (provided) => ({
      ...provided,
      flex: 1,
      width: "100%",
      borderRadius: "4px",
      padding: "0 10px",
      fontSize: "15px",
      lineHeight: "1",
      color: "var(--violet-11)",
      boxShadow: "0 0 0 1px var(--violet-7)",
      height: "35px",
      display: "inline-flex",
      alignItems: "center",
      justifyContent: "center",
      borderColor: "#d1d5db",
    }),
    menu: (provided) => ({
      ...provided,
      width: "100%",
    }),
    singleValue: (provided) => ({
      ...provided,
      color: "var(--violet-11)",
    }),
    input: (provided) => ({
      ...provided,
      margin: 0,
      padding: 0,
    }),
    placeholder: (provided) => ({
      ...provided,
      color: "var(--violet-7)",
    }),
  };

  const customers = [
    { value: "Customer A", label: "Customer A" },
    { value: "Customer B", label: "Customer B" },
    { value: "Customer C", label: "Customer C" },
    { value: "Customer D", label: "Customer D" },
    { value: "Customer E", label: "Customer E" },
    { value: "Customer F", label: "Customer F" },
    { value: "Customer G", label: "Customer G" },
    { value: "Customer H", label: "Customer H" },
  ];

  const currencies = [
    { value: "USD", label: "USD" },
    { value: "AFG", label: "AFG" },
    { value: "EUR", label: "EUR" },
    { value: "GBP", label: "GBP" },
    { value: "JPY", label: "JPY" },
    { value: "CNY", label: "CNY" },
  ];

  const filteredToCustomers = customers.filter(
    (customer) => customer.value !== fromCustomer?.value
  );

  const filteredFromCustomers = customers.filter(
    (customer) => customer.value !== toCustomer?.value
  );

  return (
    <Dialog.Root onOpenChange={handleDialogOpen}>
      <Dialog.Trigger asChild>
        <Button className="IconButton">
          <PlusIcon />
        </Button>
      </Dialog.Trigger>

      <Dialog.Portal>
        <Dialog.Overlay className="DialogOverlay" />

        <Dialog.Content className="DialogContent">
          <Dialog.Title className="DialogTitle"> Add Ledger Entry</Dialog.Title>
          <Dialog.Description className="DialogDescription">
            Adds an entry to the Ledger
          </Dialog.Description>

          <fieldset className="Fieldset">
            <label className="Label" htmlFor="amount">
              Amount
            </label>
            <input className="Input" id="amount" defaultValue={0} />
          </fieldset>

          <fieldset className="Fieldset">
            <label className="Label" htmlFor="date">
              Date
            </label>
            <DatePicker
              selected={selectedDate}
              onChange={(date) => setSelectedDate(date)}
              dateFormat="yyyy/MM/dd"
              className="Input"
              id="date"
              placeholderText={new Date().toLocaleDateString("en-CA")}
              showMonthDropdown
              showYearDropdown
              dropdownMode="select"
            />
          </fieldset>

          <fieldset className="Fieldset">
            <label className="Label" htmlFor="currency">
              Currency
            </label>
            <Select
              styles={customSelectStyles}
              id="currency"
              options={currencies}
              value={currency}
              onChange={setCurrency}
              className="react-select-container"
              classNamePrefix={"react-select"}
            />
          </fieldset>

          <fieldset className="Fieldset">
            <label className="Label" htmlFor="fromCustomer">
              From Customer
            </label>
            <Select
              styles={customSelectStyles}
              id="fromCustomer"
              options={filteredFromCustomers}
              value={fromCustomer}
              onChange={setFromCustomer}
              className="react-select-container"
              classNamePrefix={"react-select"}
            />
          </fieldset>

          <fieldset className="Fieldset">
            <label className="Label" htmlFor="toCustomer">
              To Customer
            </label>
            <Select
              styles={customSelectStyles}
              id="toCustomer"
              options={filteredToCustomers}
              value={toCustomer}
              onChange={setToCustomer}
              isDisabled={!fromCustomer}
              className="react-select-container"
              classNamePrefix={"react-select"}
            />
          </fieldset>

          <fieldset className="Fieldset checkbox-fieldset">
            <label className="Label checkbox-label" htmlFor="collected">
              Has Been Collected
            </label>
            <input
              type="checkbox"
              className="Checkbox"
              id="collected"
              checked={isCollected}
              onChange={(e) => setIsCollected(e.target.checked)}
            />
          </fieldset>

          <div
            style={{
              display: "flex",
              marginTop: 25,
              justifyContent: "flex-end",
            }}
          >
            <Dialog.Close asChild>
              <button className="Button green">Save changes</button>
            </Dialog.Close>
          </div>

          <Dialog.Close asChild>
            <button className="IconButtonX" aria-label="Close">
              <Cross2Icon />
            </button>
          </Dialog.Close>
        </Dialog.Content>
      </Dialog.Portal>
    </Dialog.Root>
  );
};
export default LedgerEntryDialog;
