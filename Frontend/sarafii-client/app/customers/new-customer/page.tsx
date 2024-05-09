import { TextField } from "@radix-ui/themes";
import React from "react";

const NewCustomer = () => {
  return (
    <div className="max-w-xl">
      <TextField.Root placeholder="Search the docs…">
        <TextField.Slot></TextField.Slot>
      </TextField.Root>
    </div>
  );
};

export default NewCustomer;
