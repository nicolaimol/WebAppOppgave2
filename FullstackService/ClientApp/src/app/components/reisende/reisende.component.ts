import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { BestillingInfo } from 'src/app/interface/bestilling';
import { KontaktPerson, Kunde, KundeObj, Post } from 'src/app/interface/kunde';
import { BestillingInfoService } from 'src/app/service/bestilling-info.service';

@Component({
  selector: 'app-reisende',
  templateUrl: './reisende.component.html',
  styleUrls: ['./reisende.component.css']
})
export class ReisendeComponent implements OnInit, OnDestroy {

  bestilling: BestillingInfo;
  subscription: Subscription;

  kontaktPerson: KontaktPerson;
  validKontaktPerson: boolean = false;
  kundeObj: KundeObj;
  voksne: Kunde[] = [];
  validVoksne: boolean[] = [];
  barn: Kunde[] = [];
  validBarn: boolean[] = [];

  validTotal: boolean = false;

  constructor(private bestillingInfoService: BestillingInfoService, private router: Router) { }


  ngOnDestroy(): void {
  
    this.subscription.unsubscribe();
  }

  ngOnInit() {
    this.subscription = this.bestillingInfoService.currentBestilling.subscribe(bestilling => this.bestilling = bestilling);

    this.bestilling = JSON.parse(localStorage.getItem('bestilling'))

    console.log(this.bestilling)
    if (this.bestilling === null) {
      this.router.navigate([''])
    } else {
      this.kontaktPerson = {
        fornavn: "",
        etternavn: "",
        foedselsdato: "",
        adresse: "",
        post: {
          postnummer: 0,
          poststed: ""
        },
        telefon: "",
        epost: ""
      }

      for (let i = 1; i < this.bestilling.antall_voksen; i++) {
        this.voksne.push({
          fornavn: "",
          etternavn: "",
          foedselsdato: "",
        })
        this.validVoksne.push(false);
      }

      for (let i = 0; i < this.bestilling.antall_barn; i++) {
        this.barn.push({
          fornavn: "",
          etternavn: "",
          foedselsdato: "",
        })
        this.validBarn.push(false);
      }
    }
  }

  updateKontaktPerson(kontaktPerson: KontaktPerson) {
    this.kontaktPerson = kontaktPerson;
    this.validKontaktPerson = true;

    this.validerTotal();
  }

  updateKontakt(kundeObj: KundeObj) {

    if (kundeObj.type === 'voksen') {
      this.voksne[kundeObj.index] = kundeObj.kunde;
      this.validVoksne[kundeObj.index] = true;
    } else {
      this.barn[kundeObj.index] = kundeObj.kunde;
      this.validBarn[kundeObj.index] = true;
    }

    this.validerTotal();
  }

  validerTotal() {
    this.validTotal = true;
    this.validTotal = this.validTotal && this.validKontaktPerson;
    for (let valid of this.validVoksne) {
      this.validTotal = this.validTotal && valid
    }
    for (let valid of this.validBarn) {
      this.validTotal = this.validTotal && valid
    }
  }

  submit() {
    console.log('nå kan vi bestille')
  }
}
