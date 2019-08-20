import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class ProductBookService {
  private apiURL = this.baseUrl + "api/Account";

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  BookProduct(productID: number): Observable<any> {
    localStorage.setItem('currentUser', "farenas1@misena.edu.co");
    return this.http.put(this.apiURL + "/reservation?productId=" + productID, localStorage.getItem('currentUser'));
  }

}
