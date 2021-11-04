import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Bruker } from 'src/app/interface/bruker';
import { BrukerService } from 'src/app/service/bruker.service';

@Component({
  selector: 'app-lag-bruker',
  templateUrl: './lag-bruker.component.html',
  styleUrls: ['./lag-bruker.component.css']
})
export class LagBrukerComponent implements OnInit {

  @Output() notifyParent = new EventEmitter<Bruker>()

  bruker: Bruker = {
    brukernavn: "",
    passord: "",
  }

  constructor(private brukerService: BrukerService) { }

  ngOnInit() {
  }

  // sendes til forleder at bruker skal lages med ny bruker
  registrer() {
    this.brukerService.lagBruker(this.bruker).subscribe(bruker => {
      this.notifyParent.emit(bruker)
      this.bruker.brukernavn = "";
      this.bruker.passord = "";
    }, err => console.log(err))
  }

}
