"use client"
import {useEffect, useState} from 'react'
import {usePathname, useRouter} from 'next/navigation'
import {products } from '@/components/Listing/listings.js'
import productstyles from './page.module.css'

function ListItem(){
    const path = usePathname()
    const [prod, setProd] = useState()
    const [loading, setLoading] = useState(true)
    const [count, setCount] = useState(0)

    useEffect(() => {
        console.log(products)
        let itemArray = path.split('/products/');
        let itemName = itemArray[1]
        console.log(itemName)

        const item = products.filter(x => x.title == itemName);
        console.log(item[0])
        setProd(item[0])
        setLoading(false)

    }, [path])
   

  

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