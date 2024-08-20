"use client";
import { useState, useEffect } from "react";
import Navbar from "@/components/Navbar/Navbar";
import styles from "./page.module.css";
import Login from "@/components/Login/Login";
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
      
        {isAccountClick && <AccountMenu />}

    </main>
  );
}