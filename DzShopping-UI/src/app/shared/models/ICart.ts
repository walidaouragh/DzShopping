import { v4 as uuidv4 } from 'uuid';

export interface ICart {
    cartId: string;
    cartItems: ICartItem[];
}

export interface ICartItem {
    cartItemId: number;
    productName: string;
    price: number;
    quantity: number;
    pictureUrl: string;
    productBrand: string;
    productType: string;
}

export interface ICartTotals {
    shipping: number;
    subtotal: number;
    total: number;
}

export class Cart implements ICart {
    // npm i uuid (check redis)
    // To create a unique identifier for the cart
    cartId = uuidv4();
    cartItems: ICartItem[] = [];
}
