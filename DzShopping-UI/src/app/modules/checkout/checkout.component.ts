import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account/account.service';
import { IAddress } from 'src/app/shared/models/IAddress';
import { Observable } from 'rxjs';
import { ICartTotals } from 'src/app/shared/models/ICart';
import { CartService } from '../cart/cart.service';

@Component({
    selector: 'app-checkout',
    templateUrl: './checkout.component.html',
    styleUrls: ['./checkout.component.scss'],
})
export class CheckoutComponent implements OnInit {
    constructor(private fb: FormBuilder, private accountService: AccountService, private cartService: CartService) {}

    public checkoutForm: FormGroup;
    public cartTotals$: Observable<ICartTotals>;

    ngOnInit() {
        this.createCheckoutForm();
        this.getAddressFormValues();
        this.getDeliveryMethodValue();
        this.cartTotals$ = this.cartService.cartTotal$;
    }

    public createCheckoutForm(): void {
        this.checkoutForm = this.fb.group({
            addressForm: this.fb.group({
                firstName: [null, Validators.required],
                lastName: [null, Validators.required],
                street: [null, Validators.required],
                city: [null, Validators.required],
                state: [null, Validators.required],
                zipCode: [null, Validators.required],
            }),
            deliveryForm: this.fb.group({
                deliveryMethod: [null, Validators.required],
            }),
            paymentForm: this.fb.group({
                nameOnCard: [null, Validators.required],
            }),
        });
    }

    public getAddressFormValues(): void {
        this.accountService.getAddress().subscribe(
            (address: IAddress) => {
                if (address) {
                    this.checkoutForm.get('addressForm').patchValue(address);
                }
            },
            (error) => {
                console.log(error);
            }
        );
    }

    public getDeliveryMethodValue() {
        const cartId = localStorage.getItem('cart_id');
        const cart = this.cartService.getCurrentCartValue();

        if (cart.deliveryMethodId !== null) {
            this.checkoutForm.get('deliveryForm').get('deliveryMethod').patchValue(cart.deliveryMethodId.toString());
        }
    }
}
