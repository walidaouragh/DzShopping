import { Component, OnInit, Input } from '@angular/core';
import { IProduct } from 'src/app/shared/models/IProduct';

@Component({
    selector: 'app-product-item',
    templateUrl: './product-item.component.html',
    styleUrls: ['./product-item.component.scss'],
})
export class ProductItemComponent implements OnInit {
    constructor() {}

    @Input() product: IProduct;

    ngOnInit() {}
}
