import NavbarStyles from "./NavbarLogo.module.css";
import Login from "@/components/Login/Login";
import Link from 'next/link';
import { Dropdown, DropdownButton, DropdownItem } from 'react-bootstrap';
import { useRouter } from "next/navigation";
import { LoginContext } from "@/app/contexts/LoginContext";
import { useContext, useRef } from "react";

const NavbarLogo = ({ showLogin, setShowLogin, isLogin, setIsLogin, isAccountClick, setIsAccountClick, searchHandler }: { showLogin: boolean, setShowLogin: any, isLogin: boolean, setIsLogin: any, isAccountClick: boolean, setIsAccountClick: any, searchHandler: any }) => {

    const inputEl = useRef("");

    const { isSeller, user } = useContext(LoginContext);
    const router = useRouter();

    const toHomePage = () => {
        router.push(`/`);
    }

    const toAccountPage = () => {
        console.log(user.id);
        router.push(`/account/${user.id}`);
    }

    const toOrdersPage = () => {
        router.push(`/orders`);
    }

    const toCartPage = () => {
        router.push(`/cart`);
    }

    const getSearchTerm = () => {
        searchHandler(inputEl.current.value);
      };

    const handleLogin = () => {
        if (showLogin) {
            console.log("Logout");
            setShowLogin(false);
            setIsLogin(false);
            setIsAccountClick(false);
            router.push(`/login`);
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
    console.log(user);
    console.log(isSeller);

    return (
        
        <nav className="navbar navbar-light bg-body-tertiary">
            <div className="container-fluid">
                <div style={{ cursor: 'pointer' }} onClick={toHomePage}>
                    <img src="/images/NotTheSilkRoadLogo.png" alt="Logo" className={NavbarStyles.logo} style={{ height: '100%', objectFit: 'contain' }} />
                </div>
                
                <div className="d-flex align-items-center">

                    {isLogin || user ? (
                        <>
                            <DropdownButton id="dropdown-basic-button" title="Account">
                                <Dropdown.Item><div onClick={toAccountPage}> Manage Account </div></Dropdown.Item>
                                <Dropdown.Item><div onClick={toOrdersPage}>Order History</div></Dropdown.Item>
                                <Dropdown.Item><div onClick={toCartPage}>Cart</div></Dropdown.Item>
                            </DropdownButton>
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

export default NavbarLogo;