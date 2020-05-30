import { Component, OnInit } from '@angular/core';
import { OrdersService } from '../orders.service';
import { IOrder } from 'src/app/shared/models/IOrderToCreate';
import { Router } from '@angular/router';

@Component({
    selector: 'app-orders',
    templateUrl: './orders.component.html',
    styleUrls: ['./orders.component.scss'],
})
export class OrdersComponent implements OnInit {
    constructor(private orderService: OrdersService, private router: Router) {}

    public orders: IOrder[];

    ngOnInit() {
        this.getOrders();
    }

    public getOrders(): void {
        this.orderService.getOrders().subscribe(
            (orders: IOrder[]) => {
                this.orders = orders;
            },
            (error) => {
                console.log(error);
            }
        );
    }

    public orderDetails(orderId: number): void {
        this.router.navigateByUrl('/orders/' + orderId);
    }
}
