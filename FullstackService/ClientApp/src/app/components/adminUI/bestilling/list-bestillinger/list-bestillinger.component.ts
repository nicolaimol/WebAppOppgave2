import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Bestilling } from 'src/app/interface/bestilling';
import { AuthService } from 'src/app/service/auth.service';
import { BestillingService } from 'src/app/service/bestilling.service';
import { ModalSlettComponent } from '../../../modal-slett/modal-slett.component';

@Component({
  selector: 'app-list-bestillinger',
  templateUrl: './list-bestillinger.component.html',
  styleUrls: ['./list-bestillinger.component.css']
})
export class ListBestillingerComponent implements OnInit {

  bestillinger:Bestilling[] = [];

  constructor(private router: Router, private authService: AuthService, private bestillingService: BestillingService, private modalService: NgbModal) { }

  ngOnInit() {
    // flytter til /admin om brukeren ikke er logget inn
    this.authService.auth.subscribe(auth => {
      if (!auth) this.router.navigate(['/admin'])
      else {
        // henter alle bestillinger
        this.bestillingService.hentAlleBestillinger().subscribe(b => {
          this.bestillinger = b;
        })
      }
    })


  }

  slett(b: Bestilling) {
    // bekreftmodal vises
    // ved slett slettes bestilling fra database
    const modalRef = this.modalService.open(ModalSlettComponent)
    modalRef.componentInstance.message = "Slett bestillingen: " + b.referanse;

    modalRef.result.then((result) => {
      if (result == "Lukk"){

      } else {
        this.bestillingService.slettBestillingById(b.id).subscribe(be => {
        this.bestillinger = this.bestillinger.filter(be => be.id != b.id)})
      }
    })
    
  }



}
