import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private authSource = new BehaviorSubject<boolean>(false);
  auth = this.authSource.asObservable();

  constructor(private httpClient: HttpClient) { }

  changeAuth(b:boolean) {
    this.authSource.next(b);
  }

  checkAuth(): Observable<any> { 
    const url = "/api/bruker/auth";
    return this.httpClient.get<any>(url);
  }

  loggUt(): Observable<any> {
    const url = "/api/bruker/out"
    return this.httpClient.get<any>(url);
  }
}
