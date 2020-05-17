import { Component, OnInit } from '@angular/core';
import { CartService } from 'src/app/modules/cart/cart.service';
import { Observable } from 'rxjs';
import { ICartTotals } from '../../models/ICart';

@Component({
    selector: 'app-order-totals',
    templateUrl: './order-totals.component.html',
    styleUrls: ['./order-totals.component.scss'],
})
export class OrderTotalsComponent implements OnInit {
    constructor(private cartService: CartService) {}

    public cartTotal$: Observable<ICartTotals>;

    ngOnInit() {
        this.cartTotal$ = this.cartService.cartTotal$;
    }
}
