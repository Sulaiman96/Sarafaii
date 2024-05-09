import Image from "next/image";
import Link from "next/link";
import AgGrid from "./AgGrid";
import { Button } from "@radix-ui/themes";
import AddButton from "./components/AddButtonComponent/AddButton";

export default function Home() {
  return (
    <main>
      <div className="flex justify-between">
        <div>
          <Button>
            <Link href={"customers/new-customer"}>New Customer</Link>
          </Button>
        </div>
        <div className="">
          <AddButton />
        </div>
      </div>
      <AgGrid />
    </main>
  );
}
