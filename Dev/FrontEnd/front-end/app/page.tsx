"use client";
import { useState, useEffect, useRef } from "react";
import Navbar from "@/components/Navbar/Navbar";
import styles from "./page.module.css";
import Login from "@/components/Login/Login";
import Listing from "@/components/Listing/Listing"
import RegistrationForm from "@/components/RegistrationForm/RegistrationForm";
import { useRouter } from "next/navigation";
import { LoginContext } from "./contexts/LoginContext";
import { CartContext } from "./contexts/CartContext";
import { useContext } from "react";
import WaveLoading from "@/components/Loading/WaveLoading";
import Toast from "@/components/Toast/Toast";


export default function Home() {

  const apiUrl = process.env.NEXT_PUBLIC_API_URL;

  console.log(apiUrl);

  const router = useRouter();
  

  const { isSeller, user} = useContext(LoginContext); // isSeller
  const {cart, setCart} = useContext(CartContext)


  const [isLogin, setIsLogin] = useState(false);
  const [showLogin, setShowLogin] = useState(false);
  const [isAccountClick, setIsAccountClick] = useState(false);
  const [registrationClick, setRegistrationClick] = useState(false);
  const [loading, setLoading] = useState(true);
  const [products, setProducts] = useState([]);
  const [successMessage, setSuccessMessage] = useState(false)

 

  useEffect(() => {
    if (isLogin) {
      console.log(isLogin);
      console.log(user);
      console.log(isSeller);
      console.log("Context is Working!");

      


    }
  }, [isLogin])

  useEffect(() => {
    const query = new URLSearchParams(window.location.search);
    if(query.get('success')){
      setSuccessMessage(true)
      setTimeout(() => {setSuccessMessage(false)}, 3000)
    }
  }, [])

  useEffect(() => {

    const getCustomer = async () => {
      const response = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/customer`);
      const data = await response.json();
    };

  }, [])

  useEffect(() => {
    getProducts();
  }, [])

  const getProducts = async () => {
    setLoading(true);
    const response = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/product`);
    const data = await response.json();
    console.log(data);
    setProducts(data);
    setLoading(false);
  }


  

  const [searchInput, setSearchInput] = useState("");
  const [searchResult, setSearchResults] = useState([]);
  

  

  const searchHandler = (searchTerm) => {
    setSearchInput(searchTerm);
    if (searchTerm !== "") {
      const newList = products.filter((item) => {
        return Object.values(item)
          .join(" ")
          .toLowerCase()
          .includes(searchTerm.toLowerCase());
      });
      setSearchResults(newList);
    } else {
      setSearchResults(products);
    }
  };


  if(loading) return <WaveLoading />


  return (
    
    
    <main className={styles.main}>
      
      {/* <RegistrationForm /> */}
      {successMessage && (<Toast message={"Your purchase was successful!"}/>)}

      {!showLogin ? (
        <>

          <Navbar showLogin={showLogin} setShowLogin={setShowLogin} isLogin={isLogin} setIsLogin={setIsLogin}
            isAccountClick={isAccountClick} setIsAccountClick={setIsAccountClick} searchHandler={searchHandler}  />

          <div className={styles.listing}>
            {(searchResult.length > 0 ? searchResult : products).map((item, key) => {
              return (


                <Listing key={key} title={item.title} description={item.description} imageUrl={item.imageUrl} price={item.price} productId={item.id} quantity={item.quantity} />

              )
            })}
          </div>

          {isAccountClick ? (

            <div className="dropdown">
              <button className="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                Account Menu
              </button>
              <div className="dropdown-menu" aria-labelledby="dropdownMenuButton">
                <a className="dropdown-item" href="#">Action</a>
                <a className="dropdown-item" href="#">Another action</a>
                <a className="dropdown-item" href="#">Something else here</a>
              </div>
            </div>


          ) : (<></>)}

        </>

      ) : (

        <Login showLogin={showLogin} setShowLogin={setShowLogin} isLogin={isLogin} setIsLogin={setIsLogin}
          isAccountClick={isAccountClick} setIsAccountClick={setIsAccountClick} registrationClick={registrationClick} setRegistrationClick={setRegistrationClick} />

      )}
    </main>
  );
}