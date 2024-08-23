"use client";
import {useEffect, useState} from 'react'
import accountstyles from "./page.module.css";
import { useSearchParams, usePathname } from "next/navigation";
function Account() {
  const searchParams = useSearchParams();
  const path = usePathname();
  const [customer, setCustomer] = useState();

  const seller = searchParams.get("seller");

  useEffect(() => {

    const getCustomer = async () => {
   
        let itemArray = path.split('/account/');
        let itemName = itemArray[1]
        console.log(itemName)

        const response = await fetch(`http://localhost:5224/customer/${itemName}`)

        const customer = await response.json();
        setCustomer(customer)
    }

    getCustomer();



  }, [])

  return (
    <div className={accountstyles.container}>

        <div className={accountstyles.info}>
      <input type="text" placeholder={customer ? customer.name : "name"} />
      <input type="email" placeholder={customer ? customer.email : "email"} />
      {!seller && <input type="text" placeholder={customer ? customer.address : "address"} />}
      <button>Delete Account</button>
      </div>

      {seller && (
        <div className={accountstyles.product}>
          <h2>Add a Product</h2>
          <input type="text" placeholder="product name" />
          <input type="number" placeholder="price" />
          <input type="file" placeholder="image" />
          <textarea rows="3" type="number" placeholder="description" />
        </div>
      )}
    </div>
  );
}

export default Account;
