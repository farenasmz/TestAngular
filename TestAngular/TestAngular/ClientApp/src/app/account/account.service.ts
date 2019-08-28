import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IUserInfo } from './user-info';
import { Observable } from 'rxjs/Observable';
import { Router } from '@angular/router';

@Injectable()
export class AccountService {
  // pass: Aa123456!
  private apiURL = this.baseUrl + "api/Account";
  toastsSerivce: any;
  translateService: any;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router: Router) { }

  create(userInfo: IUserInfo): Observable<any> {
    return this.http.post<any>(this.apiURL + "/Create", userInfo)
  }

  login(userInfo: IUserInfo): Observable<IUserInfo> {
    return this.http.post<any>(this.apiURL + "/Login", userInfo);
  }

  obtenerToken(): string {
    return localStorage.getItem("token");
  }

  obtenerExpiracionToken(): string {
    return localStorage.getItem("tokenExpiration");
  }

  logout() {
    localStorage.removeItem("token");
    localStorage.removeItem("tokenExpiration");
    localStorage.removeItem("currentUser");
    localStorage.removeItem("rol");
    localStorage.clear();
    this.router.navigate(['register-login']);
  }

  estaLogueado(): boolean {

    var exp = this.obtenerExpiracionToken();

    if (!exp) {
      // el token no existe
      return false;
    }

    var now = new Date().getTime();
    var dateExp = new Date(exp);

    if (now >= dateExp.getTime()) {
      // ya expiró el token
      localStorage.removeItem('token');
      localStorage.removeItem('tokenExpiration');
      localStorage.removeItem("currentUser");
      localStorage.removeItem("rol");
      return false;
    } else {
      return true;
    }

  }

} 
