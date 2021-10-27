import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private authSource = new BehaviorSubject<boolean>(false);
  auth = this.authSource.asObservable();

  constructor() { }

  changeAuth(b:boolean) {
    this.authSource.next(b);
  }
}
