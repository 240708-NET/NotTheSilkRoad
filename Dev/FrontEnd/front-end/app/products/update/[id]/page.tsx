"use client"
import { useEffect, useState, useContext } from 'react'
import { usePathname, useRouter } from 'next/navigation'
import productstyles from './page.module.css'
import updatePageStyles from './page.module.css'
import { LoginContext } from '../../../contexts/LoginContext'
import { Form, Button, Col, Row, Container } from 'react-bootstrap';
import { products } from '@/components/Listing/listings.js'
import NavbarLogo from '@/components/NavbarLogo/NavbarLogo'
import WaveLoading from '@/components/Loading/WaveLoading'
import router from 'next/navigation'


function ListItem() {
  const { isSeller, user } = useContext(LoginContext)
  const path = usePathname()
  const [prod, setProd] = useState()
  const [loading, setLoading] = useState(true)
  const [count, setCount] = useState(0)
  const router = useRouter()

  const [productInfo, setProductInfo] = useState({
  })

  const updateProduct = async () => {
    setLoading(true);
    console.log("Update Product: ", productInfo.id);

    const response = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/product/${productInfo.id}`, {
      method: "PUT",

      body: JSON.stringify({
        id: productInfo.id,
        title: productInfo.title,
        price: productInfo.price,
        description: productInfo.description,
        imageUrl: productInfo.imageUrl,
        quantity: productInfo.quantity,
        seller: user,
        categories: [],
      }),

      headers: {
        "Content-Type": "application/json",
      },
    })
    setLoading(false);
  }

  useEffect(() => {

    let itemArray = path.split('/products/update/');
    let itemName = itemArray[1]
    itemName = itemName.replaceAll('%20', ' ');
    console.log("Item Array:");
    console.log(itemArray[1]);
    console.log("Item Name:");
    console.log(itemName)

    const testProd = async () => {

      setLoading(true);
      const response = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/product`);
      const products = await response.json();
      setLoading(false);

      console.log("Products:");
      console.log(products);

      const item = products.find((product: any) => product.title == itemName);
      console.log("Item Object Being Found: ");
      console.log(item);
      setProd(item);

      console.log("Prod: ");
      console.log(prod);

      setLoading(false);

      //This section needs to be here for some reason in order for the productInfo variable to be set properly with correct data
      setProductInfo({
        id: item ? item.id : 0,
        title: item ? item.title : '',
        price: item ? item.price : 0,
        description: item ? item.description : '',
        imageUrl: item ? item.imageUrl : '',
        quantity: item ? item.quantity : 0,
        seller: user,
        categories: item ? item.categories : [],
      })
    }
    testProd();

    console.log("Prod Item:");
    console.log(prod);

  }, [path])

  if (loading) {
    return <WaveLoading />
  }
  console.log("User: ");
  console.log(user);

  console.log("Products Test:");
  console.log(products);
  return (

    <div>
      <NavbarLogo />
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
                          <div onClick={() => router.back()} className={updatePageStyles.back}>
                                <div className="fas fa-long-arrow-alt-left me-2">
                                    <img src="/images/arrow-90deg-left.svg" alt="Arrow Back" />
                                    Go Back
                                </div>
                            </div>
                            <h2 className="pt-sm-2 pb-1 mb-0 text-nowrap">Edit Product</h2>
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
                                      <label>Change Product Title</label>
                                      <input
                                        onChange={(e) => {
                                          const value = e.target.value?.trim();
                                          if (value) {
                                            setProductInfo({ ...productInfo, title: value });
                                          }
                                          else {
                                            setProductInfo({ ...productInfo, title: prod.title });
                                          }
                                        }}
                                        className="form-control"
                                        type="text"
                                        name="title"
                                        placeholder={'Product Title'}// Set value from state
                                      />
                                    </div>
                                  </div>
                                </div>
                                <div className="row">
                                  <div className="col">
                                    <div className="form-group">
                                      <label>Change Product Description</label>
                                      <input
                                        onChange={(e) => {
                                          const value = e.target.value?.trim();
                                          if (value) {
                                            setProductInfo({ ...productInfo, description: value });
                                          }
                                          else {
                                            setProductInfo({ ...productInfo, description: prod.description });
                                          }
                                        }}
                                        className="form-control"
                                        type="text"
                                        name="description"
                                        placeholder={'Product Description'}// Set value from state
                                      />
                                    </div>
                                  </div>
                                </div>
                                <div className="row">
                                  <div className="col">
                                    <div className="form-group">
                                      <label>Change Price</label>
                                      <input
                                        onChange={(e) => {
                                          const value = e.target.value?.trim();
                                          if (value) {
                                            setProductInfo({ ...productInfo, price: value });
                                          }
                                          else {
                                            setProductInfo({ ...productInfo, price: prod.price });
                                          }
                                        }}
                                        className="form-control"
                                        type="number"
                                        name="price"
                                        placeholder={'Product Price'}// Set value from state
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
                                        <label>Change Quantity</label>
                                        <input onChange={(e) => {
                                          const value = e.target.value?.trim();
                                          if (value) {
                                            setProductInfo({ ...productInfo, quantity: value });
                                          }
                                          else {
                                            setProductInfo({ ...productInfo, quantity: prod.quantity });
                                          }
                                        }} className="form-control" type="number" placeholder={'Product Quantity'}/>
                                      </div>
                                    </div>
                                  </div>
                                  <div className="row">
                                    <div className="col">
                                      <div className="form-group">
                                        <label>Change Image URL</label>
                                        <input onChange={(e) => {
                                          const value = e.target.value?.trim();
                                          if (value) {
                                            setProductInfo({ ...productInfo, imageUrl: value });
                                          }
                                          else {
                                            setProductInfo({ ...productInfo, imageUrl: prod.imageUrl });
                                          }
                                        }} className="form-control" type="text" placeholder={'Product Image URL'}/>
                                      </div>
                                    </div>
                                  </div>
                                </div>
                              </div>
                            </div>
                            <div className="row">
                              <div className="col d-flex justify-content-end">
                                <button onClick={() => updateProduct()} className="btn btn-primary">Update Product</button>
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

          
          <div className='col'>
            <div className="container-fluid">
              <div className="row flex-lg-nowrap">
                <div className="col">
                  <div className="row">
                    <div className="col mb-3">
                      <div className="card">
                      <h2 style={{textAlign: 'center', padding:'1rem', backgroundColor:'grey'}}>Updated Product</h2>
                        <div className="card-body">
                          <div className="e-profile">
                            <div className="card-body">
                              <div className={productstyles.updateItem}>
                                <img className={updatePageStyles.imageTest} src={productInfo.imageUrl} alt="product image" />
                                <div className={productstyles.info}>
                                  <h2>{productInfo.title}</h2>
                                  <p>{productInfo.description}</p>
                                  <p>Price: ${productInfo.price}</p>
                                  <p>Available: {productInfo.quantity}</p>
                                  <span className={productstyles.quantity}>
                                    <button>-</button>
                                    <input type="number" step="1" placeholder="Quantity" />
                                    <button>+</button>
                                  </span>
                                  <button className={productstyles.cart}>Add To Cart</button>
                                </div>
                              </div>
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    )
}

export default ListItem;