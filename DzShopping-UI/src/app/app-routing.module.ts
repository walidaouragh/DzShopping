import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './modules/home/home.component';
import { TestErrorComponent } from './core/test-error/test-error.component';
import { ServerErrorComponent } from './core/server-error/server-error.component';
import { NotFoundErrorComponent } from './core/not-found-error/not-found-error.component';
import { skip } from 'rxjs/operators';
import { AuthGuard } from './core/guards/auth.guard';

const routes: Routes = [
    { path: '', component: HomeComponent, data: { breadcrumb: 'Home' } },
    { path: 'test-error', component: TestErrorComponent, data: { breadcrumb: 'Test Error' } },
    { path: 'server-error', component: ServerErrorComponent, data: { breadcrumb: 'Server Error' } },
    { path: 'not-found-error', component: NotFoundErrorComponent, data: { breadcrumb: 'Not Fount Error' } },
    {
        path: 'shop',
        loadChildren: () => import('./modules/shop/shop.module').then((mod) => mod.ShopModule),
        data: { breadcrumb: 'Shop' },
    },
    {
        path: 'contact',
        loadChildren: () => import('./modules/contact/contact.module').then((mod) => mod.ContactModule),
        data: { breadcrumb: 'Contact' },
    },
    {
        path: 'cart',
        loadChildren: () => import('./modules/cart/cart.module').then((mod) => mod.CartModule),
        data: { breadcrumb: 'Cart' },
    },
    {
        path: 'checkout',
        canActivate: [AuthGuard],
        loadChildren: () => import('./modules/checkout/checkout.module').then((mod) => mod.CheckoutModule),
        data: { breadcrumb: 'Checkout' },
    },
    {
        path: 'account',
        loadChildren: () => import('./modules/account/account.module').then((mod) => mod.AccountModule),
        data: { breadcrumb: { skip: true } },
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
