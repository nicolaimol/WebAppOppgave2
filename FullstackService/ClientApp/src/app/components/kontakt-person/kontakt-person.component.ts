import { Component, Input, OnInit } from '@angular/core';
import { KontaktPerson } from 'src/app/interface/kunde';

@Component({
  selector: 'app-kontakt-person',
  templateUrl: './kontakt-person.component.html',
  styleUrls: ['./kontakt-person.component.css']
})
export class KontaktPersonComponent implements OnInit {

  @Input() person: KontaktPerson;

  showInput: boolean = false;

  constructor() { }

  ngOnInit() {
    console.log(this.person);
  }

  submit() {
    console.log(this.person);
  }

}
