import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Bruker } from 'src/app/interface/bruker';
import { AuthService } from 'src/app/service/auth.service';
import { BrukerService } from 'src/app/service/bruker.service';

@Component({
  selector: 'app-list-bruker',
  templateUrl: './list-bruker.component.html',
  styleUrls: ['./list-bruker.component.css']
})
export class ListBrukerComponent implements OnInit {

  brukere: Bruker[] = [];

  constructor(private brukerService: BrukerService, private router: Router, private authService: AuthService) { }

  ngOnInit() {
    this.authService.auth.subscribe(auth => {
      if (!auth) this.router.navigate(['/admin'])
    })


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
