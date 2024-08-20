import AccountPageStyles from "../../app/AccountMenu/page";
import NavbarStyles from "./Navbar.module.css";
import Login from "@/components/Login/Login";
import AccountMenu from "@/app/AccountMenu/page";
import Link from 'next/link';
const Navbar = ({showLogin, setShowLogin, isLogin, setIsLogin, isAccountClick, setIsAccountClick} : {showLogin: boolean, setShowLogin: any, isLogin: boolean, setIsLogin: any, isAccountClick: boolean, setIsAccountClick: any}) =>
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
            setShowLogin(true);
        }
        else
        {
            setIsAccountClick(true);
            setShowLogin(false);
        }
    }

    return (
        <nav className="navbar navbar-light bg-body-tertiary">
          <div className="container-fluid">
            <a className="navbar-brand">Navbar</a>
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
                  <button onClick={handleAccountButton} className="btn btn-primary">
                    Account
                  </button>
                  <button onClick={handleLogin} className="btn btn-primary">
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
    
    
    

/*
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
      */

export default Navbar;