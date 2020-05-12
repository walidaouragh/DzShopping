import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ShopService } from './shop-service';
import { IProduct } from 'src/app/shared/models/IProduct';
import { IBrand } from 'src/app/shared/models/IBrand';
import { IType } from 'src/app/shared/models/IType';
import { IPagination } from 'src/app/shared/models/IPagination';
import { ShopParams } from '../../shared/models/shopParams';

@Component({
    selector: 'app-shop',
    templateUrl: './shop.component.html',
    styleUrls: ['./shop.component.scss'],
})
export class ShopComponent implements OnInit {
    constructor(private shopService: ShopService) {}

    @ViewChild('search', { static: false }) searchTerm: ElementRef;

    public shopParams = new ShopParams();
    public products: IProduct[];
    public brands: IBrand[];
    public types: IType[];
    public totalCount: number;
    public sortOptions = [
        { name: 'Alphabetical', value: 'name' },
        { name: 'Price: Low to High', value: 'priceAsc' },
        { name: 'Price: High to Low', value: 'PriceDesc' },
    ];

    ngOnInit(): void {
        this.getProducts();
        this.getBrands();
        this.getTypes();
    }

    public getProducts(): void {
        this.shopService.getProducts(this.shopParams).subscribe(
            (response: IPagination) => {
                this.products = response.data;
                this.shopParams.pageNumber = response.pageIndex;
                this.shopParams.pageSize = response.pageSize;
                this.totalCount = response.count;
            },
            (error: any) => {
                console.log(error);
            }
        );
    }

    public getBrands(): void {
        this.shopService.getBrands().subscribe(
            (response: IBrand[]) => {
                this.brands = [{ productBrandId: 0, productBrandName: 'All' }, ...response];
            },
            (error: any) => {
                console.log(error);
            }
        );
    }

    public getTypes(): void {
        this.shopService.getTypes().subscribe(
            (response: IType[]) => {
                this.types = [{ productTypeId: 0, productTypeName: 'All' }, ...response];
            },
            (error: any) => {
                console.log(error);
            }
        );
    }

    public onBrandSelected(brandId: number): void {
        this.shopParams.brandId = brandId;
        this.shopParams.pageNumber = 1;
        this.getProducts();
    }

    public onTypeSelected(typeId: number): void {
        this.shopParams.typeId = typeId;
        this.shopParams.pageNumber = 1;
        this.getProducts();
    }

    public onSortSelected(sort: string): void {
        this.shopParams.sort = sort;
        this.getProducts();
    }

    public onPageChanged(event: any) {
        if (this.shopParams.pageNumber !== event) {
            this.shopParams.pageNumber = event;
            this.getProducts();
        }
    }

    public onProductSearch() {
        this.shopParams.search = this.searchTerm.nativeElement.value;
        this.shopParams.pageNumber = 1;
        this.getProducts();
    }

    public onProductReset() {
        this.searchTerm.nativeElement.value = '';
        this.shopParams = new ShopParams();
        this.getProducts();
    }
}
