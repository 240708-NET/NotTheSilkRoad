import { useRouter } from 'next/navigation';
import {useEffect, useState} from 'react'
import listingstyles from './Listing.module.css'; // Keep your custom CSS for specific styles
import { describe } from 'node:test';

function Listing({title, description, imageUrl, price, isAccountPage, productId, quantity, deleteProduct}: { title: string; description: string; imageUrl: string; price: number, isAccountPage: boolean, productId: number, quantity: number, deleteProduct: () => void }) {
  const router = useRouter();
  const [categories, setCategories] = useState([])
  const formatter = new Intl.NumberFormat("en-US", {
    style: "currency",
    currency: "USD",
    maximumFractionDigits: 0,
  });

  const createRoute = (item: string) => {
    router.push(`/products/${item}`);
  };


  useEffect(() => {
    getCategories();
  }, [])


  const getCategories = async () => {

    let list = [];

    const response = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/category`)

    if(response.ok){
      const data = await response.json();

      data.forEach(x => {
        let y = x.products;
        y.forEach( y => {
          if(y.id === productId){
            list.push(x.description)
          }
        })
      })

      console.log(list)
      setCategories(list)
    } else {
      console.log("Something went wrong")
    }
    
  }

  const createProductUpdateRoute = (item: string) => {
    router.push(`/products/update/${item}`);
  };


  return (
    <div className="card-deck">
    <div className={`card ${listingstyles.card}`}>  {/* Combine custom & Bootstrap classes */}
      <div className="d-flex justify-content-between p-3">
        <p className="lead mb-0">{title}</p>
        <div
          className="bg-info rounded-circle d-flex align-items-center justify-content-center shadow-1-strong"
          style={{ width: 35, height: 35 }}
        >
          <p className="text-white mb-0 small">x{quantity}</p> {/* Replace with dynamic quantity */}
        </div>
      </div>
      <img src={imageUrl} className="card-img-top" alt={title} />
      <div className="card-body">
        <div className="d-flex justify-content-between">
          <p className="small">


            {categories.length > 0 ? <p className="small">Categories: <div>{categories}</div></p> : <p></p>}


            <a href="#!" className="text-muted">
              Category
            </a>
           

          </p>
          <p className="small text-danger">
            <s>${formatter.format(price)}</s>
          </p>
        </div>
        <div className="d-flex justify-content-between mb-3">
          <h5 className="mb-0">{description}</h5> <h5 className="text-dark mb-0">{formatter.format(price)}</h5>
        </div>
        <div className="d-flex justify-content-between mb-2">
        </div>
    
        <div className={listingstyles.buttonContainer}>
  {isAccountPage && <button className="btn btn-primary" onClick={() => createProductUpdateRoute(title)}>
    Edit Listing
  </button>}

  {!isAccountPage ? (<button className="btn btn-primary" onClick={() => createRoute(title)}>
    View
  </button>) : (<button className="btn btn-primary" onClick={() => deleteProduct(productId)}>
    Delete
  </button>
  )}
</div>

        
        
      </div>
    </div>
    </div>
  );
}

export default Listing;