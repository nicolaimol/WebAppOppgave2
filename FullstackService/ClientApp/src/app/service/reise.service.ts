import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Post } from '../interface/kunde';
import { Lugar } from '../interface/lugar';
import { Reise } from '../interface/reise';

@Injectable({
  providedIn: 'root'
})
export class ReiseService {

  url:string = 'api/reise'
  httpHeaders: HttpHeaders = new HttpHeaders({
    'Content-Type': 'application/json'
  });

  constructor(private httpClient: HttpClient) { }

  hentAlleReiser(): Observable<Reise[]> {
    return this.httpClient.get<Reise[]>(this.url)
  }

  hentReiseById(id: number): Observable<Reise> {
    const url = `${this.url}/${id}`
    return this.httpClient.get<Reise>(url);
  }

  hentAlleLugererById(id: number): Observable<Lugar[]> {
    const url = `${this.url}/lugar/${id}`
    return this.httpClient.get<Lugar[]>(url)
  }

  HentPostByPostnummer(postnummer: string): Observable<Post> {
    const url = `${this.url}/postnummer/${postnummer}`
    return this.httpClient.get<Post>(url);
  }

  updateReise(reise: Reise): Observable<Reise> {
    const url = `${this.url}/${reise.id}`
    return this.httpClient.put<Reise>(url, reise, {
      "headers": this.httpHeaders
    })
  }

  lagreReise(reise: Reise): Observable<Reise> {
    return this.httpClient.post<Reise>(this.url, reise, {
      "headers": this.httpHeaders
    })
  }
}
