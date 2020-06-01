import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';
import { ICart, ICartItem, Cart, ICartTotals } from 'src/app/shared/models/ICart';
import { map } from 'rxjs/operators';
import { IProduct } from 'src/app/shared/models/IProduct';
import { IDeliveryMethod } from 'src/app/shared/models/IDeliveryMethod';

@Injectable({
    providedIn: 'root',
})
export class CartService {
    constructor(private http: HttpClient) {}
    public baseUrl = environment.apiUrl;

    private cartSource = new BehaviorSubject<ICart>(null);
    public cart$ = this.cartSource.asObservable();

    private cartTotalSource = new BehaviorSubject<ICartTotals>(null);
    public cartTotal$ = this.cartTotalSource.asObservable();

    public shipping = 0;

    public getCart(cartId: string) {
        return this.http.get<ICart>(this.baseUrl + `cart?cartId=${cartId}`).pipe(
            map((cart: ICart) => {
                this.cartSource.next(cart);
                this.shipping = cart.shippingPrice;
                this.calculateTotals();
            })
        );
    }

    public setCart(cart: ICart) {
        return this.http.post<ICart>(this.baseUrl + 'cart', cart).subscribe(
            (response: ICart) => {
                this.cartSource.next(response);
                this.calculateTotals();
            },
            (error) => {
                console.log(error);
            }
        );
    }

    public removeItemFromCart(item: ICartItem) {
        const cart = this.getCurrentCartValue();
        if (cart.cartItems.some((x) => x.cartItemId === item.cartItemId)) {
            cart.cartItems = cart.cartItems.filter((i) => i.cartItemId !== item.cartItemId);
            if (cart.cartItems.length > 0) {
                this.setCart(cart);
            } else {
                this.deleteCart(cart);
            }
        }
    }

    public getCurrentCartValue() {
        return this.cartSource.value;
    }

    public addItemToCart(item: IProduct, quantity = 1) {
        const itemToAdd: ICartItem = this.mapProductItemToCartItem(item, quantity);
        let cart = this.getCurrentCartValue();
        if (cart === null) {
            cart = this.createCart();
        }

        cart.cartItems = this.addOrUpdateItem(cart.cartItems, itemToAdd, quantity);
        this.setCart(cart);
    }

    public incrementItemQuantity(item: ICartItem) {
        const cart = this.getCurrentCartValue();
        const foundItemIndex = cart.cartItems.findIndex((x) => x.cartItemId === item.cartItemId);
        cart.cartItems[foundItemIndex].quantity++;
        this.setCart(cart);
    }

    public decrementItemQuantity(item: ICartItem) {
        const cart = this.getCurrentCartValue();
        const foundItemIndex = cart.cartItems.findIndex((x) => x.cartItemId === item.cartItemId);
        if (cart.cartItems[foundItemIndex].quantity > 1) {
            cart.cartItems[foundItemIndex].quantity--;
            this.setCart(cart);
        } else {
            this.removeItemFromCart(item);
        }
    }

    public deleteCart(cart: ICart) {
        return this.http.delete<ICartItem>(this.baseUrl + `cart?cartId=${cart.cartId}`).subscribe(
            () => {
                this.cartSource.next(null);
                this.cartTotalSource.next(null);
                localStorage.removeItem('cart_id');
            },
            (error) => {
                console.log(error);
            }
        );
    }

    public deleteLocalCartAfterSubmitOrder(cartId: string): void {
        this.cartSource.next(null);
        this.cartTotalSource.next(null);
        localStorage.removeItem('cart_id');
    }

    public setShippingPrice(deliveryMethod: IDeliveryMethod) {
        this.shipping = deliveryMethod.price;
        const cart = this.getCurrentCartValue();
        cart.deliveryMethodId = deliveryMethod.id;
        cart.shippingPrice = deliveryMethod.price;
        this.calculateTotals();
        this.setCart(cart);
    }

    public createPaymentIntent() {
        return this.http.post(this.baseUrl + `payments/${this.getCurrentCartValue().cartId}`, {}).pipe(
            map((cart: ICart) => {
                this.cartSource.next(cart);
            })
        );
    }

    private mapProductItemToCartItem(item: IProduct, quantity: number): ICartItem {
        return {
            cartItemId: item.productId,
            productName: item.productName,
            price: item.price,
            pictureUrl: item.pictureUrl,

            productBrand: item.productBrand,
            productType: item.productBrand,
            quantity,
        };
    }

    private createCart(): ICart {
        const cart = new Cart();
        localStorage.setItem('cart_id', cart.cartId);
        return cart;
    }

    private addOrUpdateItem(cartItems: ICartItem[], itemToAdd: ICartItem, quantity: number): ICartItem[] {
        const index = cartItems.findIndex((i) => i.cartItemId === itemToAdd.cartItemId);
        if (index === -1) {
            itemToAdd.quantity = quantity;
            cartItems.push(itemToAdd);
        } else {
            cartItems[index].quantity += quantity;
        }

        return cartItems;
    }

    private calculateTotals() {
        const cart = this.getCurrentCartValue();
        const shipping = this.shipping;
        const subtotal = cart.cartItems.reduce((a, b) => b.price * b.quantity + a, 0);
        const total = subtotal + shipping;
        this.cartTotalSource.next({ shipping, subtotal, total });
    }
}
