import { Component, OnDestroy, OnInit } from '@angular/core';
import { BestillingInfo } from 'src/app/interface/bestilling';
import { Lugar } from 'src/app/interface/lugar';
import { Reise } from 'src/app/interface/reise';
import { AuthService } from 'src/app/service/auth.service';
import { BestillingInfoService } from 'src/app/service/bestilling-info.service';
import { ReiseService } from 'src/app/service/reise.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, OnDestroy{
  
  funcListener: any;
  func(event){
    if(event.key == "loggInn"){
      if(event.newValue == "false"){
        this.authService.changeAuth(false);
      }
      else if (event.newValue == "true"){
        this.authService.changeAuth(true);
      }
    }
  }

  constructor(private authService: AuthService) {
    this.funcListener = this.func.bind(this);
    window.addEventListener("storage", this.funcListener);
  }

  ngOnInit() {
    this.authService.auth.subscribe(auth => {
      if (!auth) {
        this.authService.checkAuth().subscribe(c => {
          this.authService.changeAuth(true);
        })
      }
    })
  }

  ngOnDestroy(){
    window.removeEventListener("storage", this.funcListener);
  }
}

