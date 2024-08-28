"use client";
import React, { useEffect, useState } from 'react';
import './page.module.css'; // Assuming your styles are in page.module.css
import { usePathname } from "next/navigation";
import { useContext } from 'react';
import { LoginContext } from "../../contexts/LoginContext";
import Listing from "@/components/Listing/Listing";
import accountstyles from "./page.module.css";
import NavbarLogo from "@/components/NavbarLogo/NavbarLogo";
import router from "next/router";
import Navbar from "@/components/Navbar/Navbar";
import WaveLoading from '@/components/Loading/WaveLoading';

const Account = () => {
  const path = usePathname();
  const [customer, setCustomer] = useState();

  const [seller, setIsSeller] = useState(false);

  const { isSeller, user } = useContext(LoginContext); // isSeller

  const [loading, setLoading] = useState(true);
  const [listLoading, setListLoading] = useState(false);

  const [isLogin, setIsLogin] = useState(false);
  const [showLogin, setShowLogin] = useState(false);
  const [isAccountClick, setIsAccountClick] = useState(false);
  const [registrationClick, setRegistrationClick] = useState(false);



  const [productInfo, setProductInfo] = useState({
    title: "",
    price: "",
    description: "",
    imageUrl: "",
    quantity: "",
  });

  const [userInfo, setUserInfo] = useState({
    confirmPassword: "",
  })

  const [products, setProducts] = useState([]);

  useEffect(() => {
    const getCustomer = async () => {

      let itemArray = path.split('/account/');
      let itemName = itemArray[1]
      console.log(itemName)

      if (isSeller) {
        const response = await fetch(`http://localhost:${process.env.NEXT_PUBLIC_PORT}/seller/${itemName}`);

        const customer = await response.json();

        setUserInfo({
          id: customer.id,
          name: customer.name,
          email: customer.email,
          address: customer.address,
          password: customer.password,
          orders: customer.orders,
        })


        setCustomer(customer);
      }
      else {
        const response = await fetch(`http://localhost:${process.env.NEXT_PUBLIC_PORT}/customer/${itemName}`);

        const customer = await response.json();

        setUserInfo({
          id: customer.id,
          name: customer.name,
          email: customer.email,
          address: customer.address,
          password: customer.password,
          orders: customer.orders,
        })

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
    const response = await fetch(`http://localhost:${process.env.NEXT_PUBLIC_PORT}/product`);
    const data = await response.json();
    console.log(data);
    const filteredData = data.filter((product: any) => product.seller.id === user.id);
    console.log(filteredData);
    setProducts(filteredData);
    console.log(products);
    setLoading(false);
  }

  const addProduct = async () => {
    const response = await fetch(`http://localhost:${process.env.NEXT_PUBLIC_PORT}/product`, {
      method: "POST",

      body: JSON.stringify({
        title: productInfo.title,
        price: productInfo.price,
        description: productInfo.description,
        imageUrl: productInfo.imageUrl,
        quantity: productInfo.quantity,
        seller: user,
      }),

      headers: {
        "Content-Type": "application/json",
      },
    })
  }

  const updateUserInfo = async () => {
      setLoading(true);
    let itemArray = path.split('/account/');
    let itemName = itemArray[1]

      if (isSeller) {
        const response = await fetch(`http://localhost:${process.env.NEXT_PUBLIC_PORT}/seller/${itemName}`, {
          method: "PUT",

          body: JSON.stringify({
            id: itemName,
            email: userInfo.email,
            name: userInfo.name,
            password: userInfo.password,
            address: userInfo.address,
            orders: userInfo.orders,
          }),

          headers: {
            "Content-Type": "application/json",
          },
        })

        if (!response.ok) {
          const errorData = await response.json(); // Parse error message
          console.error("Error updating user:", errorData.message || errorData);
          console.error("Error updating user:", errorData.message || errorData);
          console.error("Validation Errors:", errorData.errors); // Log the errors object
          // Handle specific errors based on the error details
          // Handle the error appropriately (e.g., display an error message to the user)
        } else {
          console.log("User updated successfully!");
        }
      }
      else {
        const response = await fetch(`http://localhost:${process.env.NEXT_PUBLIC_PORT}/customer/${itemName}`, {
          method: "PUT",

          body: JSON.stringify({
            id: itemName,
            email: userInfo.email,
            name: userInfo.name,
            password: userInfo.password,
            address: userInfo.address,
            orders: userInfo.orders,
          }),

          headers: {
            "Content-Type": "application/json",
          },
        })

        if (!response.ok) {
          const errorData = await response.json(); // Parse error message
          console.error("Error updating user:", errorData.message || errorData);
          console.error("Error updating user:", errorData.message || errorData);
          console.error("Validation Errors:", errorData.errors); // Log the errors object
          // Handle specific errors based on the error details
          // Handle the error appropriately (e.g., display an error message to the user)
        } else {
          console.log("User updated successfully!");
        }
      }
      setLoading(false);
  }

  const deleteProduct = async (myID: string) => {
    setListLoading(true);

    const newList = products.filter((product: any) => product.id !== myID);
    console.log("New List: ");
    console.log(newList);

    setProducts(newList);

    setListLoading(false);

    const response = await fetch(`http://localhost:${process.env.NEXT_PUBLIC_PORT}/product/${myID}`, {
      method: "DELETE",
    });
    const data = await response.json();
    console.log(data);
  };

  if (loading) {
    return <WaveLoading />
  }

  return (

    <>
      <div className={accountstyles.navbar}>
        <NavbarLogo showLogin={showLogin} setShowLogin={setShowLogin} isLogin={isLogin} setIsLogin={setIsLogin}
          isAccountClick={isAccountClick} setIsAccountClick={setIsAccountClick} />
      </div>


      {isSeller ? (<div className="container-fluid">
        <div className="row">
          <div className={accountstyles.testContainer}> {/* Update User Section */}
            <div className="row">
            <div className="card">
                  <div className="card-body">
                    <div className="e-profile">
                      <div className="row">
                        <div className="col d-flex flex-column flex-sm-row justify-content-between mb-3">
                          <div className="text-center text-sm-left mb-2 mb-sm-0">
                            <h4 className="pt-sm-2 pb-1 mb-0 text-nowrap">Update Account Information</h4>
                          </div>
                        </div>
                      </div>
                      <div className="tab-content pt-3">
                        <div className="tab-pane active">
                          <form className="form">
                            <div className="row">
                              <div className="col">
                                <div className="row">
                                  <div className="col">
                                    <div className="form-group">
                                      <label>Change Name</label>
                                      <input
                                        onChange={(e) => setUserInfo({ ...userInfo, name: e.target.value })}
                                        className="form-control"
                                        type="text"
                                        name="name"
                                        placeholder={customer ? customer.name : 'John Smith'}
                                      // Set value from state
                                      />
                                    </div>
                                  </div>
                                </div>
                                <div className="row">
                                  <div className="col">
                                    <div className="form-group">
                                      <label>Change Email</label>
                                      <input
                                        onChange={(e) => setUserInfo({ ...userInfo, email: e.target.value })}
                                        className="form-control"
                                        type="text"
                                        name="email"
                                        placeholder={customer ? customer.email : '@johnny.s'}
                                      />
                                    </div>
                                  </div>
                                </div>
                              </div>
                              <div className="row">
                                <div className="col mb-3">
                                  <div className="row">
                                    <div className="col">
                                      <div className="form-group">
                                        <label>New Password</label>
                                        <input onChange={(e) => setUserInfo({ ...userInfo, password: e.target.value })} className="form-control" type="password" placeholder="••••••" />
                                      </div>
                                    </div>
                                  </div>
                                  <div className="row">
                                    <div className="col">
                                      <div className="form-group">
                                        <label>Repeat New Password</label>
                                        <input onChange={(e) => setUserInfo({ ...userInfo, confirmPassword: e.target.value })} className="form-control" type="password" placeholder="••••••" />
                                      </div>
                                    </div>
                                  </div>
                                </div>
                              </div>
                            </div>
                            <div className="row">
                              <div className="col d-flex justify-content-end">
                                {(userInfo.password === userInfo.confirmPassword) || (userInfo.password == null && userInfo.confirmPassword == null) || (userInfo.password == "" && userInfo.confirmPassword == "") ? <button onClick={updateUserInfo} className="btn btn-primary" >Save Changes</button> : <button className="btn btn-primary" disabled>Passwords Must Match</button>}
                              </div>
                            </div>
                          </form>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
      
       
            </div>
          </div>

          <div className={accountstyles.testContainer}>
            {isSeller && (
              <div>
                {isSeller && (
          <div className={accountstyles.product}>
            <h2>Add a Product</h2>
            <input type="text" placeholder="product name" onChange={(e) => setProductInfo({ ...productInfo, title: e.target.value })} />
            <input type="number" placeholder="price" onChange={(e) => setProductInfo({ ...productInfo, price: e.target.value })} />
            <input type="text" placeholder="description" onChange={(e) => setProductInfo({ ...productInfo, description: e.target.value })} />
            <input type="number" placeholder="quantity" onChange={(e) => setProductInfo({ ...productInfo, quantity: e.target.value })} />
            <input type="text" placeholder="image url" onChange={(e) => setProductInfo({ ...productInfo, imageUrl: e.target.value })} />
            <button className="btn btn-primary" onClick={addProduct}>Add Product</button>
          </div>
        )}
              </div>
            )}
          </div>

          <div className={accountstyles.list}>
          {!listLoading && (
            <>
              
              {products && products.map((item, key) => {
                return (<Listing key={key} title={item.title} description={item.description} imageUrl={item.imageUrl} price={item.price} isAccountPage={true} productId={item.id} quantity={item.quantity} deleteProduct={deleteProduct} />)
              })}
            </>
          )
          }
        </div>
      </div>


        </div>) : 
        
        
        (<div className="container-fluid">
          <div className="row">
            <div className={accountstyles.testContainer}> {/* Update User Section */}
              <div className="row">
              <div className="card">
                    <div className="card-body">
                      <div className="e-profile">
                        <div className="row">
                          <div className="col d-flex flex-column flex-sm-row justify-content-between mb-3">
                            <div className="text-center text-sm-left mb-2 mb-sm-0">
                              <h4 className="pt-sm-2 pb-1 mb-0 text-nowrap">Update Account Information</h4>
                            </div>
                          </div>
                        </div>
                        <div className="tab-content pt-3">
                          <div className="tab-pane active">
                            <form className="form">
                              <div className="row">
                                <div className="col">
                                  <div className="row">
                                    <div className="col">
                                      <div className="form-group">
                                        <label>Change Name</label>
                                        <input
                                          onChange={(e) => setUserInfo({ ...userInfo, name: e.target.value })}
                                          className="form-control"
                                          type="text"
                                          name="name"
                                          placeholder={customer ? customer.name : 'John Smith'}
                                        // Set value from state
                                        />
                                      </div>
                                    </div>
                                  </div>
                                  <div className="row">
                                    <div className="col">
                                      <div className="form-group">
                                        <label>Change Email</label>
                                        <input
                                          onChange={(e) => setUserInfo({ ...userInfo, email: e.target.value })}
                                          className="form-control"
                                          type="text"
                                          name="email"
                                          placeholder={customer ? customer.email : '@johnny.s'}
                                        />
                                      </div>
                                    </div>
                                  </div>
                                </div>
                                <div className="row">
                                  <div className="col mb-3">
                                    <div className="row">
                                      <div className="col">
                                        <div className="form-group">
                                          <label>New Password</label>
                                          <input onChange={(e) => setUserInfo({ ...userInfo, password: e.target.value })} className="form-control" type="password" placeholder="••••••" />
                                        </div>
                                      </div>
                                    </div>
                                    <div className="row">
                                      <div className="col">
                                        <div className="form-group">
                                          <label>Repeat New Password</label>
                                          <input onChange={(e) => setUserInfo({ ...userInfo, confirmPassword: e.target.value })} className="form-control" type="password" placeholder="••••••" />
                                        </div>
                                      </div>
                                    </div>
                                  </div>
                                </div>
                              </div>
                              <div className="row">
                                <div className="col d-flex justify-content-end">
                                  {(userInfo.password === userInfo.confirmPassword) || (userInfo.password == null && userInfo.confirmPassword == null) || (userInfo.password == "" && userInfo.confirmPassword == "") ? <button onClick={updateUserInfo} className="btn btn-primary" >Save Changes</button> : <button className="btn btn-primary" disabled>Passwords Must Match</button>}
                                </div>
                              </div>
                            </form>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
              </div>
            </div>
        </div>
  
  
          </div>)}

      
     

      {/* Rest of your code */}
    </>

    // <>
    //   <div className={accountstyles.navbar}>
    //     <NavbarLogo showLogin={showLogin} setShowLogin={setShowLogin} isLogin={isLogin} setIsLogin={setIsLogin}
    //       isAccountClick={isAccountClick} setIsAccountClick={setIsAccountClick} />
    //   </div>


    //   <div className="container-fluid">
    //     <div className="row flex-lg-nowrap">
    //       <div className="col">
    //         <div className="row">
    //           <div className="col mb-3">
            //     <div className="card">
            //       <div className="card-body">
            //         <div className="e-profile">
            //           <div className="row">
            //             <div className="col d-flex flex-column flex-sm-row justify-content-between mb-3">
            //               <div className="text-center text-sm-left mb-2 mb-sm-0">
            //                 <h4 className="pt-sm-2 pb-1 mb-0 text-nowrap">{customer ? customer.name : 'John Smith'}</h4>
            //                 <p className="mb-0">{customer ? customer.email : '@johnny.s'}</p>

            //               </div>
            //             </div>
            //           </div>
            //           <ul className="nav nav-tabs">
            //             <li className="nav-item"><a href="#" className="active nav-link">Settings</a></li>
            //           </ul>
            //           <div className="tab-content pt-3">
            //             <div className="tab-pane active">
            //               <form className="form">
            //                 <div className="row">
            //                   <div className="col">
            //                     <div className="row">
            //                       <div className="col">
            //                         <div className="form-group">
            //                           <label>Change Name</label>
            //                           <input
            //                             onChange={(e) => setUserInfo({ ...userInfo, name: e.target.value })}
            //                             className="form-control"
            //                             type="text"
            //                             name="name"
            //                             placeholder={customer ? customer.name : 'John Smith'}
            //                           // Set value from state
            //                           />
            //                         </div>
            //                       </div>
            //                     </div>
            //                     <div className="row">
            //                       <div className="col">
            //                         <div className="form-group">
            //                           <label>Change Email</label>
            //                           <input
            //                             onChange={(e) => setUserInfo({ ...userInfo, email: e.target.value })}
            //                             className="form-control"
            //                             type="text"
            //                             name="email"
            //                             placeholder={customer ? customer.email : '@johnny.s'}
            //                           />
            //                         </div>
            //                       </div>
            //                     </div>
            //                   </div>
            //                   <div className="row">
            //                     <div className="col mb-3">
            //                       <div className="row">
            //                         <div className="col">
            //                           <div className="form-group">
            //                             <label>New Password</label>
            //                             <input onChange={(e) => setUserInfo({ ...userInfo, password: e.target.value })} className="form-control" type="password" placeholder="••••••" />
            //                           </div>
            //                         </div>
            //                       </div>
            //                       <div className="row">
            //                         <div className="col">
            //                           <div className="form-group">
            //                             <label>Repeat New Password</label>
            //                             <input onChange={(e) => setUserInfo({ ...userInfo, confirmPassword: e.target.value })} className="form-control" type="password" placeholder="••••••" />
            //                           </div>
            //                         </div>
            //                       </div>
            //                     </div>
            //                   </div>
            //                 </div>
            //                 <div className="row">
            //                   <div className="col d-flex justify-content-end">
            //                     {(userInfo.password === userInfo.confirmPassword) || (userInfo.password == null && userInfo.confirmPassword == null) || (userInfo.password == "" && userInfo.confirmPassword == "") ? <button onClick={updateUserInfo} className="btn btn-primary" >Save Changes</button> : <button className="btn btn-primary" disabled>Passwords Must Match</button>}
            //                   </div>
            //                 </div>
            //               </form>
            //             </div>
            //           </div>
            //         </div>
            //       </div>
            //     </div>
            //   </div>
            // </div>

    //         {/* Omit Right side section */}
    //       </div>
    //     </div>
        
        // {isSeller && (
        //   <div className={accountstyles.product}>
        //     <h2>Add a Product</h2>
        //     <input type="text" placeholder="product name" onChange={(e) => setProductInfo({ ...productInfo, title: e.target.value })} />
        //     <input type="number" placeholder="price" onChange={(e) => setProductInfo({ ...productInfo, price: e.target.value })} />
        //     <input type="text" placeholder="description" onChange={(e) => setProductInfo({ ...productInfo, description: e.target.value })} />
        //     <input type="number" placeholder="quantity" onChange={(e) => setProductInfo({ ...productInfo, quantity: e.target.value })} />
        //     <input type="text" placeholder="image url" onChange={(e) => setProductInfo({ ...productInfo, imageUrl: e.target.value })} />
        //     {/* <textarea rows="3" type="number" placeholder="description" /> */}

        //     <button onClick={addProduct}>Add Product</button>
        //   </div>
        // )}
      //   <div className={accountstyles.list}>
      //     {!listLoading && (
      //       <>
      //         {products && products.map((item, key) => {
      //           return (<Listing key={key} title={item.title} description={item.description} imageUrl={item.imageUrl} price={item.price} isAccountPage={true} productId={item.id} quantity={item.quantity} deleteProduct={deleteProduct} />)
      //         })}
      //       </>
      //     )
      //     }
      //   </div>
      // </div>
    // </>
  );
};

export default Account;