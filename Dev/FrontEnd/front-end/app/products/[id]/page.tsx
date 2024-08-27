"use client"

import {useEffect, useState, useContext} from 'react'
import {usePathname, useRouter} from 'next/navigation'
import productstyles from './page.module.css'
import { LoginContext } from '@/app/contexts/LoginContext'
import { CartContext } from '@/app/contexts/CartContext'
import { Form, Button, Col, Row, Container } from 'react-bootstrap';

function ListItem(){
    const {isSeller, user} = useContext(LoginContext)
    const {cart, setCart, cartId, setCartId} = useContext(CartContext)
    const path = usePathname()
    const router = useRouter();
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

    const getProducts = async () => {
        setLoading(true);
        const response = await fetch("http://localhost:5224/product");
        const data = await response.json();
        setLoading(false);

        return data;
      }
            
            

      const increment = () => {
        setCount(prevCount => prevCount + 1)
      }
      const decrement = () => {
        if(count >= 0){
        setCount(prevCount => prevCount - 1)
        }
      }
    

    const addProduct = async () => {
        console.log(cartId)
        let date = formatDate();
        console.log(date)
        let newcart = [...cart, prod]
        console.log(
            JSON.stringify({
                product: {
                    id: prod.id,
                    price: prod.price,
                    quantity: prod.quantity,
                     imageUrl: "n/a"
                },
                quantity: count,
                price: count * prod.price,
                orderId: cartId
                

                  

            }))
        


        const itemresponse = await fetch(`http://localhost:5224/item`, {
            method: 'POST',
            body: JSON.stringify({
                product: {
                    id: prod.id,
                    price: prod.price,
                    quantity: prod.quantity,
                    imageUrl: "n/a"
                },
                quantity: count,
                price: count * prod.price,
                orderId: cartId,
               

                  

            }),
            headers: {
                "Content-Type": "application/json"
            }
        })

        if(itemresponse.ok){
            const data = await itemresponse.json()
            console.log(data.id)
            


               const response = await fetch(`http://localhost:5224/order/${cartId}`, {
            method: 'PUT',
            body: JSON.stringify({
                id: cartId,
                customer: {
                    id: user.id,
                },
                items: [...cart, data],
                date: date,
                active: true,
                shippingAddress: user.address
            }),
            headers: {
                "Content-Type": "application/json"
            }
        })

        if(response.ok){
           console.log('Item added to cart successfully!')
           setCart([...cart, data])
        } else {
            console.error('Something went wrong.')
        }

     

 
    






        } else {
            console.error("Something went wrong")
        }




        
        

     

       
    }

    
    useEffect(() => {
        console.log(cart)
    }, [cart])


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

                <img src={productInfo.imageUrl} alt="product image"/>
                <div className={productstyles.info}>
                 <h2>{productInfo.title}</h2>
                 <p>{productInfo.description}</p>
                 <p>Price: {productInfo.price}</p>
                     <p>Available: {productInfo.quantity}</p>
                 <span className={productstyles.quantity}>
                   <button onClick={decrement}>-</button>
                   <input type="number" step="1" placeholder={count ? count : "Quantity"}/>
                   <button onClick={increment}>+</button>
                 </span>
                 <button onClick={addProduct} className={productstyles.cart}>Add To Cart</button>
                 <p onClick={() => router.push('/cart')}> Go To Cart</p>
                 </div>
                 </div>
                 

          
             </div>

        )}
       
        </>

    )

}

export default ListItem;