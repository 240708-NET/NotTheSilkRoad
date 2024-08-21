import AccountMenuStyles from "./AccountMenuStyles.module.css";
//import { BrowserRouter, Routes, Route, NavLink } from 'react-router-dom';
import Link from 'next/link';
import { useRouter } from "next/router";
import Cart from "../../app/cart/page";

function AccountMenu() {
    return (
        <div className={AccountMenuStyles.accountMenu}>
                <nav>
                    <Link href="/cart">Cart</Link>
                </nav>
                <p>Manage Account</p>
                <p>Orders</p>
        </div>
    )
};

export default AccountMenu;