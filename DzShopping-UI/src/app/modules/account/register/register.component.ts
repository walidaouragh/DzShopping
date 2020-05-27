import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder, AsyncValidatorFn } from '@angular/forms';
import { AccountService } from '../account.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { timer, of } from 'rxjs';
import { switchMap, map } from 'rxjs/operators';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
    constructor(
        private accountService: AccountService,
        private router: Router,
        private toastr: ToastrService,
        private fb: FormBuilder
    ) {}

    public registerForm: FormGroup;

    ngOnInit() {
        this.CreateRegisterForm();
    }

    public register() {
        this.accountService.register(this.registerForm.value).subscribe(
            () => {
                this.router.navigateByUrl('/shop');
            },
            (error) => {
                this.toastr.error(error.errors), console.log(error);
            }
        );
    }

    private CreateRegisterForm(): void {
        this.registerForm = this.fb.group({
            displayName: [null, Validators.required],
            // validator.email is not 100% valid, used (http://regexlib.com/) liabrary.
            email: [
                null,
                [Validators.required, Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')],
                [this.validateEmailExists()],
            ],
            password: [null, Validators.required],
        });
    }

    private validateEmailExists(): AsyncValidatorFn {
        return (control) => {
            return timer(500).pipe(
                switchMap(() => {
                    if (!control.value) {
                        return of(null);
                    }
                    return this.accountService.checkEmailExists(control.value).pipe(
                        map((res) => {
                            return res ? { emailExists: true } : null;
                        })
                    );
                })
            );
        };
    }
}
