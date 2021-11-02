import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Log } from '../interface/log'
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LogService {

  constructor(private httpClient: HttpClient) { }

  hentAlleLog(): Observable<Log[]> {
    return this.httpClient.get<Log[]>("/api/log");
  }
}
