import AccountPageStyles from "../../app/AccountMenu/page";
import NavbarStyles from "./Navbar.module.css";
import Login from "@/components/Login/Login";
import AccountMenu from "@/app/AccountMenu/page";
import Link from 'next/link';
function Navbar({showLogin, setShowLogin, isLogin, setIsLogin, isAccountClick, setIsAccountClick} : {showLogin: boolean, setShowLogin: any, isLogin: boolean, setIsLogin: any, isAccountClick: boolean, setIsAccountClick: any})
{
    const handleLogin = () => {
        if(showLogin)
        {
            console.log("Logout");
            setShowLogin(false);
            setIsLogin(false);
            setIsAccountClick(false);
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


    return (

<div className={NavbarStyles.navbar}>
            <h1>NotTheSilkRoad</h1>
            
            <div className={NavbarStyles.search}>

                <input type="text" placeholder="Search"></input>
                <button>Search</button>
            
            </div>

          {isLogin ? (
            <nav>
              <Link href="/AccountMenu">Account</Link>
              <button onClick={handleLogin}>Logout</button>
            </nav>
          ) : (
            <button onClick={handleLogin}>Login</button>
          )}

        </div>
      );
    }

export default Navbar;