import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { BestillingInfo } from '../interface/bestilling'

@Injectable({
  providedIn: 'root'
})
export class BestillingInfoService {
  bestilling:BestillingInfo = {
    utreiseDato: "",
    pris: 0,
    antallLugarer: 0,
    reiseId: 0,
    antall_barn: 0,
    antall_voksen: 1
  };
  private bestillingInfoSource = new BehaviorSubject(null);
  currentBestilling = this.bestillingInfoSource.asObservable();

  constructor() { }

  changeBestilling(newBestilling:BestillingInfo) {
    this.bestillingInfoSource.next(newBestilling);
  }
}
