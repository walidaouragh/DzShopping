import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './modules/home/home.component';
import { TestErrorComponent } from './core/test-error/test-error.component';
import { ServerErrorComponent } from './core/server-error/server-error.component';
import { NotFoundErrorComponent } from './core/not-found-error/not-found-error.component';

const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'test-error', component: TestErrorComponent },
    { path: 'server-error', component: ServerErrorComponent },
    { path: 'not-found-error', component: NotFoundErrorComponent },
    {
        path: 'shop',
        loadChildren: () => import('./modules/shop/shop.module').then((mod) => mod.ShopModule),
    },
    {
        path: 'contact',
        loadChildren: () => import('./modules/contact/contact.module').then((mod) => mod.ContactModule),
    },
    {
        path: '**',
        redirectTo: '',
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
})
export class AppRoutingModule {}
