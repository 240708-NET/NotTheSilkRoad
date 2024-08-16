import LoginStyles from "./Login.module.css";
function Login()
{
    return(
        <div className={LoginStyles.login}>
            <input type="text" placeholder="Email"></input>
            <input type="password" placeholder="Password"></input>
        </div>
    )
};

export default Login;