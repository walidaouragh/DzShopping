import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { AccountRoutingModule } from './account-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
    declarations: [LoginComponent, RegisterComponent],
    imports: [CommonModule, AccountRoutingModule, SharedModule],
})
export class AccountModule {}
