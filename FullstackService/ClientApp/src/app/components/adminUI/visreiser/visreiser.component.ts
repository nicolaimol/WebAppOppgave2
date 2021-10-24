import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Reise } from 'src/app/interface/reise';
import { ReiseService } from 'src/app/service/reise.service';

@Component({
  selector: 'app-visreiser',
  templateUrl: './visreiser.component.html',
  styleUrls: ['./visreiser.component.css']
})
export class VisreiserComponent implements OnInit {

  reiser: Reise[] = [];

  constructor(private reiseService: ReiseService, private router: Router) { }

  ngOnInit() {
    this.reiseService.hentAlleReiser().subscribe(reiser => {
      this.reiser = reiser;
    })
  }

  ny():void {
    this.router.navigate(['/admin/reiser/ny'])
  }
  slett(reise:Reise){
    for (let i = 0; i < this.reiser.length; i++){
      if (this.reiser[i].id === reise.id){
        this.reiser.splice(i, 1);
        return;
      }
    }
  }
}
