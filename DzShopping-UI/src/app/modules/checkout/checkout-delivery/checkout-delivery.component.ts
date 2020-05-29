import { Component, OnInit, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { IDeliveryMethod } from 'src/app/shared/models/IDeliveryMethod';
import { CheckoutService } from '../checkout.service';
import { CartService } from '../../cart/cart.service';

@Component({
    selector: 'app-checkout-delivery',
    templateUrl: './checkout-delivery.component.html',
    styleUrls: ['./checkout-delivery.component.scss'],
})
export class CheckoutDeliveryComponent implements OnInit {
    constructor(private checkoutService: CheckoutService, private cartService: CartService) {}

    @Input() checkoutForm: FormGroup;
    public deliveryMethods: IDeliveryMethod[];

    ngOnInit() {
        this.checkoutService.getDeliveryMethods().subscribe(
            (dm: IDeliveryMethod[]) => {
                this.deliveryMethods = dm;
            },
            (error) => {
                console.log(error);
            }
        );
    }

    public setShippingPrice(deliveryMethod: IDeliveryMethod) {
        this.cartService.setShippingPrice(deliveryMethod);
    }
}
