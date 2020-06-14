import { Component, Input, AfterViewInit, ViewChild, ElementRef, OnDestroy } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { CartService } from '../../cart/cart.service';
import { CheckoutService } from '../checkout.service';
import { ToastrService } from 'ngx-toastr';
import { ICart } from 'src/app/shared/models/ICart';
import { Router, NavigationExtras } from '@angular/router';

// To avoid stripe error;
declare var Stripe;

@Component({
    selector: 'app-checkout-payment',
    templateUrl: './checkout-payment.component.html',
    styleUrls: ['./checkout-payment.component.scss'],
})
export class CheckoutPaymentComponent implements AfterViewInit, OnDestroy {
    constructor(
        private cartService: CartService,
        private checkoutService: CheckoutService,
        private toastr: ToastrService,
        private router: Router
    ) {}

    @Input() checkoutForm: FormGroup;
    @ViewChild('cardNumber', { static: true }) cardNumberElement: ElementRef;
    @ViewChild('cardExpiry', { static: true }) cardExpiryElement: ElementRef;
    @ViewChild('cardCvc', { static: true }) cardCvcElement: ElementRef;

    public stripe: any;
    public cardNumber: any;
    public cardExpiry: any;
    public cardCvc: any;
    public cardErrors: any;
    public cardHandler = this.onChange.bind(this);
    public loading = false;
    public cardNumberValid = false;
    public cardExpiryValid = false;
    public cardCvcValid = false;

    ngAfterViewInit(): void {
        // this key from stripe => Developers => API keys => Publishable key
        this.stripe = Stripe('pk_test_afqqHiazsfByqXLiNmv3ziP600RD9LjYkQ');
        const elements = this.stripe.elements();

        this.cardNumber = elements.create('cardNumber');
        this.cardNumber.mount(this.cardNumberElement.nativeElement);
        this.cardNumber.addEventListener('change', this.cardHandler);

        this.cardExpiry = elements.create('cardExpiry');
        this.cardExpiry.mount(this.cardExpiryElement.nativeElement);
        this.cardExpiry.addEventListener('change', this.cardHandler);

        this.cardCvc = elements.create('cardCvc');
        this.cardCvc.mount(this.cardCvcElement.nativeElement);
        this.cardCvc.addEventListener('change', this.cardHandler);
    }

    public ngOnDestroy() {
        this.cardNumber.destroy();
        this.cardExpiry.destroy();
        this.cardCvc.destroy();
    }

    public onChange(event) {
        if (event.error) {
            this.cardErrors = event.error.message;
        } else {
            this.cardErrors = null;
        }

        switch (event.elementType) {
            case 'cardNumber':
                this.cardNumberValid = event.complete;
                break;
            case 'cardExpiry':
                this.cardExpiryValid = event.complete;
                break;
            case 'cardCvc':
                this.cardCvcValid = event.complete;
                break;
        }
    }

    public async submitOrder() {
        this.loading = true;
        const cart = this.cartService.getCurrentCartValue();

        try {
            const createdOrder = await this.createOrder(cart);
            const PaymentResult = await this.confirmPaymentWithStripe(cart);

            if (PaymentResult.paymentIntent) {
                this.cartService.deleteCart(cart);

                const navigationExtras: NavigationExtras = { state: createdOrder };
                this.router.navigate(['checkout/success'], navigationExtras);
            } else {
                this.toastr.error(PaymentResult.error.message);
            }
            this.loading = false;
        } catch (error) {
            console.log(error);
            this.loading = false;
        }
    }

    private getOrderToCreate(cart: ICart) {
        return {
            cartId: cart.cartId,
            deliveryMethodId: +this.checkoutForm.get('deliveryForm').get('deliveryMethod').value,
            shippToAddress: this.checkoutForm.get('addressForm').value,
        };
    }

    private async createOrder(cart: ICart) {
        const orderToCreate = this.getOrderToCreate(cart);
        return this.checkoutService.createOrder(orderToCreate).toPromise();
    }

    private async confirmPaymentWithStripe(cart: ICart) {
        return this.stripe.confirmCardPayment(cart.clientSecret, {
            payment_method: {
                card: this.cardNumber,
                billing_details: {
                    name: this.checkoutForm.get('paymentForm').get('nameOnCard').value,
                },
            },
        });
    }
}
