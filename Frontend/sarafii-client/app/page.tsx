import Image from "next/image";
import Link from "next/link";
import AgGrid from "./AgGrid";

export default function Home() {
  return (
    <main>
      <h1>Hello World</h1>
      <Link href={"/customers"}>Customers</Link>
      <AgGrid />
    </main>
  );
}
