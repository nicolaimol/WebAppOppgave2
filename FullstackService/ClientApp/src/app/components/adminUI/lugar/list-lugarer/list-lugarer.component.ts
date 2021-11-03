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
    this.authService.auth.subscribe(auth => {
      if (!auth) this.router.navigate(['/admin'])
    })
    
    this.id = Number(this.route.snapshot.paramMap.get('id'));

    this.lugarService.hentAlleLugererById(this.id).subscribe(lugarer => {
      this.lugarer = lugarer
    })

    this.reiseService.hentReiseById(this.id).subscribe(reise => {
      this.reise = reise;
    })
  }

  ny() {
    this.vis = false;
  }

  lagret(lugar: Lugar) {
    this.vis = true;
    this.lugarer.push(lugar);
  }

  avbryt() {
    this.vis = true;
  }

  slett(id: number) {
    console.log(id)
    this.lugarer = this.lugarer.filter(l => l.id != id)
    /*
    this.lugarService.slettLugar(id).subscribe(lugar => {
      this.lugarer = this.lugarer.filter(l => l.id != lugar.id)
    })
    */
  }

  

}