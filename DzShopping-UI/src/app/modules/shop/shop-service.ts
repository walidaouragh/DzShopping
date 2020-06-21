import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, from, of } from 'rxjs';
import { IPagination, Pagination } from 'src/app/shared/models/IPagination';
import { IBrand } from 'src/app/shared/models/IBrand';
import { IType } from 'src/app/shared/models/IType';
import { map } from 'rxjs/operators';
import { ShopParams } from '../../shared/models/shopParams';
import { IProduct } from '../../shared/models/IProduct';

@Injectable({
    providedIn: 'root',
})
export class ShopService {
    constructor(private http: HttpClient) {}

    public products: IProduct[] = [];
    public brands: IBrand[] = [];
    public types: IType[] = [];
    public pagination = new Pagination();
    public shopParams = new ShopParams();

    private baseUrl = 'https://localhost:5001/api';

    getProducts(useCache: boolean): Observable<any> {
        if (useCache === false) {
            this.products = [];
        }

        if (this.products.length > 0 && useCache === true) {
            const pagesReceived = Math.ceil(this.products.length / this.shopParams.pageSize);

            if (this.shopParams.pageNumber <= pagesReceived) {
                this.pagination.data = this.products.slice(
                    (this.shopParams.pageNumber - 1) * this.shopParams.pageSize,
                    this.shopParams.pageNumber * this.shopParams.pageSize
                );

                return of(this.pagination);
            }
        }

        let params = new HttpParams();

        if (this.shopParams.brandId !== 0) {
            params = params.append('brandId', this.shopParams.brandId.toString());
        }

        if (this.shopParams.typeId !== 0) {
            params = params.append('typeId', this.shopParams.typeId.toString());
        }

        if (this.shopParams.search) {
            params = params.append('search', this.shopParams.search);
        }

        params = params.append('sort', this.shopParams.sort);
        params = params.append('pageIndex', this.shopParams.pageNumber.toString());
        params = params.append('pageSize', this.shopParams.pageSize.toString());

        return this.http
            .get<IPagination>(this.baseUrl + '/genericProduct', {
                observe: 'response',
                params,
            })
            .pipe(
                map((response) => {
                    this.products = [...this.products, ...response.body.data];
                    this.pagination = response.body;
                    return this.pagination;
                })
            );
    }

    setShopParams(params: ShopParams) {
        this.shopParams = params;
    }

    getShopParams() {
        return this.shopParams;
    }

    getBrands(): Observable<any> {
        if (this.brands.length > 0) {
            return of(this.brands);
        }
        return this.http.get<IBrand[]>(this.baseUrl + '/genericProduct/brands').pipe(
            map((response) => {
                this.brands = response;
                return response;
            })
        );
    }

    getTypes(): Observable<any> {
        if (this.types.length > 0) {
            return of(this.types);
        }
        return this.http.get<IType[]>(this.baseUrl + '/genericProduct/types').pipe(
            map((response) => {
                this.types = response;
                return response;
            })
        );
    }

    getProduct(productId: number): Observable<any> {
        const product = this.products.find((p) => p.productId === productId);

        if (product) {
            return of(product);
        }

        return this.http.get<IProduct>(this.baseUrl + `/genericProduct/${productId}`);
    }
}
