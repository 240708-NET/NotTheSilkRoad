"use client"
import { createContext, useState } from "react";

const LoginContext = createContext({
    isSeller : false,
    setIsSeller : () => {},

    user : null,
    setUser : () => {},
});

const LoginProvider = ({ children }: any) => {
    const [user, setUser] = useState(null);
    const [isSeller, setIsSeller] = useState(false);

    return (
        <LoginContext.Provider value={{user, setUser, isSeller, setIsSeller}}>
            {children}
        </LoginContext.Provider>
    )
}

export { LoginContext, LoginProvider }