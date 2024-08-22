import AccountPageStyles from "../AccountMenu/AccountMenu";
import NavbarStyles from "./Navbar.module.css";
import Login from "@/components/Login/Login";
import AccountMenu from "@/components/AccountMenu/AccountMenu";
import Link from 'next/link';
import { Dropdown, DropdownButton, DropdownItem } from 'react-bootstrap';
const Navbar = ({ showLogin, setShowLogin, isLogin, setIsLogin, isAccountClick, setIsAccountClick }: { showLogin: boolean, setShowLogin: any, isLogin: boolean, setIsLogin: any, isAccountClick: boolean, setIsAccountClick: any }) => {
    const handleLogin = () => {
        if (showLogin) {
            console.log("Logout");
            setShowLogin(false);
            setIsLogin(false);
            setIsAccountClick(false);
        }
        else {
            console.log("Login");
            setShowLogin(true);
            setIsLogin(true);
        }
    };

    const handleLogout = () => {
        window.location.href = '/';
    };

    const handleAccountButton = () => {
        if (isAccountClick) {
            setIsAccountClick(false);
            //setShowLogin(true);
        }
        else {
            setIsAccountClick(true);
            //setShowLogin(false);
        }
    }

    return (
        <nav className="navbar navbar-light bg-body-tertiary">
            <div className="container-fluid">
                <a href="/" className="navbar-brand">
                    <img src="images/NotTheSilkRoadLogo.png" alt="Logo" className={NavbarStyles.logo} style={{ height: '100%', objectFit: 'contain' }} />
                </a>
                <form className="d-flex input-group w-auto">
                    <input
                        type="search"
                        className="form-control   
             rounded"
                        placeholder="Search"
                        aria-label="Search"
                        aria-describedby="search-addon"
                    />
                    <span className="input-group-text border-0" id="search-addon">

                        <i className="fas fa-search"></i>
                    </span>
                </form>
                <div className="d-flex align-items-center">

                    {isLogin ? (
                        <>
                            <DropdownButton id="dropdown-basic-button" title="Account">
                                <Dropdown.Item><Link href="/account">Manage Account</Link></Dropdown.Item>
                                <Dropdown.Item><Link href="#">Order History</Link></Dropdown.Item>
                                <Dropdown.Item><Link href="/cart">Cart</Link></Dropdown.Item>
                            </DropdownButton>;
                            <button onClick={handleLogout} className="btn btn-primary">
                                Logout
                            </button>
                        </>
                    ) : (
                        <button onClick={handleLogin} className="btn btn-primary">
                            Login
                        </button>
                    )}
                </div>
            </div>
        </nav>
    );
};

export default Navbar;