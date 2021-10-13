import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { BestillingInfo } from 'src/app/interface/bestilling';
import { KontaktPerson, Kunde, Post } from 'src/app/interface/kunde';
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
  voksne: Kunde[] = [];
  barn: Kunde[] = [];

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
    }
  }

  updateKontaktPerson(kontaktPerson: KontaktPerson) {
    this.kontaktPerson = kontaktPerson;
    console.log(this.kontaktPerson);
  }
}
