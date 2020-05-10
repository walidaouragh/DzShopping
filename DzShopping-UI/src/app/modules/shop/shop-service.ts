import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, from } from 'rxjs';
import { IPagination } from 'src/app/shared/models/IPagination';
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

    private baseUrl = 'https://localhost:5001/api';

    getProducts(shopParams: ShopParams): Observable<any> {
        let params = new HttpParams();

        if (shopParams.brandId !== 0) {
            params = params.append('brandId', shopParams.brandId.toString());
        }

        if (shopParams.typeId !== 0) {
            params = params.append('typeId', shopParams.typeId.toString());
        }

        if (shopParams.search) {
            params = params.append('search', shopParams.search);
        }

        params = params.append('sort', shopParams.sort);
        params = params.append('pageIndex', shopParams.pageNumber.toString());
        params = params.append('pageSize', shopParams.pageSize.toString());

        return this.http
            .get<IPagination>(this.baseUrl + '/genericProduct', {
                observe: 'response',
                params,
            })
            .pipe(
                map((response) => {
                    return response.body;
                })
            );
    }

    getBrands(): Observable<any> {
        return this.http.get<IBrand[]>(this.baseUrl + '/genericProduct/brands');
    }

    getTypes(): Observable<any> {
        return this.http.get<IType[]>(this.baseUrl + '/genericProduct/types');
    }

    getProduct(productId: number): Observable<any> {
        return this.http.get<IProduct>(this.baseUrl + `/genericProduct/${productId}`);
    }
}
