import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { ICart } from 'src/app/shared/models/ICart';
import { CartService } from '../../cart/cart.service';

@Component({
    selector: 'app-checkout-review',
    templateUrl: './checkout-review.component.html',
    styleUrls: ['./checkout-review.component.scss'],
})
export class CheckoutReviewComponent implements OnInit {
    constructor(private cartService: CartService) {}

    public cart$: Observable<ICart>;

    ngOnInit() {
        this.cart$ = this.cartService.cart$;
    }
}
