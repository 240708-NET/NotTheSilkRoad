"use client";
import { useEffect, useState } from 'react'
import accountstyles from "./page.module.css";
import { usePathname } from "next/navigation";
import { useContext } from 'react';
import { LoginContext } from "../../contexts/LoginContext";
import Listing from "@/components/Listing/Listing";

function Account() {
  const path = usePathname();
  const [customer, setCustomer] = useState();

  const [seller, setIsSeller] = useState(false);

  const { isSeller, user } = useContext(LoginContext); // isSeller

  const [loading, setLoading] = useState(true);
  const [listLoading, setListLoading] = useState(false);

  const [productInfo, setProductInfo] = useState({
    title: "",
    price: "",
    description: "",
  });

  const [products, setProducts] = useState([]);

  useEffect(() => {
    const getCustomer = async () => {

      let itemArray = path.split('/account/');
      let itemName = itemArray[1]
      console.log(itemName)

      if (isSeller) {
        const response = await fetch(`http://localhost:5224/seller/${itemName}`);

        const customer = await response.json();
        setCustomer(customer);
      }
      else {
        const response = await fetch(`http://localhost:5224/customer/${itemName}`);

        const customer = await response.json();
        setCustomer(customer);
      }
      setLoading(false);
    }
    getCustomer();

  }, [])

  useEffect(() => {
    if (isSeller) {
      console.log(isSeller);
      setLoading(true);

      getProducts();
    }
  }, [])

  const getProducts = async () => {
    setLoading(true);
    const response = await fetch("http://localhost:5224/product");
    const data = await response.json();
    console.log(data);
    const filteredData = data.filter((product: any) => product.seller.id === user.id);
    console.log(filteredData);
    setProducts(filteredData);
    console.log(products);
    setLoading(false);
  }

  const addProduct = async () => {
    const response = await fetch("http://localhost:5224/product", {
      method: "POST",

      body: JSON.stringify({
        title: productInfo.title,
        price: productInfo.price,
        description: productInfo.description,
        seller: user,
      }),

      headers: {
        "Content-Type": "application/json",
      },
    })
  }

  const deleteProduct = async (myID: string) => {
    setListLoading(true);

    const newList = products.filter((product: any) => product.id !== myID);
    console.log("New List: ");
    console.log(newList);

    setProducts(newList);

    setListLoading(false);

    const response = await fetch(`http://localhost:5224/product/${myID}`, {
      method: "DELETE",
    });
    const data = await response.json();
    console.log(data);
  };

  if (loading) {
    return <div>Loading...</div>
  }

  return (
    <div className={accountstyles.container}>

      <div className={accountstyles.info}>
        <input type="text" placeholder={customer ? customer.name : "name"} />
        <input type="email" placeholder={customer ? customer.email : "email"} />
        {!seller && <input type="text" placeholder={customer ? customer.address : "address"} />}
        <button>Delete Account</button>
      </div>

      {isSeller && (
        <div className={accountstyles.product}>
          <h2>Add a Product</h2>
          <input type="text" placeholder="product name" onChange={(e) => setProductInfo({ ...productInfo, title: e.target.value })} />
          <input type="number" placeholder="price" onChange={(e) => setProductInfo({ ...productInfo, price: e.target.value })} />
          <input type="text" placeholder="image" onChange={(e) => setProductInfo({ ...productInfo, description: e.target.value })} />
          {/* <textarea rows="3" type="number" placeholder="description" /> */}

          <button onClick={addProduct}>Add Product</button>
        </div>
      )}

<div className={accountstyles.list}>


  {!listLoading && (
    <>
{products && products.map((item, key) => {
  return (<Listing key={key} title={item.title} image={item.description} price={item.price} isAccountPage={true} productId={item.id} deleteProduct={deleteProduct}/>)
})}
</>

  )
  
  
  }
        

    </div>
    </div>


  );
}

export default Account;