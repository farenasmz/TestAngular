import { Injectable, Inject } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { IProduct } from './IProduct';
import { HttpParams, HttpClient } from '@angular/common/http';

@Injectable()
export class ProductService {

  private apiURL = this.baseUrl + "api/products";

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getProducts(): Observable<IProduct[]> {
    return this.http.get<IProduct[]>(this.apiURL);
  }

  getProduct(productID: string): Observable<IProduct> {
    let params = new HttpParams().set('incluirDirecciones', "true");
    return this.http.get<IProduct>(this.apiURL + '/' + productID, { params: params });
  }

  createProduct(product: IProduct): Observable<IProduct> {
    return this.http.post<IProduct>(this.apiURL, product);
  }

  updateProduct(product: IProduct): Observable<IProduct> {
    return this.http.put<IProduct>(this.apiURL + "/" + product.id.toString(), product);
  }

  deleteProduct(productID: string): Observable<IProduct> {
    return this.http.delete<IProduct>(this.apiURL + "/" + productID);
  }

  ResetProduct(product: IProduct) {
    return this.http.post(this.apiURL + "/ResetProduct", product);
  }
}
