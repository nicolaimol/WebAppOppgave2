import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { KontaktPerson, Kunde } from '../interface/kunde';

@Injectable({
  providedIn: 'root'
})
export class ReisendeService {

  httpHeaders: HttpHeaders = new HttpHeaders({
    "Content-Type": "application/json"
  })

  constructor(private httpClient: HttpClient) { }

  ChangeKontaktPerson(k: KontaktPerson): Observable<KontaktPerson> {
    const url = `/api/reisende/kontaktperson/${k.id}`

    return this.httpClient.put<KontaktPerson>(url, k, {headers: this.httpHeaders})
  }

  ChangeVoksen(k: Kunde): Observable<Kunde> {
    const url = `/api/reisende/voksen/${k.id}`

    return this.httpClient.put<Kunde>(url, k, {headers: this.httpHeaders})
  }

  ChangeBarn(k: Kunde): Observable<Kunde> {
    const url = `/api/reisende/barn/${k.id}`

    return this.httpClient.put<Kunde>(url, k, {headers: this.httpHeaders})
  }
}
