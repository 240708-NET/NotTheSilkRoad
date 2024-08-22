import AccountMenuStyles from "./AccountMenuStyles.module.css";
//import { BrowserRouter, Routes, Route, NavLink } from 'react-router-dom';
import Link from 'next/link';
import { useRouter } from "next/router";
import Cart from "../../app/cart/page";

function AccountMenu() {
    return (
        <div className="dropdown">
  <button className="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
    Account Menu
  </button>
  <div className="dropdown-menu" aria-labelledby="dropdownMenuButton">
    <a className="dropdown-item" href="#">Action</a>
    <a className="dropdown-item" href="#">Another action</a>
    <a className="dropdown-item" href="#">Something else here</a>
  </div>
</div>

    )
};


export default AccountMenu;