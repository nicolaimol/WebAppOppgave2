import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Reise } from 'src/app/interface/reise';
import { ReiseService } from 'src/app/service/reise.service';

@Component({
  selector: 'app-endre-reise',
  templateUrl: './endre-reise.component.html',
  styleUrls: ['./endre-reise.component.css']
})
export class EndreReiseComponent implements OnInit {

  id: number;
  reise: Reise = {
    id: 0,
    strekning: "",
    prisPerGjest: 0,
    prisBil: 0,
    bildeLink: "",
    info: "",
    maLugar: false
  };

  constructor(private route: ActivatedRoute, 
    private reiseService: ReiseService,
    private router: Router) { }

  ngOnInit() {
      this.id = Number(this.route.snapshot.paramMap.get('id'));
      this.reiseService.hentReiseById(this.id).subscribe(reise => {
        this.reise = reise
        console.log(reise)
      }, error => {
        this.router.navigate(['/admin/reiser'])
      })
  }

  update():void {
    this.reiseService.updateReise(this.reise).subscribe(reise => {
      this.reise = reise;
    })
  }


  avbryt() {
    this.router.navigate(['/admin/reiser'])
  }

}
