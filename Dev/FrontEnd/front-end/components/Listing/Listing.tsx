import { useRouter } from 'next/navigation';
import listingstyles from './Listing.module.css'; // Keep your custom CSS for specific styles

function Listing({title, image, price, isAccountPage, productId, deleteProduct}: { title: string; image: string; price: number, isAccountPage: boolean, productId: string, deleteProduct: () => void }) {
  const router = useRouter();
  const formatter = new Intl.NumberFormat("en-US", {
    style: "currency",
    currency: "USD",
    maximumFractionDigits: 0,
  });

  const createRoute = (item: string) => {
    router.push(`/products/${item}`);
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
          <p className="text-white mb-0 small">x4</p> {/* Replace with dynamic quantity */}
        </div>
      </div>
      <img src={image} className="card-img-top" alt={title} />
      <div className="card-body">
        <div className="d-flex justify-content-between">
          <p className="small">
            <a href="#!" className="text-muted">
              Category
            </a>
          </p>
          <p className="small text-danger">
            <s>${formatter.format(price)}</s>
          </p>
        </div>
        <div className="d-flex justify-content-between mb-3">
          <h5 className="mb-0">{title}</h5> <h5 className="text-dark mb-0">{formatter.format(price)}</h5>
        </div>
        <div className="d-flex justify-content-between mb-2">
        </div>
        {!isAccountPage ? (<button className="btn btn-primary" onClick={() => createRoute(title)}>
          View
        </button>) : (<button className="btn btn-primary" onClick={() => deleteProduct(productId)}>
          Delete
        </button>)}
        
      </div>
    </div>
    </div>
  );
}

export default Listing;