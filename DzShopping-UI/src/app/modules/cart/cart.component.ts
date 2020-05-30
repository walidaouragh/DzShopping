import { Component, OnInit } from '@angular/core';
import { CartService } from './cart.service';
import { ICart, ICartItem, ICartTotals } from 'src/app/shared/models/ICart';
import { Observable } from 'rxjs';

@Component({
    selector: 'app-cart',
    templateUrl: './cart.component.html',
    styleUrls: ['./cart.component.scss'],
})
export class CartComponent implements OnInit {
    constructor(private cartService: CartService) {}

    public cart$: Observable<ICart>;
    public cartTotals$: Observable<ICartTotals>;

    ngOnInit() {
        this.cart$ = this.cartService.cart$;
        this.cartTotals$ = this.cartService.cartTotal$;
    }

    public removeCartItem(item: ICartItem) {
        this.cartService.removeItemFromCart(item);
    }

    public incrementItemQuantity(item: ICartItem) {
        this.cartService.incrementItemQuantity(item);
    }

    public decrementItemQuantity(item: ICartItem) {
        this.cartService.decrementItemQuantity(item);
    }
}
