import { Component, OnInit } from '@angular/core';
import { Bruker } from 'src/app/interface/bruker';
import { BrukerService } from 'src/app/service/bruker.service';

@Component({
  selector: 'app-list-bruker',
  templateUrl: './list-bruker.component.html',
  styleUrls: ['./list-bruker.component.css']
})
export class ListBrukerComponent implements OnInit {

  brukere: Bruker[] = [];

  constructor(private brukerService: BrukerService) { }

  ngOnInit() {
    this.brukerService.hentAlleBRukere().subscribe(b => {
      this.brukere = b;
    })
  }

  slett(id: number) {
    this.brukerService.slettBruker(id).subscribe(data => {
        this.brukere = this.brukere.filter(b => b.id !== id);
    }, error => {
      console.log(error);
    })
    
  }

}
