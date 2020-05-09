import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop.component';
import { ShopRoutingModule } from './shop-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { ProductItemComponent } from './product-item/product-item.component';

@NgModule({
    declarations: [ShopComponent, ProductItemComponent],
    imports: [CommonModule, ShopRoutingModule, SharedModule],
    exports: [ShopComponent, ProductItemComponent],
})
export class ShopModule {}
