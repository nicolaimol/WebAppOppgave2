import { Component, OnInit } from '@angular/core';
import { Reise } from 'src/app/interface/reise';
import { ReiseService } from 'src/app/service/reise.service';

@Component({
  selector: 'app-visreiser',
  templateUrl: './visreiser.component.html',
  styleUrls: ['./visreiser.component.css']
})
export class VisreiserComponent implements OnInit {

  reiser: Reise[] = [];

  constructor(private reiseService: ReiseService) { }

  ngOnInit() {
    this.reiseService.hentAlleReiser().subscribe(reiser => {
      this.reiser = reiser;
    })
  }

}
