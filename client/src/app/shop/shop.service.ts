import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IBrand } from '../shared/models/brand';
import {IPagination} from '../shared/models/pagination';
import { IProduct } from '../shared/models/product';
import { IType } from '../shared/models/type';
import { map } from 'rxjs/operators';
import { shopParams } from '../shared/models/shopParams';
import { environment } from 'src/environments/environment';
import { of } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = environment.apiUrl;
  products: IProduct[]=[];
  constructor(private http: HttpClient) { }


  getProducts(shopParams: shopParams){
    let params = new HttpParams();

    if (shopParams.brandId !== 0) {
      params = params.append('brandId', shopParams.brandId.toString());
    }

    if (shopParams.typeId !== 0){
      params = params.append('typeId', shopParams.typeId.toString());
    }
    if (shopParams.search){
      params = params.append('search', shopParams.search);
    }

    params = params.append('sort', shopParams.sort);
    params = params.append('itemsperpage', shopParams.itemsPerPage.toString());
    params = params.append('pageIndex', shopParams.pageIndex.toString());
    return this.http.get<IPagination>(this.baseUrl + 'products',
      // pipe --> permet de diriger le resultat de fonctions vers d 'autres fonctions
      {observe: 'response', params})
        .pipe(map(response => {
        return response.body;
      }));
  }
  getBrands(){
    return this.http.get<IBrand[]>(this.baseUrl + 'products/brands');
  }
  getTypes(){
    return this.http.get<IType[]>(this.baseUrl + 'products/types');
  }
  // dans le cours getProduct(id: number)
  getProduct(id: number){
    // tslint:disable-next-line: no-debugger
    const product = this.products.find(p => p.id === id);
    if(product){
      return of(product);
    }
    // tslint:disable-next-line: no-debugger
//   debugger;
    const tp=this.http.get<IProduct>(`${this.baseUrl}products/${id}`);
    console.log(tp);
    return tp;
   
  }
}
