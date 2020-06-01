import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { ICart } from 'src/app/shared/models/ICart';
import { CartService } from '../../cart/cart.service';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-checkout-review',
    templateUrl: './checkout-review.component.html',
    styleUrls: ['./checkout-review.component.scss'],
})
export class CheckoutReviewComponent implements OnInit {
    constructor(private cartService: CartService, private toastr: ToastrService) {}

    public cart$: Observable<ICart>;

    ngOnInit() {
        this.cart$ = this.cartService.cart$;
    }

    public createPaymentIntent(): void {
        this.cartService.createPaymentIntent().subscribe(
            (response: any) => {
                this.toastr.success('Success');
            },
            (error) => {
                this.toastr.error(error.message);
                console.log(error);
            }
        );
    }
}
