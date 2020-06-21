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
    constructor(private shopService: ShopService) {
        this.shopParams = this.shopService.getShopParams();
    }

    @ViewChild('search', { static: false }) searchTerm: ElementRef;

    public shopParams: ShopParams;
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
        this.getProducts(true);
        this.getBrands();
        this.getTypes();
    }

    public getProducts(useCache = false): void {
        this.shopService.getProducts(useCache).subscribe(
            (response: IPagination) => {
                this.products = response.data;
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
        const params = this.shopService.getShopParams();
        params.brandId = brandId;
        params.pageNumber = 1;
        this.shopService.setShopParams(params);
        this.getProducts();
    }

    public onTypeSelected(typeId: number): void {
        const params = this.shopService.getShopParams();
        params.typeId = typeId;
        params.pageNumber = 1;
        this.shopService.setShopParams(params);
        this.getProducts();
    }

    public onSortSelected(sort: string): void {
        const params = this.shopService.getShopParams();
        params.sort = sort;
        this.shopService.setShopParams(params);
        this.getProducts();
    }

    public onPageChanged(event: any) {
        const params = this.shopService.getShopParams();
        if (params.pageNumber !== event) {
            params.pageNumber = event;
            this.shopService.setShopParams(params);
            this.getProducts(true);
        }
    }

    public onProductSearch() {
        const params = this.shopService.getShopParams();
        params.search = this.searchTerm.nativeElement.value;
        params.pageNumber = 1;
        this.shopService.setShopParams(params);
        this.getProducts();
    }

    public onProductReset() {
        this.searchTerm.nativeElement.value = '';
        this.shopParams = new ShopParams();
        this.shopService.setShopParams(this.shopParams);
        this.getProducts();
    }
}
