import { Component, OnInit } from '@angular/core';
import { BestillingInfo } from 'src/app/interface/bestilling';
import { Lugar } from 'src/app/interface/lugar';
import { Reise } from 'src/app/interface/reise';
import { BestillingInfoService } from 'src/app/service/bestilling-info.service';
import { ReiseService } from 'src/app/service/reise.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  
  
  
  ngOnInit(): void {
    
  }
}

