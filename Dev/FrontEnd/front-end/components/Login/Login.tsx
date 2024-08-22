import LoginStyles from "./Login.module.css";
import 'bootstrap/dist/css/bootstrap.min.css';
import { Container, Row, Col } from 'react-bootstrap';
import RegistrationForm from "../RegistrationForm/RegistrationForm";

const Login = ({ showLogin, setShowLogin, isLogin, setIsLogin, isAccountClick, setIsAccountClick, registrationClick, setRegistrationClick }: { showLogin: boolean, setShowLogin: any, isLogin: boolean, setIsLogin: any, isAccountClick: boolean, setIsAccountClick: any, registrationClick: boolean, setRegistrationClick: any }) => {

    const handleLogin = () => {
        if (showLogin) {
            console.log("Logout");
            setShowLogin(false);
            setIsLogin(true);
            setIsAccountClick(false);
        } else {
            console.log("Login");
            setShowLogin(true);
            setIsLogin(true);
        }
    };

    const handleRegistrationClick = () => {
        setRegistrationClick(true);
    };

    return (
        <>
            {!registrationClick ? (
                <div className={LoginStyles.login}>
                    <a href="/" className="navbar-brand">
                        <img src="images/NotTheSilkRoadLogo.png" alt="Logo" className={LoginStyles.logo} style={{ height: '100%', objectFit: 'contain' }} />
                    </a>
                    <Container className="card card-registration py-4 px-4">
                        <h3 className="mb-4 text-uppercase">Login</h3>

                        <form>
                            <div className="form-outline mb-4">
                                <input
                                    type="email"
                                    id="form2Example17"
                                    className="form-control form-control-lg"
                                    placeholder="Email address"
                                />
                            </div>

                            <div className="form-outline mb-4">
                                <input
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

                            <a className="small text-muted" href="#!">
                                Forgot password?
                            </a>

                            <p className="mb-5 pb-lg-2">
                                Don't have an account? <button onClick={handleRegistrationClick} className="btn btn-link">
                                    Register here
                                </button>
                            </p>

                            <a href="#!" className="small text-muted">
                                Terms of use.
                            </a>
                            <a href="#!" className="small text-muted">
                                Privacy policy
                            </a>
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