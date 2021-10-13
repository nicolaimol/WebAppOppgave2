import { Component, OnInit } from '@angular/core';
import { Reise } from 'src/app/interface/reise';

@Component({
  selector: 'app-reise-item',
  templateUrl: './reise-item.component.html',
  styleUrls: ['./reise-item.component.css']
})
export class ReiseItemComponent implements OnInit {

  reise: Reise = {
    id: 0,
    strekning: "Oslo - Kiel",
    prisPerGjest: 0,
    prisBil: 0,
    bildeLink: "test",
    info: "her er det masse info om reisen",
    maLugar: true
  }
  constructor() { }

  ngOnInit() {
  }

}
