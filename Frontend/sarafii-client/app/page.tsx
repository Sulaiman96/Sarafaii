import Image from "next/image";
import Link from "next/link";
import AgGrid from "./AgGrid";
import { Button } from "@radix-ui/themes";
import LedgerEntryDialog from "./components/LedgerEntryDialogComponent/LedgerEntryDialog";

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
          <LedgerEntryDialog />
        </div>
      </div>
      <AgGrid />
    </main>
  );
}
