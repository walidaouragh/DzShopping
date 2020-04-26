import { Component, OnInit } from '@angular/core';
import { ShopService } from './shop-service';
import { IProduct } from 'src/app/shared/models/IProduct';

@Component({
    selector: 'app-shop',
    templateUrl: './shop.component.html',
    styleUrls: ['./shop.component.scss'],
})
export class ShopComponent implements OnInit {
    constructor(private shopService: ShopService) {}

    public produts: IProduct[];

    ngOnInit(): void {
        this.getProducts();
    }

    getProducts(): void {
        this.shopService.getProducts().subscribe((products: any[]) => {
            console.log('products', products);
        });
    }
}
