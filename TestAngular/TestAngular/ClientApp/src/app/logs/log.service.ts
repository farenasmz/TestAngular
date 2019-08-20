import { Injectable, Inject } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HttpParams, HttpClient } from '@angular/common/http';
import { ILog } from './log/ILog';

@Injectable()
export class LogService {

  private apiURL = this.baseUrl + "api/Logs";

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getLogs(): Observable<ILog[]> {
    return this.http.get<ILog[]>(this.apiURL);
  }
}
