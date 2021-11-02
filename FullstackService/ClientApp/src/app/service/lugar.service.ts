import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Lugar } from '../interface/lugar';

@Injectable({
  providedIn: 'root'
})
export class LugarService {
  url:string = 'api/reise'
  httpHeaders: HttpHeaders = new HttpHeaders({
    'Content-Type': 'application/json'
  });


  constructor(private httpClient: HttpClient) { }

  lagreLugar(lugar: Lugar): Observable<Lugar> {
    const url = `${this.url}/lugar/`
    return this.httpClient.post<Lugar>(url, lugar, {
      'headers': this.httpHeaders
    })
  }

  updateLugar(lugar: Lugar): Observable<Lugar> {
    const url = `${this.url}/lugar/`
    return this.httpClient.put<Lugar>(url, lugar, {
      headers: this.httpHeaders
    })
  }

  hentAlleLugererById(id: number): Observable<Lugar[]> {
    const url = `${this.url}/lugar/${id}`
    return this.httpClient.get<Lugar[]>(url)
  }

  slettLugar(id: number): Observable<Lugar> {
    return this.httpClient.delete<Lugar>(`${this.url}/lugar/${id}`);
  }
}
