import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop.component';
import { ShopRoutingModule } from './shop-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
    declarations: [ShopComponent],
    imports: [CommonModule, ShopRoutingModule, SharedModule],
    exports: [ShopComponent],
})
export class ShopModule {}
