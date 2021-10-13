import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Lugar } from '../interface/lugar';
import { Reise } from '../interface/reise';

@Injectable({
  providedIn: 'root'
})
export class ReiseService {

  url:string = 'api/reise'

  constructor(private httpClient: HttpClient) { }

  hentAlleReiser(): Observable<Reise[]> {
    return this.httpClient.get<Reise[]>(this.url)
  }

  hentAlleLugererById(id: number): Observable<Lugar[]> {
    const url = `${this.url}/lugar/${id}`
    return this.httpClient.get<Lugar[]>(url)
  }
}
