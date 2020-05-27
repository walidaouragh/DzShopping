import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { PagerComponent } from './components/pager/pager.component';
import { PagingHeaderComponent } from './components/paging-header/paging-header.component';
import { OrderTotalsComponent } from './components/order-totals/order-totals.component';
import { ReactiveFormsModule } from '@angular/forms';
import { TextInputComponent } from './components/text-input/text-input.component';

@NgModule({
    declarations: [PagerComponent, PagingHeaderComponent, OrderTotalsComponent, TextInputComponent],
    imports: [
        CommonModule,
        ReactiveFormsModule,
        CarouselModule.forRoot(),
        PaginationModule.forRoot(),
        BsDropdownModule.forRoot(),
    ],
    exports: [
        PaginationModule,
        CarouselModule,
        BsDropdownModule,
        PagerComponent,
        PagingHeaderComponent,
        OrderTotalsComponent,
        ReactiveFormsModule,
        TextInputComponent,
    ],
})
export class SharedModule {}
