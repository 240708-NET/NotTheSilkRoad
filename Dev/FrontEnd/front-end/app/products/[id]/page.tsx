"use client"
import {useEffect, useState, useContext} from 'react'
import {usePathname, useRouter} from 'next/navigation'
import productstyles from './page.module.css'
import { LoginContext } from '@/app/contexts/LoginContext'
import { CartContext } from '@/app/contexts/CartContext'

function ListItem(){
    const {user} = useContext(LoginContext)
    const {cart, setCart, cartId, setCartId} = useContext(CartContext)
    const path = usePathname()
    const router = useRouter();
    const [prod, setProd] = useState()
    const [loading, setLoading] = useState(true)
    const [count, setCount] = useState(0)

    useEffect(() => {
        let itemArray = path.split('/products/');
        let itemName = itemArray[1]
        console.log(itemName)

        const getItem = async () => {
        const products = await getProducts();

        const item = products.find(x => x.title == itemName);
        console.log(item)
        setProd(item)
        setLoading(false)
        }
        getItem();

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
   

  

   if(loading){
    return <h1>Loading...</h1>
   }
    
    return (
        <>
        {prod  && (
            <div className={productstyles.container}>
                <div className={productstyles.product}>
                <img src={prod.image}/>
                <div className={productstyles.info}>
                 <h2>{prod.title}</h2>
                 <p>{prod.description}</p>
                 <p>{prod.price}</p>
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