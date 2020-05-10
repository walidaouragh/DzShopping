import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ShopService } from '../shop-service';
import { IProduct } from '../../../shared/models/IProduct';

@Component({
    selector: 'app-product-details',
    templateUrl: './product-details.component.html',
    styleUrls: ['./product-details.component.scss'],
})
export class ProductDetailsComponent implements OnInit {
    constructor(private activatedRoute: ActivatedRoute, private shopService: ShopService) {}

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
