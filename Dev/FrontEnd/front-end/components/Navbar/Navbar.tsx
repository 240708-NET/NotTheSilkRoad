import NavbarStyles from "./Navbar.module.css";
import Login from "@/components/Login/Login";
import Link from 'next/link';
import { Dropdown, DropdownButton, DropdownItem } from 'react-bootstrap';
import { useRouter } from "next/navigation";
import { LoginContext } from "@/app/contexts/LoginContext";
import { useContext } from "react";

const Navbar = ({ showLogin, setShowLogin, isLogin, setIsLogin, isAccountClick, setIsAccountClick }: { showLogin: boolean, setShowLogin: any, isLogin: boolean, setIsLogin: any, isAccountClick: boolean, setIsAccountClick: any }) => {

    const { isSeller, user } = useContext(LoginContext);
    const router = useRouter();

    const toAccountPage = () => {
        console.log(user.id);
        router.push(`/account/${user.id}`);
    }

    const toOrdersPage = () => {
        router.push(`/orders/${user.id}`);
    }

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
    console.log(user);
    console.log(isSeller);

    return (
        
        <nav className="navbar navbar-light bg-body-tertiary">
            {user && <p>Welcome, {user.name}</p>}
            <div className="container-fluid">
                <a href="/" className="navbar-brand">
                    <img src="images/NotTheSilkRoadLogo.png" alt="Logo" className={NavbarStyles.logo} style={{ height: '100%', objectFit: 'contain' }} />
                </a>
                <form className="d-flex input-group w-auto">
                    <input
                        type="search"
                        className="form-control rounded"
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

                                

                                <Dropdown.Item><div onClick={toAccountPage}> Manage Account </div></Dropdown.Item>
                                <Dropdown.Item><Link href="/orders/7">Order History</Link></Dropdown.Item>
                                <Dropdown.Item><Link href="/cart">Cart</Link></Dropdown.Item>
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

export default Navbar;