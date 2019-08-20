import { Injectable, Inject } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HttpParams, HttpClient } from '@angular/common/http';
import { IUser } from './IUser';

@Injectable()
export class UserService {

  private apiURL = this.baseUrl + "api/Account";

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getUsers(): Observable<IUser[]> {
    return this.http.get<IUser[]>(this.apiURL);
  }

  getUser(UserID: string): Observable<IUser> {
    let params = new HttpParams().set('incluirDirecciones', "true");
    return this.http.get<IUser>(this.apiURL + '/' + UserID, { params: params });
  }

  createUser(user: IUser): Observable<IUser> {
    return this.http.post<IUser>(this.apiURL, user);
  }

  updateUser(product: IUser): Observable<IUser> {
    return this.http.put<IUser>(this.apiURL + "/" + product.id.toString(), product);
  }

  deleteUser(userID: string): Observable<IUser> {
    return this.http.delete<IUser>(this.apiURL + "/" + userID);
  }

}
