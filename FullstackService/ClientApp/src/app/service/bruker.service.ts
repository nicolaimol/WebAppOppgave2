import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Bruker, BrukerUpdate } from '../interface/bruker';

@Injectable({
  providedIn: 'root'
})
export class BrukerService {

  httpHeaders: HttpHeaders = new HttpHeaders({"Content-Type": "application/json"})

  constructor(private httpClient: HttpClient) { }

  validerBruker(b: Bruker): Observable<Bruker>{
    const url = "api/bruker/auth";
    return this.httpClient.post<Bruker>(url, b, {headers:this.httpHeaders});
  }

  lagBruker(b: Bruker): Observable<Bruker> {
    return this.httpClient.post<Bruker>(`api/bruker`, b, {headers: this.httpHeaders});
  }

  updateBruker(b: BrukerUpdate, id: number): Observable<Bruker> {
    return this.httpClient.put<Bruker>(`api/bruker/${id}`, b, {headers: this.httpHeaders})
  }

  hentAlleBRukere(): Observable<Bruker[]> {
    return this.httpClient.get<Bruker[]>(`api/bruker`)
  }

  slettBruker(id: number): Observable<Bruker> {
    return this.httpClient.delete<Bruker>(`api/bruker/${id}`);
  }
}
