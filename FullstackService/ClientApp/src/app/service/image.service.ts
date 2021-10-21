import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { transform } from 'typescript';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  httpHeaders: HttpHeaders = new HttpHeaders({
    'Content-Type': 'multipart/form-data'
  });

  constructor(private httpClient: HttpClient) { }

  uploadImage(image: any): Observable<any> {
    return this.httpClient.post<string>(`/api/image`, image)
  }
}
