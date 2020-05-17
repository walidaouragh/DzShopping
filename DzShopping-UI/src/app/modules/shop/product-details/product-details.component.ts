import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ShopService } from '../shop-service';
import { IProduct } from '../../../shared/models/IProduct';
import { BreadcrumbService } from 'xng-breadcrumb';
import { CartService } from '../../cart/cart.service';

@Component({
    selector: 'app-product-details',
    templateUrl: './product-details.component.html',
    styleUrls: ['./product-details.component.scss'],
})
export class ProductDetailsComponent implements OnInit {
    constructor(
        private activatedRoute: ActivatedRoute,
        private shopService: ShopService,
        private breadcrumbService: BreadcrumbService,
        private cartService: CartService
    ) {
        this.breadcrumbService.set('@productDetails', '');
    }

    public productId: number;
    public product: IProduct;
    public quantity = 1;

    ngOnInit() {
        this.productId = +this.activatedRoute.snapshot.paramMap.get('productId');
        this.getProduct(this.productId);
    }

    public getProduct(productId: number): void {
        this.shopService.getProduct(productId).subscribe(
            (product: IProduct) => {
                this.product = product;
                this.breadcrumbService.set('@productDetails', this.product.productName);
            },
            (error) => {
                console.log(error);
            }
        );
    }

    public decrementQuantity(): void {
        if (this.quantity > 1) {
            this.quantity--;
        }
    }

    public incrementQuantity(): void {
        this.quantity++;
    }

    public addItemToCart(): void {
        this.cartService.addItemToCart(this.product, this.quantity);
    }
}
