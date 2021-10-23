import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Bestilling } from 'src/app/interface/bestilling';
import { BestillingService } from 'src/app/service/bestilling.service';

@Component({
  selector: 'app-hent-bestilling',
  templateUrl: './hent-bestilling.component.html',
  styleUrls: ['./hent-bestilling.component.css']
})
export class HentBestillingComponent implements OnInit {

  ref: string = "";

  @Output() notifyParent = new EventEmitter<Bestilling>();

  constructor(private bestillingService: BestillingService) { }

  ngOnInit() {
  }

  hent() {
    this.bestillingService.hentBestillingByRef(this.ref)
    .subscribe(bestilling => {
      this.notifyParent.emit(bestilling)
    })
  }

}
