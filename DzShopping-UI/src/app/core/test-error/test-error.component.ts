import { Component, OnInit, ErrorHandler } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Component({
    selector: 'app-test-error',
    templateUrl: './test-error.component.html',
    styleUrls: ['./test-error.component.css'],
})
export class TestErrorComponent implements OnInit {
    constructor(private http: HttpClient) {}

    public baseUrl = environment.apiUrl;
    public validationErrors: any;

    ngOnInit() {}

    get404Error() {
        this.http.get(this.baseUrl + 'genericProduct/5555').subscribe(
            (response: HttpErrorResponse) => {
                console.log(response);
            },
            (error) => {
                console.log(error);
            }
        );
    }

    get500Error() {
        this.http.get(this.baseUrl + 'buggy/serverError').subscribe(
            (response: HttpErrorResponse) => {
                console.log(response);
            },
            (error) => {
                console.log(error);
            }
        );
    }

    get400Error() {
        this.http.get(this.baseUrl + 'buggy/badRequest').subscribe(
            (response: HttpErrorResponse) => {
                console.log(response);
            },
            (error) => {
                console.log(error);
            }
        );
    }

    get400ValidationError() {
        this.http.get(this.baseUrl + 'genericProduct/blabla').subscribe(
            (response: HttpErrorResponse) => {
                console.log(response);
            },
            (error) => {
                console.log(error);
                this.validationErrors = error.errors;
            }
        );
    }
}
