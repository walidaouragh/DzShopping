import { Component, OnInit } from '@angular/core';
import { AccountService } from '../account.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
    constructor(
        private accountService: AccountService,
        private router: Router,
        private activatedRoute: ActivatedRoute
    ) {}

    public loginForm: FormGroup;
    private returnUrl: string;

    ngOnInit() {
        this.returnUrl = this.activatedRoute.snapshot.queryParams.returnUrl || '/shop';
        this.createLoginForm();
    }

    public login() {
        this.accountService.login(this.loginForm.value).subscribe(
            () => {
                this.router.navigateByUrl(this.returnUrl);
            },
            (error) => {
                console.log(error);
            }
        );
    }

    private createLoginForm(): void {
        this.loginForm = new FormGroup({
            // validator.email is not 100% valid, used (http://regexlib.com/) liabrary.
            email: new FormControl('', [
                Validators.required,
                Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$'),
            ]),
            password: new FormControl('', Validators.required),
        });
    }
}
