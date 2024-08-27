"use client"
import { useEffect, useState, useContext } from 'react'
import { usePathname, useRouter } from 'next/navigation'
import productstyles from './page.module.css'
import { LoginContext } from '../../contexts/LoginContext'
import { Form, Button, Col, Row, Container } from 'react-bootstrap';
import { products } from '@/components/Listing/listings.js'

function ListItem() {

    const { isSeller, user } = useContext(LoginContext)
    const path = usePathname()
    const [prod, setProd] = useState()
    const [loading, setLoading] = useState(true)
    const [count, setCount] = useState(0)


    const [productInfo, setProductInfo] = useState({
    })

    const updateProduct = async () => {
        setLoading(true);
        console.log("Update Product: ", productInfo.id);

        const response = await fetch(`http://localhost:5224/product/${productInfo.id}`, {
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

        let itemArray = path.split('/products/');
        let itemName = itemArray[1]
        itemName = itemName.replaceAll(/%20/g, " ")
        console.log("Item Name:");
        console.log(itemName)

        const testProd = async () => {

            setLoading(true);
            const response = await fetch("http://localhost:5224/product");
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
        return <h1>Loading...</h1>
    }
    console.log("User: ");
    console.log(user);

    console.log("Products Test:");
    console.log(products);
    return (

        <div>
            <div className={productstyles.container}>
                <div className={productstyles.product}>
                    <img src={productInfo.imageUrl} alt="product image" />
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
        </div>)
}


export default ListItem;