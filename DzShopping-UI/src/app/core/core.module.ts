import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { SharedModule } from '../shared/shared.module';
import { RouterModule } from '@angular/router';
import { TestErrorComponent } from './test-error/test-error.component';
import { NotFoundErrorComponent } from './not-found-error/not-found-error.component';
import { ServerErrorComponent } from './server-error/server-error.component';
import { ToastrModule } from 'ngx-toastr';
import { SectionHeaderComponent } from './section-header/section-header.component';
import { BreadcrumbModule } from 'xng-breadcrumb';

@NgModule({
    declarations: [
        NavBarComponent,
        TestErrorComponent,
        NotFoundErrorComponent,
        ServerErrorComponent,
        SectionHeaderComponent,
    ],
    imports: [
        CommonModule,
        SharedModule,
        RouterModule,
        BreadcrumbModule,
        ToastrModule.forRoot({ positionClass: 'toast-bottom-right', preventDuplicates: true }),
    ],
    exports: [NavBarComponent, SectionHeaderComponent],
})
export class CoreModule {}
