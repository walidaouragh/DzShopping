import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { PagerComponent } from './components/pager/pager.component';
import { PagingHeaderComponent } from './components/paging-header/paging-header.component';
import { OrderTotalsComponent } from './components/order-totals/order-totals.component';

@NgModule({
    declarations: [PagerComponent, PagingHeaderComponent, OrderTotalsComponent],
    imports: [CommonModule, CarouselModule.forRoot(), PaginationModule.forRoot()],
    exports: [PaginationModule, CarouselModule, PagerComponent, PagingHeaderComponent, OrderTotalsComponent],
})
export class SharedModule {}
