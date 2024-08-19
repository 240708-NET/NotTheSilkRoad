import AccountPageStyles from "../AccountMenu/AccountMenu";
import NavbarStyles from "./Navbar.module.css";
import Login from "@/components/Login/Login";
import AccountMenu from "@/components/AccountMenu/AccountMenu";
function Navbar({showLogin, setShowLogin, isLogin, setIsLogin, isAccountClick, setIsAccountClick} : {showLogin: boolean, setShowLogin: any, isLogin: boolean, setIsLogin: any, isAccountClick: boolean, setIsAccountClick: any})
{
    const handleLogin = () => {
        if(showLogin)
        {
            console.log("Logout");
            setShowLogin(false);
            setIsLogin(false);
        }
        else
        {
            console.log("Login");
            setShowLogin(true);
            setIsLogin(true);
        }
    };

    const handleAccountButton = () => {
        if(isAccountClick)
        {
            setIsAccountClick(false);
        }
        else
        {
            setIsAccountClick(true);
        }
    }

    return(
        <div className={NavbarStyles.navbar}>
            <h1>NotTheSilkRoad</h1>
            
            <div className={NavbarStyles.search}>
                <input type="text" placeholder="Search"></input>
                <button>Search</button>
            </div>
            
            <button onClick={handleAccountButton}>{isLogin ? "Account" : ""}</button>
            <button onClick={handleLogin}>{isLogin ? "Logout" : "Login"}</button>
        </div>
    )
};

export default Navbar;