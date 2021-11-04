import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Lugar } from 'src/app/interface/lugar';
import { Reise } from 'src/app/interface/reise';
import { AuthService } from 'src/app/service/auth.service';
import { LugarService } from 'src/app/service/lugar.service';
import { ReiseService } from 'src/app/service/reise.service';

@Component({
  selector: 'app-list-lugarer',
  templateUrl: './list-lugarer.component.html',
  styleUrls: ['./list-lugarer.component.css']
})
export class ListLugarerComponent implements OnInit {

  // definerer variabler
  id: number = 0;

  lugarer: Lugar[] = [];

  reise: Reise = {
    strekning: "",
    prisPerGjest: 0,
    prisBil: 0,
    info: "",
    maLugar: true,
    bildeLink: {
      url: ""
    }
  }

  vis: boolean = true;

  constructor(private route: ActivatedRoute,
      private lugarService: LugarService, 
      private reiseService: ReiseService,
      private router: Router, 
      private authService: AuthService) { }

  ngOnInit() {
    // Hvis du ikke er logget inn, så sendes du til /admin
    this.authService.auth.subscribe(auth => {
      if (!auth) this.router.navigate(['/admin'])
    })
    // henter id-en til reisen
    this.id = Number(this.route.snapshot.paramMap.get('id'));

    // henter alle lugarer til denne reisen
    this.lugarService.hentAlleLugererById(this.id).subscribe(lugarer => {
      this.lugarer = lugarer
    })
    // henter reisen med id-en vår
    this.reiseService.hentReiseById(this.id).subscribe(reise => {
      this.reise = reise;
    })
  }

  // viser annen komponent ved klikk på ny lugar
  ny() {
    this.vis = false;
  }

  // ved lagring av lugar
  lagret(lugar: Lugar) {
    this.vis = true;
    this.lugarer.push(lugar);
  }

  avbryt() {
    this.vis = true;
  }

  // ved sletting av en lugar
  slett(id: number) {
    console.log(id)
    //this.lugarer = this.lugarer.filter(l => l.id != id)
    this.lugarService.slettLugar(id).subscribe(lugar => {
      this.lugarer = this.lugarer.filter(l => l.id != lugar.id)
    })
    
  }

  

}
