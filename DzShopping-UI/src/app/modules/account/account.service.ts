import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IAddress } from 'src/app/shared/models/IAddress';
import { Observable, of, ReplaySubject } from 'rxjs';
import { IUser } from 'src/app/shared/models/IUser';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable({
    providedIn: 'root',
})
export class AccountService {
    constructor(private http: HttpClient, private router: Router) {}

    public baseUrl = environment.apiUrl;
    // Didn't use BehaviorSubject because I don't need to emit an initial value in authGuard.
    private currentUserSource = new ReplaySubject<IUser>(1);
    public currentUser$ = this.currentUserSource.asObservable();

    public login(values: any) {
        return this.http.post(this.baseUrl + 'account/login', values).pipe(
            map((user: IUser) => {
                if (user) {
                    localStorage.setItem('token', user.token);
                    this.currentUserSource.next(user);
                }
            })
        );
    }

    public register(values: any) {
        return this.http.post(this.baseUrl + 'account/register', values).pipe(
            map((user: IUser) => {
                if (user) {
                    localStorage.setItem('token', user.token);
                    this.currentUserSource.next(user);
                }
            })
        );
    }

    public logout() {
        localStorage.removeItem('token');
        this.currentUserSource.next(null);
        this.router.navigateByUrl('/');
    }

    public loadCurrentUser(token: string) {
        if (token === null) {
            this.currentUserSource.next(null);
            return of(null);
        }
        let headers = new HttpHeaders();
        headers = headers.set('Authorization', `Bearer ${token}`);
        return this.http.get(this.baseUrl + 'account', { headers }).pipe(
            map((user: IUser) => {
                if (user) {
                    localStorage.setItem('token', user.token);
                    this.currentUserSource.next(user);
                }
            })
        );
    }

    public getAddress(): Observable<any> {
        return this.http.get<IAddress>(this.baseUrl + 'account/address');
    }

    public updateAddress(address: IAddress): Observable<any> {
        return this.http.put<IAddress>(this.baseUrl + 'account/address', address);
    }

    public checkEmailExists(email: string): Observable<any> {
        return this.http.get<string>(this.baseUrl + `account/emailexists?email=${email}`);
    }
}
