"use client"
import {useEffect, useState} from 'react'
import {usePathname, useRouter} from 'next/navigation'
import {products } from '@/components/Listing/listings.js'
function ListItem(){
    const path = usePathname()
    const [prod, setProd] = useState()
    const [loading, setLoading] = useState(true)

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
            <>
           
                <div>
                 <p>This is your item: {prod.title}</p>
                 <img src={prod.image}/>
                 <p>{prod.price}</p>
                 </div>

          
             </>

        )}
       
        </>

    )
}

export default ListItem;