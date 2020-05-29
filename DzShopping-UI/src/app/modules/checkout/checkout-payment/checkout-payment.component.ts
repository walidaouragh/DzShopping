import { Component, OnInit, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { CartService } from '../../cart/cart.service';
import { CheckoutService } from '../checkout.service';
import { ToastrService } from 'ngx-toastr';
import { ICart } from 'src/app/shared/models/ICart';
import { Router, NavigationExtras } from '@angular/router';

@Component({
    selector: 'app-checkout-payment',
    templateUrl: './checkout-payment.component.html',
    styleUrls: ['./checkout-payment.component.scss'],
})
export class CheckoutPaymentComponent implements OnInit {
    constructor(
        private cartService: CartService,
        private checkoutService: CheckoutService,
        private toastr: ToastrService,
        private router: Router
    ) {}

    @Input() checkoutForm: FormGroup;

    ngOnInit() {}

    public submitOrder(): void {
        const cart = this.cartService.getCurrentCartValue();

        const orderToCreate = this.getOrderToCreate(cart);

        this.checkoutService.createOrder(orderToCreate).subscribe(
            () => {
                this.toastr.success('Order submitted');
                this.cartService.deleteLocalCartAfterSubmitOrder(cart.cartId);

                const navigationExtras: NavigationExtras = { state: orderToCreate };
                this.router.navigate(['checkout/success'], navigationExtras);
            },

            (error) => {
                this.toastr.error(error.message);
                console.log(error);
            }
        );
    }
    private getOrderToCreate(cart: ICart) {
        return {
            cartId: cart.cartId,
            deliveryMethodId: +this.checkoutForm.get('deliveryForm').get('deliveryMethod').value,
            shippToAddress: this.checkoutForm.get('addressForm').value,
        };
    }
}
