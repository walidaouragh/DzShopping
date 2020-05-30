import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IOrder } from 'src/app/shared/models/IOrderToCreate';

@Injectable({
    providedIn: 'root',
})
export class OrdersService {
    constructor(private http: HttpClient) {}

    private baseUrl = environment.apiUrl;

    public getOrders() {
        return this.http.get<IOrder[]>(this.baseUrl + 'order');
    }

    public getOrder(orderId: number) {
        return this.http.get<IOrder>(this.baseUrl + `order/${orderId}`);
    }
}
