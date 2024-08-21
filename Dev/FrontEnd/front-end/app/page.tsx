"use client";
import { useState, useEffect } from "react";
import Navbar from "@/components/Navbar/Navbar";
import styles from "./page.module.css";
import Login from "@/components/Login/Login";
import Listing from "@/components/Listing/Listing"
import { products } from "@/components/Listing/listings.js"
import AccountMenu from "@/components/AccountMenu/AccountMenu";
import RegistrationForm from "@/components/RegistrationForm/RegistrationForm";

export default function Home() {
  const [isLogin, setIsLogin] = useState(false);
  const [showLogin, setShowLogin] = useState(false);
  const [isAccountClick, setIsAccountClick] = useState(false);
  const [registrationClick, setRegistrationClick] = useState(false);

  return (
    <main className={styles.main}>

      {/* <RegistrationForm /> */}

      {!showLogin ? (
        <>

          <Navbar showLogin={showLogin} setShowLogin={setShowLogin} isLogin={isLogin} setIsLogin={setIsLogin}
            isAccountClick={isAccountClick} setIsAccountClick={setIsAccountClick} />

          <div className={styles.listing}>
            {products.map((item, key) => {
              return (
                <Listing key={key} title={item.title} image={item.image} price={item.price} />
              )
            })}
          </div>

          {isAccountClick && <AccountMenu />}

        </>

      ) : (

        <Login showLogin={showLogin} setShowLogin={setShowLogin} isLogin={isLogin} setIsLogin={setIsLogin}
          isAccountClick={isAccountClick} setIsAccountClick={setIsAccountClick} registrationClick={registrationClick} setRegistrationClick={setRegistrationClick} />

      )}
    </main>
  );
}