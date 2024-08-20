"use client"
import {useEffect, useState} from 'react'
import {usePathname, useRouter} from 'next/navigation'
import {products } from '@/components/Listing/listings.js'
function ListItem(){
    const path = usePathname()
    const [prod, setProd] = useState()

    useEffect(() => {
        console.log(products)
        let itemArray = path.split('/products/');
        let itemName = itemArray[1]
        console.log(itemName)

        const item = products.filter(x => x.title == itemName);
        console.log(item)
        setProd(item)

    }, [path])
   

  

   
    
    return (
        <>
        {prod  && (
            <>
            {prod.map((item, key) => {
                <div key={key}>
                 <p>This is your item: {item.title}</p>
                 <img src={item.image}/>
                 <p>{item.price}</p>
                 </div>

            })}
            
             </>

        )}
       
        </>

    )
}

export default ListItem;