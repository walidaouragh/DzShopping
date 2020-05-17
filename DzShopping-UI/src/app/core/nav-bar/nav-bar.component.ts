import { Component, OnInit } from '@angular/core';
import { CartService } from 'src/app/modules/cart/cart.service';
import { Observable } from 'rxjs';
import { ICartItem, ICart } from 'src/app/shared/models/ICart';

@Component({
    selector: 'app-nav-bar',
    templateUrl: './nav-bar.component.html',
    styleUrls: ['./nav-bar.component.scss'],
})
export class NavBarComponent implements OnInit {
    constructor(private cartService: CartService) {}

    public cart$: Observable<ICart>;

    ngOnInit() {
        this.cart$ = this.cartService.cart$;
    }
}
