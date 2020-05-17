import { Component, OnInit } from '@angular/core';
import { CartService } from './modules/cart/cart.service';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
    constructor(private cartService: CartService) {}
    title = 'Dz Shopping';
    ngOnInit() {
        const cartId = localStorage.getItem('cart_id');
        if (cartId) {
            this.cartService.getCart(cartId).subscribe(
                () => {},
                (error) => {
                    console.log(error);
                }
            );
        }
    }
}
