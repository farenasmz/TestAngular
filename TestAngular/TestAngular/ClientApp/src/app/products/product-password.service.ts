import { Injectable, Inject } from '@angular/core';
import { IUser } from '../users/IUser';
import { Observable } from 'rxjs/Observable';

import { HttpClient } from '@angular/common/http';

@Injectable()
export class ProductPasswordService {
  private apiURL = this.baseUrl + "api/Products";
  
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  ResetProduct(productId: number, user: IUser): Observable<IUser> {
    user.email = localStorage.getItem("currentUser");
    return this.http.post<any>(this.apiURL + "/ResetProduct?productId=" + productId, user);
  }
}
