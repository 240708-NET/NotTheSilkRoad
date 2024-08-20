"use client";
import { useState, useEffect } from "react";
import Navbar from "@/components/Navbar/Navbar";
import styles from "./page.module.css";
import Login from "@/components/Login/Login";
import Listing from "@/components/Listing/Listing"
import {products} from "@/components/Listing/listings.js"
import AccountMenu from "@/app/AccountMenu/page";

export default function Home() {
  const [isLogin, setIsLogin] = useState(false);
  const [showLogin, setShowLogin] = useState(false);
  const [isAccountClick, setIsAccountClick] = useState(false);



  

  return (
    <main className={styles.main}>

      <Navbar showLogin={showLogin} setShowLogin={setShowLogin} isLogin={isLogin} setIsLogin={setIsLogin}
        isAccountClick={isAccountClick} setIsAccountClick={setIsAccountClick} />
      {showLogin && <Login />}

      <div className={styles.listing}>
      {products.map((item, key) => {
        return (
          <Listing key={key} title={item.title} image={item.image} price={item.price}/>
        )
      })}
      </div>

      
        {isAccountClick && <AccountMenu />}


    </main>
  );
}