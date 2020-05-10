import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopRoutingModule } from './contact-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { ContactComponent } from './contact.component';

@NgModule({
    declarations: [ContactComponent],
    imports: [CommonModule, ShopRoutingModule, SharedModule],
})
export class ContactModule {}
