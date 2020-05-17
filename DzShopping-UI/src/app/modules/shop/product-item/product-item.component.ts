import { Component, OnInit, Input } from '@angular/core';
import { IProduct } from 'src/app/shared/models/IProduct';
import { CartService } from '../../cart/cart.service';

@Component({
    selector: 'app-product-item',
    templateUrl: './product-item.component.html',
    styleUrls: ['./product-item.component.scss'],
})
export class ProductItemComponent implements OnInit {
    constructor(private cartService: CartService) {}

    @Input() product: IProduct;

    ngOnInit() {}

    public addItemToCart(): void {
        this.cartService.addItemToCart(this.product);
    }
}
