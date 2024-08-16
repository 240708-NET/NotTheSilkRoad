"use client";
import { useState, useEffect } from "react";
import Navbar from "@/components/Navbar/Navbar";
import styles from "./page.module.css";
import Login from "@/components/Login/Login";
export default function Home() {
  const [isLogin, setIsLogin] = useState(false);
  const [showLogin, setShowLogin] = useState(false);

  return (
    <main className={styles.main}>
      <Navbar showLogin = {showLogin} setShowLogin={setShowLogin} isLogin={isLogin} setIsLogin={setIsLogin}/>
      {showLogin && <Login />}
    </main>
  );
}