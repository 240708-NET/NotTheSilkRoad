import AccountMenuStyles from "./AccountMenuStyles.module.css";
//import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import Link from 'next/link';
import { useRouter } from "next/router";
import Cart from "../Cart/Cart";
function AccountMenu() {
    //const router = useRouter();

    const createRouter = () => {
        window.location.href = "/cart";
    }

    return (
        <div className={AccountMenuStyles.accountMenu}>
            <Link href = "/cart">Cart</Link>
            <p>Manage Account</p>
            <p>Orders</p>
        </div>
    )
};

export default AccountMenu;