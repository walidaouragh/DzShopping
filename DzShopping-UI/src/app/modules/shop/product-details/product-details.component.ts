import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ShopService } from '../shop-service';
import { IProduct } from '../../../shared/models/IProduct';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
    selector: 'app-product-details',
    templateUrl: './product-details.component.html',
    styleUrls: ['./product-details.component.scss'],
})
export class ProductDetailsComponent implements OnInit {
    constructor(
        private activatedRoute: ActivatedRoute,
        private shopService: ShopService,
        private breadcrumbService: BreadcrumbService
    ) {
        this.breadcrumbService.set('@productDetails', '');
    }

    public productId: number;
    public product: IProduct;
    public quantity = 0;

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
        if (this.quantity > 0) {
            this.quantity--;
        }
    }

    public incrementQuantity(): void {
        this.quantity++;
    }

    public addItemToBasket(): void {}
}
