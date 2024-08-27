"use client"
import orderstyles from './page.module.css'

import {useEffect, useState, useContext} from 'react'
import {LoginContext} from '@/app/contexts/LoginContext'

function Orders(){

    const {user} = useContext(LoginContext)
    const [orders, setOrders] = useState([])

    const formatter = new Intl.NumberFormat("en-US", {
        style: "currency",
        currency: "USD",
        maximumFractionDigits: 0,
      });


    useEffect(() => {
        const getOrders = async () => {
            const response = await fetch(`http://localhost:5224/order`)

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

        <h1>Order History</h1>

        {orders && orders.map((item, key) => {

            return (
                <div className={orderstyles.info}>
                    <p>Customer Name: {item.customer.name}</p>
                    <p>Order #: {item.id}</p>
                    <p>Date Ordered: {item.date}</p>
                   <p>Total: {formatter.format(item.items.reduce((acc, product) => acc + product.price, 0))}</p>

                </div>
            )
        })}
       


        
        </div>
    )
}

export default Orders;