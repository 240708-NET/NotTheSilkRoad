import React from 'react';
import RegistrationFormStyles from './RegistrationForm.module.css';

import { useContext, useState } from 'react';
import { LoginContext } from '@/app/contexts/LoginContext';
import { useRouter } from 'next/navigation';


const RegistrationForm = () => {

    const { user, setUser, isSeller, setIsSeller } = useContext(LoginContext);
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [name, setName] = useState("");
    const [address, setAddress] = useState("");


    const createUser = async () => {
        if(isSeller) {
            const response = fetch('http://localhost:5224/seller', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    email, password, name
                })
            })
        }
        else
        {
            const response = fetch('http://localhost:5224/customer', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    email, password, name, address
                })
            })
        }
        
    }

    return (
        <div className={RegistrationFormStyles.registrationForm}>
            <a href="/" className="navbar-brand">
        <img src="images/NotTheSilkRoadLogo.png" alt="Logo" className={RegistrationFormStyles.logo} style={{ height: '100%', objectFit: 'contain' }} />
      </a>
            <section className="d-flex justify-content-center align-items-center h-100">
                <div className="card card-registration my-4 p-4">
                    <h3 className="mb-4 text-uppercase">User Registration Form</h3>

                    <div className="form-outline mb-3">
                        <input
                            required
                            type="text"
                            id="form3Example97"
                            className="form-control form-control-lg"
                            placeholder="Full Name"
                            onChange={(e) => setName(e.target.value)}
                        />
                        <label className="form-label" htmlFor="form3Example97">
                            Full Name
                        </label>
                    </div>

                    <div className="form-outline mb-3">
                        <input
                            required
                            type="email"
                            id="form3Example97"
                            className="form-control form-control-lg"
                            placeholder="Email"
                            onChange={(e) => setEmail(e.target.value)}
                        />
                        <label className="form-label" htmlFor="form3Example97">
                            Email
                        </label>
                    </div>

                    <div className="form-outline mb-3">
                        <input
                            required
                            type="password"
                            id="form3Example10"
                            className="form-control form-control-lg"
                            placeholder="Password"
                            onChange={(e) => setPassword(e.target.value)}
                        />
                        <label className="form-label" htmlFor="form3Example10">
                            Password
                        </label>
                    </div>

                    <div className="form-outline mb-3">
                        <input
                            required
                            type="password"
                            id="form3Example100"
                            className="form-control form-control-lg"
                            placeholder="Repeat Password"
                        />
                        <label className="form-label" htmlFor="form3Example100">
                            Repeat Password
                        </label>
                    </div>

                    {!isSeller && 

                    <div className="form-outline mb-3">
                        <input
                            required
                            type="text"
                            id="form3Example8"
                            className="form-control form-control-lg"
                            placeholder="Street Address"
                            onChange={(e) => setAddress(e.target.value)}
                        />
                        <label className="form-label" htmlFor="form3Example8">
                            Street Address
                        </label>
                    </div>}
                    

                    <div className="d-flex justify-content-start align-items-center mb-3">
                        <h6 className="mb-0 me-4">Role:</h6>

                        <div className="form-check form-check-inline mb-0">
                            <input
                                className="form-check-input"
                                type="radio"
                                name="role"
                                id="customerRole"
                                value="customer"
                                defaultChecked
                                onChange={() => setIsSeller(false)}
                            />
                            <label className="form-check-label" htmlFor="customerRole">
                                Customer
                            </label>
                        </div>

                        <div className="form-check form-check-inline mb-0 me-4">
                            <input
                                className="form-check-input"
                                type="radio"
                                name="role"
                                id="sellerRole"
                                value="seller"
                                onChange={() => setIsSeller(true)}
                            />
                            <label className="form-check-label" htmlFor="sellerRole">
                                Seller
                            </label>
                        </div>

                    </div>

                    <div className="d-flex justify-content-center">
                        <button
                            type="button"
                            className="btn btn-warning btn-lg"
                            data-mdb-ripple-init=""
                            data-mdb-button-init=""
                            onClick={() => createUser()}
                        >
                            Create Account
                        </button>
                    </div>
                </div>

            </section>
        </div>
    );
};

export default RegistrationForm;