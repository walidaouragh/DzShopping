import { Component, OnInit, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { AccountService } from '../../account/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-checkout-address',
    templateUrl: './checkout-address.component.html',
    styleUrls: ['./checkout-address.component.scss'],
})
export class CheckoutAddressComponent implements OnInit {
    constructor(private accountService: AccountService, private toastr: ToastrService) {}

    @Input() checkoutForm: FormGroup;

    ngOnInit() {}

    public saveUserAddress() {
        this.accountService.updateAddress(this.checkoutForm.get('addressForm').value).subscribe(
            () => {
                this.toastr.success('Address updated');
            },

            (error) => {
                this.toastr.error(error.message);
                console.log(error);
            }
        );
    }
}
