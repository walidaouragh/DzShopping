import { Component, OnInit } from '@angular/core';
import { CartService } from 'src/app/modules/cart/cart.service';
import { Observable } from 'rxjs';
import { ICartItem, ICart } from 'src/app/shared/models/ICart';
import { IUser } from 'src/app/shared/models/IUser';
import { AccountService } from 'src/app/modules/account/account.service';

@Component({
    selector: 'app-nav-bar',
    templateUrl: './nav-bar.component.html',
    styleUrls: ['./nav-bar.component.scss'],
})
export class NavBarComponent implements OnInit {
    constructor(private cartService: CartService, private accountService: AccountService) {}

    public cart$: Observable<ICart>;
    public currentUser$: Observable<IUser>;

    ngOnInit() {
        this.cart$ = this.cartService.cart$;
        this.currentUser$ = this.accountService.currentUser$;
    }

    public logout() {
        this.accountService.logout();
    }
}
