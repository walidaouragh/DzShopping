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
import { StepperComponent } from './components/stepper/stepper.component';
import { CdkStepperModule } from '@angular/cdk/stepper';
import { CartSummaryComponent } from './components/cart-summary/cart-summary.component';
import { RouterModule } from '@angular/router';

@NgModule({
    declarations: [
        PagerComponent,
        PagingHeaderComponent,
        OrderTotalsComponent,
        TextInputComponent,
        StepperComponent,
        CartSummaryComponent,
    ],
    imports: [
        CommonModule,
        ReactiveFormsModule,
        CarouselModule.forRoot(),
        PaginationModule.forRoot(),
        BsDropdownModule.forRoot(),
        CdkStepperModule,
        RouterModule,
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
        CdkStepperModule,
        StepperComponent,
        CartSummaryComponent,
    ],
})
export class SharedModule {}
