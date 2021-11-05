import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Bestilling } from '../interface/bestilling';
import { Kunde } from '../interface/kunde';

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

  hentAlleBestillinger(): Observable<Bestilling[]> {
    return this.httpClient.get<Bestilling[]>(this.url)
  }

  hentBestillingById(id: number): Observable<Bestilling> {
    return this.httpClient.get<Bestilling>(this.url + "/" + id);
  }

  slettBestillingById(id: number): Observable<Bestilling> {
    return this.httpClient.delete<Bestilling>(this.url + "/" + id);
  }

  updateBestillingById(id: number, b: Bestilling): Observable<Bestilling> {
    return this.httpClient.put<Bestilling>(this.url + "/" + id, b, {headers: this.httpHeaders})
  }
}
