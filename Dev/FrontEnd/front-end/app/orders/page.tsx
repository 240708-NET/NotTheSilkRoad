"use client"
import orderstyles from './page.module.css'

import {useEffect, useState, useContext} from 'react'
import {LoginContext} from '@/app/contexts/LoginContext'

import NavbarLogo from '@/components/NavbarLogo/NavbarLogo'
import styles from "./page.module.css";

function Orders(){

    const {user} = useContext(LoginContext)
    const [orders, setOrders] = useState([])

    const [isLogin, setIsLogin] = useState(false);
  const [showLogin, setShowLogin] = useState(false);
  const [isAccountClick, setIsAccountClick] = useState(false);
  const [registrationClick, setRegistrationClick] = useState(false);

    const formatter = new Intl.NumberFormat("en-US", {
        style: "currency",
        currency: "USD",
        maximumFractionDigits: 0,
      });


    useEffect(() => {
        const getOrders = async () => {
            const response = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/order`)

            if(response.ok){
                const data = await response.json();
                const filteredData = data.filter(x => x.customer.id === user.id && x.active === false)
                console.log(filteredData)
                setOrders(filteredData)
                
            } else {
                console.error("Something went wrong")
            }
        }
        getOrders();
    }, [])


    return (
        <div className={orderstyles.container}>

           <div className={orderstyles.navbar}>
            <NavbarLogo showLogin={showLogin} setShowLogin={setShowLogin} isLogin={isLogin} setIsLogin={setIsLogin}
            isAccountClick={isAccountClick} setIsAccountClick={setIsAccountClick}  />
            
            <h2 className={orderstyles.title + " h2 text-center mb-3 border-bottom border-dark"}>Previous Orders</h2>

           </div>


        {orders && orders.map((item, key) => {

            return (
                
                <div className={orderstyles.testStyle}>
                    
                <div className={orderstyles.info}>
                    <p>Customer Name: {item.customer.name}</p>
                    <p>Order #: {item.id}</p>
                    <p>Date Ordered: {item.date}</p>
                   <p>Total: {formatter.format(item.items.reduce((acc, product) => acc + product.price, 0))}</p>

                </div>
                </div>
            )
        })}
       


        
        </div>
    )
}

export default Orders;