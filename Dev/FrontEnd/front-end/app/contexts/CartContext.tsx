"use client"
import { createContext, useState } from "react";

const CartContext = createContext({
    cart: [],
    cartId: 0,
    setCart: () => {},
    setCartId: () => {}
});

const CartProvider = ({ children }: any) => {
    const [cart, setCart] = useState([]);
    const [cartId, setCartId] = useState(0)
   

    return (
        <CartContext.Provider value={{cart, setCart, cartId, setCartId}}>
            {children}
        </CartContext.Provider>
    )
}

export { CartContext, CartProvider }