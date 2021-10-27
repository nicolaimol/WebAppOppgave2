import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Bestilling } from 'src/app/interface/bestilling';
import { AuthService } from 'src/app/service/auth.service';
import { BestillingService } from 'src/app/service/bestilling.service';

@Component({
  selector: 'app-list-bestillinger',
  templateUrl: './list-bestillinger.component.html',
  styleUrls: ['./list-bestillinger.component.css']
})
export class ListBestillingerComponent implements OnInit {

  bestillinger:Bestilling[] = [];

  constructor(private router: Router, private authService: AuthService, private bestillingService: BestillingService) { }

  ngOnInit() {
    this.authService.auth.subscribe(auth => {
      if (!auth) this.router.navigate(['/admin'])
    })

    this.bestillingService.hentAlleBestillinger().subscribe(b => {
      this.bestillinger = b;
    })

  }



}
