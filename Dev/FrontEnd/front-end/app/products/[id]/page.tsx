"use client"
import { useEffect, useState, useContext } from 'react'
import { usePathname, useRouter } from 'next/navigation'
import productstyles from './page.module.css'
import { LoginContext } from '../../contexts/LoginContext'
import { Form, Button, Col, Row, Container } from 'react-bootstrap';

function ListItem() {
    
    const { isSeller, user } = useContext(LoginContext)
    const path = usePathname()
    const [prod, setProd] = useState()
    const [loading, setLoading] = useState(true)
    const [count, setCount] = useState(0)


    const [productInfo, setProductInfo] = useState({
        id: prod ? prod.id : 0,
        title: prod ? prod.title : '',
        price: prod ? prod.price : 0,
        description: prod ? prod.description : '',
        imageUrl: prod ? prod.imageUrl : '',
        quantity: prod ? prod.quantity : 0,
        seller: user,
        categories: prod ? prod.categories : [],
      })

      const updateProduct = async () => {
        setLoading(true);

        const response = await fetch(`http://localhost:5224/product/62`, {
          method: "PUT",
    
          body: JSON.stringify({
            id: 62,
            title: "Edit Title",
            price: 300,
            description: "New Desc",
            imageUrl: "https://img-cdn.pixlr.com/image-generator/history/65bb506dcb310754719cf81f/ede935de-1138-4f66-8ed7-44bd16efc709/medium.webp",
            quantity: 10,
            seller: user,
            categories: [],
          }),
    
          headers: {
            "Content-Type": "application/json",
          },
        })
        setLoading(false);
      }

    const getProducts = async () => {
        setLoading(true);
        const response = await fetch("http://localhost:5224/product");
        const data = await response.json();
        console.log(data);
        setLoading(false);
        return data;
    }

    useEffect(() => {

        let itemArray = path.split('/products/');
        let itemName = itemArray[1]
        console.log(itemName)

        const getProductsTest = async () => {
            const products = await getProducts();

            console.log("Products:");
            console.log(products);

            const item = products.find((product: any) => product.title == itemName);
            console.log(item);
            setProd(item);
            setLoading(false);
        }

        getProductsTest();


    }, [path])

    if (loading) {
        return <h1>Loading...</h1>
    }

    return (
        <div>
            {isSeller ?
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
                                    <h4 className="pt-sm-2 pb-1 mb-0 text-nowrap">{prod ? prod.title : 'Product Title'}</h4>
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
                                                onChange={(e) => setProductInfo({ ...productInfo, title: e.target.value })}
                                                className="form-control"
                                                type="text"
                                                name="title"
                                                placeholder={prod ? prod.title : 'Product Title'}
                                                value={productInfo.title} // Set value from state
                                              />
                                            </div>
                                          </div>
                                        </div>
                                        <div className="row">
                                          <div className="col">
                                            <div className="form-group">
                                              <label>Change Product Description</label>
                                              <input
                                                onChange={(e) => setProductInfo({ ...productInfo, description: e.target.value })}
                                                className="form-control"
                                                type="text"
                                                name="description"
                                                placeholder={prod ? prod.description : 'Product Description'}
                                                value={productInfo.description} // Set value from state
                                              />
                                            </div>
                                          </div>
                                        </div>
                                        <div className="row">
                                          <div className="col">
                                            <div className="form-group">
                                              <label>Change Price</label>
                                              <input
                                                onChange={(e) => setProductInfo({ ...productInfo, price: e.target.value })}
                                                className="form-control"
                                                type="number"
                                                name="price"
                                                placeholder={prod ? prod.price : 'Product Price'}
                                                value={productInfo.price} // Set value from state
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
                                                <input onChange={(e) => setProductInfo({ ...productInfo, quantity: e.target.value })} className="form-control" type="number" placeholder={prod ? "x" + prod.quantity : 'Product Quantity'} />
                                              </div>
                                            </div>
                                          </div>
                                          <div className="row">
                                            <div className="col">
                                              <div className="form-group">
                                                <label>Change Image URL</label>
                                                <input onChange={(e) => setProductInfo({ ...productInfo, imageUrl: e.target.value })} className="form-control" type="text" placeholder="Enter Image URL" />
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
                    </div>
                </div>
                :
                (<div className={productstyles.container}>
                    <div className={productstyles.product}>
                        <img src={prod.imageUrl} alt="product image" />
                        <div className={productstyles.info}>
                            <h2>{prod.title}</h2>
                            <p>{prod.description}</p>
                            <p>Price: ${prod.price}</p>
                            <p>Available: {prod.quantity}</p>
                            <span className={productstyles.quantity}>
                                <button>-</button>
                                <input type="number" step="1" placeholder="Quantity" />
                                <button>+</button>
                            </span>
                            <button className={productstyles.cart}>Add To Cart</button>
                        </div>
                    </div>
                </div>
                )}
        </div>)
}

export default ListItem;