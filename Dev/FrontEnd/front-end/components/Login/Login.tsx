import LoginStyles from "./Login.module.css";
import 'bootstrap/dist/css/bootstrap.min.css';
import { Container, Row, Col } from 'react-bootstrap';
import RegistrationForm from "../RegistrationForm/RegistrationForm";
import { LoginContext } from "@/app/contexts/LoginContext";
import { CartContext } from "@/app/contexts/CartContext";
import { useState, useContext } from "react";
import router from "next/router";
import Link from "next/link";

const Login = ({ showLogin, setShowLogin, isLogin, setIsLogin, isAccountClick, setIsAccountClick, registrationClick, setRegistrationClick }: { showLogin: boolean, setShowLogin: any, isLogin: boolean, setIsLogin: any, isAccountClick: boolean, setIsAccountClick: any, registrationClick: boolean, setRegistrationClick: any }) => {

    const { user, setUser, isSeller, setIsSeller } : { user: any, setUser: any, isSeller: boolean, setIsSeller: any } = useContext(LoginContext);
    const {cart, setCart, cartId, setCartId} = useContext(CartContext)
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    const handleLogin = async () => {
        if (showLogin) {
            console.log("Logout");
            setShowLogin(false);
            setIsLogin(true);

                console.log(email);
                const response = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/auth`, {
                    method: "POST",
                    body: JSON.stringify({ email, password }),
                    headers: {
                        "Content-Type": "application/json",
                      },
                })

                if (response.ok) {
                    const user = await response.json();
                    setUser(user);

                    //Only customers have an address, so this determines if the signing in user is a seller or not
                    if(user.address == null){
                        setIsSeller(true);
                    }
                    else
                    {
                        setIsSeller(false);
                    }
                    console.log(user);
                        const orderresponse = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/order`)
                  
                        const data = await orderresponse.json();
                        
                        const filteredData = data.filter(x => x.customer.id === user.id && x.active === true);
                        console.log(filteredData)
                       
                  
                        if(filteredData.length === 0){

                          const newresponse = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/order`, {
                            method: 'POST',
                            body: JSON.stringify({
                              customer: user,
                              items: [],
                              date: formatDate(),
                              active: true,
                              shippingAddress: user.address
                            }),
                            headers: {
                                "Content-Type": "application/json",
                              },
                          })
                
                          if(newresponse.ok){
                            const data = await newresponse.json();
                            console.log(data.items.length)
                            setCart(data.items)
                            setCartId(data.id)
                            
                          }
                
                        } else {
                          console.log(filteredData[0].items)
                          setCart(filteredData[0].items)
                          setCartId(filteredData[0].id)
                        }              
                }
                else {
                    console.log("Error");
                    setShowLogin(true);
                    setIsLogin(false);
                    alert("Invalid email or password. Please try again.");
                }
        }
        else {
            console.log("Login");
            setShowLogin(true);
            setIsLogin(true);
        }
    };

    const formatDate = () => {
        let month = ''
        let day = ''
        if(new Date(Date.now()).getMonth() < 10){
            month = '0'+new Date(Date.now()).getMonth()
        } else {
            month = String(new Date(Date.now()).getMonth())
        }
        if(new Date(Date.now()).getDate() < 10){
            day = '0'+new Date(Date.now()).getDate()
        } else {
            day = String(new Date(Date.now()).getDate())
        }

        return `${new Date(Date.now()).getFullYear()}-${month}-${day}`
    }

    const handleRegistrationClick = () => {
        setRegistrationClick(true);
    };

    return (
        <>
            {!registrationClick ? (
                <div className={LoginStyles.login}>
                    <a href="/" className="navbar-brand">
                        <img src="/images/NotTheSilkRoadLogo.png" alt="Logo" className={LoginStyles.logo} style={{ height: '100%', objectFit: 'contain' }} />
                    </a>
                    <Container className="card card-registration py-4 px-4">
                        <h3 className="mb-4 text-uppercase">Login</h3>

                        <form>
                            <div className="form-outline mb-4">
                                <input
                                    onChange={(e) => setEmail(e.target.value)}
                                    type="email"
                                    id="form2Example17"
                                    className="form-control form-control-lg"
                                    placeholder="Email address"
                                />
                            </div>

                            <div className="form-outline mb-4">
                                <input
                                    onChange={(e) => setPassword(e.target.value)}
                                    type="password"
                                    id="form2Example27"
                                    className="form-control form-control-lg"
                                    placeholder="Password"
                                />
                            </div>

                            <div className="pt-1 mb-4">
                                <button className="btn btn-dark btn-lg btn-block" type="submit" onClick={handleLogin}>
                                    {showLogin ? "Login" : "Logout"}
                                </button>
                            </div>

                            {/* <a className="small text-muted" href="#!">
                                Forgot password?
                            </a> */}

                            <p className="mb-5 pb-lg-2">
                                Don't have an account? <button onClick={handleRegistrationClick} className="btn btn-link">
                                    Register here
                                </button>
                            </p>

                            <div>
                                <a href="/" className="fas fa-long-arrow-alt-left me-2">
                                    <img src="images/arrow-90deg-left.svg" alt="Arrow Back" />
                                    Go Back
                                </a>
                            </div>

                            {/* <a href="#!" className="small text-muted">
                                Terms of use.
                            </a>
                            <a href="#!" className="small text-muted">
                                Privacy policy
                            </a> */}
                        </form>
                    </Container>
                </div>
            ) : (
                <RegistrationForm />
            )}
        </>
    );
};

export default Login;