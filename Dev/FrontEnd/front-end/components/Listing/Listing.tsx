import listingstyles from './Listing.module.css'
import { useRouter } from 'next/navigation';

function Listing({ title, image, price}: { title: string, image: string, price: number}){

    const router = useRouter();
   

    const formatter = new Intl.NumberFormat("en-US", {
        style: "currency",
        currency: "USD",
        maximumFractionDigits: 0,
      });

      const createRoute = (item : string) => {
        router.push(`/products/${item}`)
      }

    return (
        <div className={listingstyles.card}>
            <img src={image}/>
            <span>
                <p>{title}</p>
                <p id={listingstyles.price}>{formatter.format(price)}</p>
            </span>
            <button onClick={()=> {createRoute(title)}}>View</button>

        </div>
    )

}

export default Listing;