import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Bestilling } from 'src/app/interface/bestilling';

@Component({
  selector: 'app-vis-bestilling',
  templateUrl: './vis-bestilling.component.html',
  styleUrls: ['./vis-bestilling.component.css']
})
export class VisBestillingComponent implements OnInit {

  bestilling: Bestilling = null;

  constructor(private router: Router) { }

  // init 'bestilling' values
  ngOnInit() {
    this.bestilling = JSON.parse(sessionStorage.getItem('bestilling'));

  }
   // nav back to root
  retur() {
    this.router.navigate(['/'])
  }

  /*
   * showing order
   * content depends if the order exist
   */
  update(bestilling: Bestilling) {
    this.bestilling = bestilling;
    if (bestilling != null) {
      sessionStorage.setItem("bestilling", JSON.stringify(bestilling));
    } else {
      sessionStorage.removeItem("bestilling")
    }
    
  }

}
