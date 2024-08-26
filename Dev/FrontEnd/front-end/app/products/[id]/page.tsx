"use client"
import {useEffect, useState} from 'react'
import {usePathname, useRouter} from 'next/navigation'
import productstyles from './page.module.css'

function ListItem(){
    const path = usePathname()
    const [prod, setProd] = useState()
    const [loading, setLoading] = useState(true)
    const [count, setCount] = useState(0)

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

   if(loading){
    return <h1>Loading...</h1>
   }
   
    return (
        <>
        {prod  && (
            <div className={productstyles.container}>
                <div className={productstyles.product}>
                <img src={prod.description}/>
                <div className={productstyles.info}>
                 <h2>{prod.title}</h2>
                 <p>{prod.description}</p>
                 <p>{prod.price}</p>
                 <span className={productstyles.quantity}>
                   <button>-</button>
                   <input type="number" step="1" placeholder="Quantity"/>
                   <button>+</button>
                 </span>
                 <button className={productstyles.cart}>Add To Cart</button>
                 </div>
                 </div>
                 

          
             </div>

        )}
       
        </>

    )
}

export default ListItem;