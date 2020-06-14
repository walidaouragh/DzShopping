import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { ICart } from 'src/app/shared/models/ICart';
import { CartService } from '../../cart/cart.service';
import { ToastrService } from 'ngx-toastr';
import { CdkStepper } from '@angular/cdk/stepper';

@Component({
    selector: 'app-checkout-review',
    templateUrl: './checkout-review.component.html',
    styleUrls: ['./checkout-review.component.scss'],
})
export class CheckoutReviewComponent implements OnInit {
    constructor(private cartService: CartService, private toastr: ToastrService) {}

    @Input() appStepper: CdkStepper;
    public cart$: Observable<ICart>;

    ngOnInit() {
        this.cart$ = this.cartService.cart$;
    }

    public createPaymentIntent(): void {
        this.cartService.createPaymentIntent().subscribe(
            () => {
                this.appStepper.next();
            },
            (error) => {
                console.log(error);
            }
        );
    }
}
