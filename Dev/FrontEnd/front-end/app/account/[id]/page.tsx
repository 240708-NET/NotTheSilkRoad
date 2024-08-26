"use client";
import React, { useEffect, useState } from 'react';
import './page.module.css'; // Assuming your styles are in page.module.css
import { usePathname } from "next/navigation";
import { useContext } from 'react';
import { LoginContext } from "../../contexts/LoginContext";
import Listing from "@/components/Listing/Listing";
import accountstyles from "./page.module.css";

const Account = () => {
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

  const [userInfo, setUserInfo] = useState({
    id: customer ? customer.id : '',
    name: customer ? customer.name : '',
    email: customer ? customer.email : '',
    address: customer ? customer.address : '',
    password: customer ? customer.password : '',
    orders: customer ? customer.orders : [],
  })

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

  const updateUserInfo = async () => {
    setLoading(true);
    let itemArray = path.split('/account/');
    let itemName = itemArray[1]

    if (isSeller) {
      const response = await fetch(`http://localhost:5224/seller/${itemName}`, {
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
    }
    else {
      const response = await fetch(`http://localhost:5224/customer/${itemName}`, {
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
    <>
      <div className="container-fluid">
        <div className="row flex-lg-nowrap">
          <div className="col">
            <div className="row">
              <div className="col mb-3">
                <div className="card">
                  <div className="card-body">
                    <div className="e-profile">
                      <div className="row">
                        <div className="col d-flex flex-column flex-sm-row justify-content-between mb-3">
                          <div className="text-center text-sm-left mb-2 mb-sm-0">
                            <h4 className="pt-sm-2 pb-1 mb-0 text-nowrap">{customer ? customer.name : 'John Smith'}</h4>
                            <p className="mb-0">{customer ? customer.email : '@johnny.s'}</p>

                          </div>
                        </div>
                      </div>
                      <ul className="nav nav-tabs">
                        <li className="nav-item"><a href="" className="active nav-link">Settings</a></li>
                      </ul>
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
                                        value={userInfo.name} // Set value from state
                                      />
                                      <p> User Info Name Test </p>
                                      <p>{userInfo.name}</p>
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
                                        value={userInfo.email} // Set value from state
                                      />
                                    </div>
                                  </div>
                                </div>
                                <div className="row">
                                  <div className="col">
                                    <div className="form-group">
                                      <label>Change Address</label>
                                      <input
                                        onChange={(e) => setUserInfo({ ...userInfo, address: e.target.value })}
                                        className="form-control"
                                        type="text"
                                        name="address"
                                        placeholder={customer ? customer.address : '1234 Street...'}
                                        value={userInfo.address} // Set value from state
                                      />
                                    </div>
                                  </div>
                                </div>
                              </div>
                              <div className="row">
                                <div className="col mb-3">
                                  <div className="mb-2"><b>Change Password</b></div>
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
                                        <input className="form-control" type="password" placeholder="••••••" />
                                      </div>
                                    </div>
                                  </div>
                                  <div className="row">
                                    <div className="col">
                                      <div className="form-group">
                                        <label>Current Password</label>
                                        <input onChange={(e) => setUserInfo({ ...userInfo, password: e.target.value })} className="form-control" type="password" placeholder="••••••" />
                                      </div>
                                    </div>
                                  </div>
                                </div>
                              </div>
                            </div>
                            <div className="row">
                              <div className="col d-flex justify-content-end">
                                <button onClick={updateUserInfo} className="btn btn-primary">Save Changes</button>
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

            {/* Omit Right side section */}
          </div>
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
                return (<Listing key={key} title={item.title} image={item.description} price={item.price} isAccountPage={true} productId={item.id} deleteProduct={deleteProduct} />)
              })}
            </>
          )
          }
        </div>
      </div>
    </>
  );
};

export default Account;