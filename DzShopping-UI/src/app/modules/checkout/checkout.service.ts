import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { IDeliveryMethod } from 'src/app/shared/models/IDeliveryMethod';
import { IOrderToCreate } from 'src/app/shared/models/IOrderToCreate';

@Injectable({
    providedIn: 'root',
})
export class CheckoutService {
    constructor(private http: HttpClient) {}

    baseUrl = environment.apiUrl;

    public createOrder(order: IOrderToCreate) {
        return this.http.post(this.baseUrl + 'order', order);
    }

    public getDeliveryMethods() {
        return this.http.get(this.baseUrl + 'order/deliveryMethods').pipe(
            map((dm: IDeliveryMethod[]) => {
                // sort in highest price first
                return dm.sort((a, b) => b.price - a.price);
            })
        );
    }
}
