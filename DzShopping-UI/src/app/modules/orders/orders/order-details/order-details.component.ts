import { Component, OnInit } from '@angular/core';
import { OrdersService } from '../../orders.service';
import { IOrder } from 'src/app/shared/models/IOrderToCreate';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
    selector: 'app-order-details',
    templateUrl: './order-details.component.html',
    styleUrls: ['./order-details.component.scss'],
})
export class OrderDetailsComponent implements OnInit {
    constructor(
        private orderService: OrdersService,
        private activatedRoute: ActivatedRoute,
        private breadcrumbService: BreadcrumbService
    ) {
        this.breadcrumbService.set('@OrderDetails', '');
    }

    public order: IOrder;

    ngOnInit() {
        const orderId = +this.activatedRoute.snapshot.paramMap.get('orderId');
        this.getOrderDetails(orderId);
    }

    public getOrderDetails(orderId: number): void {
        this.orderService.getOrder(orderId).subscribe((order: IOrder) => {
            this.order = order;
            this.breadcrumbService.set('@OrderDetails', `Order# ${this.order.id} - ${this.order.status}`);
        });
    }
}
