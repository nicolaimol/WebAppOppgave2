import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Bestilling } from '../interface/bestilling';

@Injectable({
  providedIn: 'root'
})
export class BestillingService {

  httpHeaders: HttpHeaders = new HttpHeaders({
    'Content-Type': 'application/json'
  });
  url: string = 'api/bestilling';

  constructor(private httpClient: HttpClient) { }

  sendBestilling(bestilling: Bestilling): Observable<Bestilling> {
    return this.httpClient.post<Bestilling>(this.url, bestilling, {
      'headers': this.httpHeaders
    });
  }

  hentBestillingByRef(ref: string): Observable<Bestilling> {
    return this.httpClient.get<Bestilling>(this.url + "/ref/" + ref)
  }
}
