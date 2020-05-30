import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrdersComponent } from './orders/orders.component';
import { OrdersRoutingModule } from './orders-routing.module';
import { OrderDetailsComponent } from './orders/order-details/order-details.component';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
    declarations: [OrdersComponent, OrderDetailsComponent],
    imports: [CommonModule, OrdersRoutingModule, SharedModule],
})
export class OrdersModule {}
