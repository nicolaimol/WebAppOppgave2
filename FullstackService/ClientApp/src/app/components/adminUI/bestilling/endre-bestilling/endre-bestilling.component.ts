import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Bestilling } from 'src/app/interface/bestilling';
import { KontaktPerson, KundeObj } from 'src/app/interface/kunde';
import { AuthService } from 'src/app/service/auth.service';
import { BestillingService } from 'src/app/service/bestilling.service';
import { ReisendeService } from 'src/app/service/reisende.service';

@Component({
  selector: 'app-endre-bestilling',
  templateUrl: './endre-bestilling.component.html',
  styleUrls: ['./endre-bestilling.component.css']
})
export class EndreBestillingComponent implements OnInit {

  id:number = -1;
  bestilling: Bestilling = null;

  constructor(private bestillingService: BestillingService,
    private router: Router,
    private authService: AuthService,
    private route: ActivatedRoute,
    private reisendeService: ReisendeService) { }

  ngOnInit() {
    this.id = Number(this.route.snapshot.paramMap.get("id"));

    this.bestillingService.hentBestillingById(this.id).subscribe(b => {
      // flytter til /admin om brukeren ikke er logget inn
      this.bestilling = b;
    }, error => {
      if (error.status === 401) {
        this.router.navigate(['/admin'])
      }
      else {
        console.log(error)
      }
    })
  }
  // sender edring av kontaktperson til backend
  changeKontaktPerson(kp: KontaktPerson) {
    this.reisendeService.ChangeKontaktPerson(kp).subscribe(k => {
      console.log(k)
    }, err => console.log(err))
  }

  // sender endringer av andre reisende til backend
  changeVoksen(kunde: KundeObj) {
    this.reisendeService.ChangeVoksen(kunde.kunde).subscribe(k => {
      console.log(k)
    }, err => console.log(err))
  }

  lagre() {
    this.bestillingService.updateBestillingById(this.bestilling.id, this.bestilling).subscribe(v => {
      console.log(v)
    })
  }

}
