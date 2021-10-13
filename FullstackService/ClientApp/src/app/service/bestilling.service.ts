import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class BestillingService {

  constructor(private httpClient: HttpClient) { }


}
