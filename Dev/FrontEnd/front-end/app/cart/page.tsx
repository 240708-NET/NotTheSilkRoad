import React, { useState, useContext } from 'react';
import CartStyles from './Cart.module.css';

function Cart() {

  return (
    <div className={CartStyles.cart}>
      <p>Your Cart:</p>
    </div>
  );
}

export default Cart;