import { Component, OnInit } from '@angular/core';
import { CartService } from './modules/cart/cart.service';
import { AccountService } from './modules/account/account.service';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
    constructor(private cartService: CartService, private accountService: AccountService) {}
    title = 'Dz Shopping';
    ngOnInit() {
        this.loadCart();
        this.loadCurrentUser();
    }

    public loadCart() {
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

    public loadCurrentUser() {
        const token = localStorage.getItem('token');

        this.accountService.loadCurrentUser(token).subscribe(
            () => {},
            (error) => {
                console.log(error);
            }
        );
    }
}
