"use client";

import Link from "next/link";
import React from "react";
import BrandLogo from "./BrandLogo";
import { usePathname } from "next/navigation";
import classNames from "classnames";

const navbar = () => {
  const currentPath = usePathname();

  const links = [
    { label: "Dashboard", href: "/" },
    { label: "Customers", href: "/customers" },
  ];

  const dropDownLinks = [
    { label: "Profile", href: "/profile" },
    { label: "Settings", href: "/settings" },
    { label: "Logout", href: "/" },
  ];
  return (
    <nav className="flex items-center justify-between flex-wrap bg-primary pr-0.5 pl-1 mb-5">
      <Link href={"/"} className="flex items-center flex-shrink-0">
        <BrandLogo color={"white"} />
        <span className="font-semibold text-xl tracking-tight text-base-100">
          Sarafaii
        </span>
      </Link>
      <ul className="flex space-x-6">
        {links.map((link) => (
          <Link
            key={link.href}
            className={classNames({
              "text-accent ": link.href === currentPath,
              "text-base-100": link.href !== currentPath,
              "text-base-100 hover:text-accent transition-colors": true,
            })}
            href={link.href}
          >
            {link.label}
          </Link>
        ))}
      </ul>

      <div className="flex-none gap-2">
        <div className="dropdown dropdown-end">
          <div
            tabIndex={0}
            role="button"
            className="btn btn-ghost btn-circle avatar"
          >
            <div className="w-10 rounded-full">
              <img
                alt="Tailwind CSS Navbar component"
                src="https://media.licdn.com/dms/image/D4E03AQF4ZB6Y2RQ3sg/profile-displayphoto-shrink_400_400/0/1699983286956?e=1720051200&v=beta&t=50KKVXdp7HymQ5z6zPZrQ37oYdQovgvhOyIE3KmxVUo"
              />
            </div>
          </div>
          <ul
            tabIndex={0}
            className="mt-3 z-[1] p-2 shadow menu menu-sm dropdown-content bg-primary rounded-box w-52 text-base-100"
          >
            {dropDownLinks.map((link) => (
              <li key={link.href} className="flex justify-between">
                <Link
                  className="text-base-100 hover:text-accent transition-colors"
                  href={link.href}
                >
                  {link.label}
                </Link>
              </li>
            ))}
          </ul>
        </div>
      </div>
    </nav>
  );
};

export default navbar;
