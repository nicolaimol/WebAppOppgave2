import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Reise } from 'src/app/interface/reise';

@Component({
  selector: 'app-reise-item',
  templateUrl: './reise-item.component.html',
  styleUrls: ['./reise-item.component.css']
})
export class ReiseItemComponent implements OnInit {

  @Input() reise: Reise
  
  constructor(private router: Router) { }

  ngOnInit() {
  }

  endre() {
    this.router.navigate([`/admin/reiser/hent/${this.reise.id}`]);
  }

}
