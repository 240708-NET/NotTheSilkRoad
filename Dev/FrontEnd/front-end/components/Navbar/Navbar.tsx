import NavbarStyles from "./Navbar.module.css";
import Login from "@/components/Login/Login";
function Navbar({showLogin, setShowLogin, isLogin, setIsLogin} : {showLogin: boolean, setShowLogin: any, isLogin: boolean, setIsLogin: any})
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

    return(
        <div className={NavbarStyles.navbar}>
            <h1>NotTheSilkRoad</h1>
            
            <div className={NavbarStyles.search}>
                <input type="text" placeholder="Search"></input>
                <button>Search</button>
            </div>
            
            <button onClick={handleLogin}>{isLogin ? "Logout" : "Login"}</button>
        </div>
    )
};

export default Navbar;