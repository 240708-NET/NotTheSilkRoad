"use client"
import React, { useState, useEffect, useContext } from 'react';
import CartStyles from './Cart.module.css';
import { CartContext } from '@/app/contexts/CartContext';
import { LoginContext } from '../contexts/LoginContext';
import NavbarLogo from '@/components/NavbarLogo/NavbarLogo';
import styles from "../page.module.css";
import { useRouter } from 'next/navigation';


function Cart() {
  const {cart, cartId} = useContext(CartContext)
  const {user} = useContext(LoginContext)
  const router = useRouter()

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

    console.log(cart)
    
  }, [])

  const toHomePage = () => {
    router.push(`/`);
}

  const createOrder = async () => {
    console.log(cart)
    console.log(user)
    try {
    const response = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/order/${cartId}`, {
      method: 'PUT',
      body: JSON.stringify({
        id: cartId,
        customer: {
          id: user.id
        },
        items: cart,
        date: formatDate(),
        active: false,
        shippingAddress: user.address
      }),
      headers: {
        "Content-Type": "application/json",
      }, 
    })

    if(response.ok){
      console.log('Items purchased successfully')
      router.push('/?success=true')
   
    } else {
      console.error('Something went wrong')
    }
  } catch (err){
    console.error(err)
  }
  }

  const formatDate = () => {
    let month = ''
    let day = ''
    if(new Date(Date.now()).getMonth() < 10){
        month = '0'+new Date(Date.now()).getMonth()
    } else {
        month = String(new Date(Date.now()).getMonth())
    }
    if(new Date(Date.now()).getDate() < 10){
        day = '0'+new Date(Date.now()).getDate()
    } else {
        day = String(new Date(Date.now()).getDate())
    }

    return `${new Date(Date.now()).getFullYear()}-${month}-${day}`
}
  return (
    
    <div className={CartStyles.pageBackground}>

    <section className={CartStyles.section} style={{ backgroundColor: '#eee'}}>
      
      {/* <a href="/" className="navbar-brand">
        <img src="images/NotTheSilkRoadLogo.png" alt="Logo" className={CartStyles.logo} />
      </a> */}
      <div className="container py-5 h-100">
        <div className="row
 d-flex justify-content-center align-items-center h-100">


<div className={styles.navbar}>
    <NavbarLogo showLogin={showLogin} setShowLogin={setShowLogin} isLogin={isLogin} setIsLogin={setIsLogin}
            isAccountClick={isAccountClick} setIsAccountClick={setIsAccountClick}  />
    </div>

          <div className="col">
            <div className="card">
              <div className="card-body p-4">
                <div className="row">
                  <div className="col-lg-7">
                    <h5 className="mb-3">
                      <div onClick={()=> router.push('/')} style={{cursor: "pointer"}} className="text-body">

                        <img src="images/arrow-90deg-left.svg" alt="Arrow Back" />
                        <i className="fas fa-long-arrow-alt-left me-2"></i>
                        Continue Shopping
                      </div>
                    </h5>
                    <hr />
                    <div className="d-flex justify-content-between align-items-center mb-4">
                      <div>
                        <p className="mb-1">Shopping cart</p>
                        <p className="mb-0">You have {cart.length} items in your cart</p>
                      </div>
                      <div>
                        <p className="mb-0">
                          <span className="text-muted">Sort by:</span>
                          <a href="#!" className="text-body">
                            price
                            <i className="fas fa-angle-down mt-1"></i>
                          </a>
                        </p>
                      </div>
                    </div>
                    
                    {/* List of shopping cart items */}
                   
                      

                        {cart.map((item, key) => {
                          console.log(item)

                          return (
                            <div  key={key} className="card mb-3">
                            <div className="card-body">
                            <div className="d-flex justify-content-between">
                          <div className="d-flex flex-row align-items-center">

                            <img
                              src={item.product.imageUrl}

                              alt="Shopping item"
                              className="img-fluid rounded-3"
                              style={{ width: '65px' }}
                            />
                            <div className="ms-3">
                              <h5>{item.product.title}</h5>
                              <p className="small mb-0">{item.product.description}</p>
                            </div>
                          </div>
                          <div className="d-flex flex-row align-items-center">
                            <div style={{
                              width:
                                '50px'
                            }}>
                              <h5 className="fw-normal mb-0">{item.quantity}</h5>
                            </div>
                            <div style={{ width: '80px' }}>
                              <h5 className="mb-0">{formatter.format(item.price)}</h5>
                            </div>
                            <a href="#!" style={{ color: '#cecece' }}>
                              <i className="fas fa-trash-alt"></i>
                            </a>
                          </div>
                        </div>
                        </div>
                        </div>
                          )
                        })}
                        
                    
                   

                    {/* More shopping cart items... */}
                  </div>

                  <div
                    className="col-lg-5">
                    <div className="card bg-primary text-white rounded-3">
                      <div className="card-body">
                        <div className="d-flex justify-content-between align-items-center mb-2">
                          <div className={CartStyles.paymentInfo}>
                            <h1 className="mb-0">Payment Information</h1>
                          </div>
                        </div>

                        <form className="mt-4">
                          <div data-mdb-input-init className="form-outline form-white mb-4">
                            <input
                              type="text"
                              id="typeName"
                              className="form-control form-control-lg"
                              placeholder="Cardholder's Name"
                            />
                            <label className="form-label" htmlFor="typeName">
                              Cardholder's Name
                            </label>
                          </div>

                          <div data-mdb-input-init className="form-outline form-white mb-4">
                            <input
                              type="text"
                              id="typeText"
                              className="form-control form-control-lg"
                              placeholder="1234 5678 9012 3457"
                            />
                            <label className="form-label" htmlFor="typeText">
                              Card Number
                            </label>
                          </div>

                          <div className="row mb-4">
                            <div className="col-md-6">
                              <div data-mdb-input-init className="form-outline form-white">
                                <div className={CartStyles.expDate}>
                                  <input
                                    type="text"
                                    id="typeExp"
                                    className="form-control form-control-lg"
                                    placeholder="MM"
                                  />

                                  <input
                                    type="text"
                                    id="typeExp"
                                    className="form-control form-control-lg"
                                    placeholder="YYYY"
                                  />
                                </div>

                                <label className="form-label" htmlFor="typeExp">
                                  Expiration
                                </label>
                              </div>
                            </div>

                            <div className="col-md-6">
                              <div data-mdb-input-init className="form-outline form-white">
                                <input
                                  type="password"
                                  id="typeText"
                                  className="form-control form-control-lg"
                                  placeholder="&#9679;&#9679;&#9679;"

                                />
                                <label className="form-label" htmlFor="typeText">
                                  Cvv
                                </label>
                              </div>
                            </div>
                          </div>
                        </form>

                        <hr className="my-4" />

                        <div className="d-flex justify-content-between">
                          <h2 className="mb-2">Total</h2>
                        </div>
                        <h4 className="mb-2">{formatter.format(cart.reduce((acc, product) => acc + product.price, 0))}</h4>

                        <button
                        onClick={createOrder}
                          type="button"
                          data-mdb-button-init
                          data-mdb-ripple-init
                          className="btn btn-info btn-block btn-lg"
                        >
                          <div className="d-flex justify-content-between">
                            <span>
                              Checkout <i className="fas fa-long-arrow-alt-right ms-2"></i>
                            </span>
                          </div>
                        </button>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
    </div>
  );
};

export default Cart;