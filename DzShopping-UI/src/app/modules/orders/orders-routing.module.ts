import { NgModule } from '@angular/core';
import { OrdersComponent } from './orders/orders.component';
import { Routes, RouterModule } from '@angular/router';
import { OrderDetailsComponent } from './orders/order-details/order-details.component';

const routes: Routes = [
    { path: '', component: OrdersComponent },
    { path: ':orderId', component: OrderDetailsComponent, data: { breadcrumb: { alias: 'OrderDetails' } } },
];

@NgModule({
    exports: [RouterModule],
    imports: [RouterModule.forChild(routes)],
})
export class OrdersRoutingModule {}
