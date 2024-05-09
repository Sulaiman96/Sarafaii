"use client";
import React from "react";
import * as Tooltip from "@radix-ui/react-tooltip";
import { PlusIcon } from "@radix-ui/react-icons";
import { Button } from "@radix-ui/themes";

const AddButton = () => {
  return (
    <Button>
      <PlusIcon></PlusIcon>
    </Button>
  );
};

export default AddButton;
