import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { IBookProduct } from './IBookProduct';

@Injectable()
export class ProductBookService {
  private apiURL = this.baseUrl + "api/Products";
  bookProduct: IBookProduct = { id: 0, email: '', productId: 0, value: 0 };
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }
  
  BookProduct(productId: number, value: number): Observable<any> {
    localStorage.setItem('currentUser', "farenas1@misena.edu.co");
    this.bookProduct.email = localStorage.getItem('currentUser');
    this.bookProduct.productId = productId;
    this.bookProduct.value = value;
    return this.http.post(this.apiURL + "/BookProduct", this.bookProduct);
  }
}
