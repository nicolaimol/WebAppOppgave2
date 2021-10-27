import { Component, OnInit, OnDestroy } from '@angular/core';
import { AuthService } from 'src/app/service/auth.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit, OnDestroy {

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
  }

  ngOnDestroy(){
    window.removeEventListener("storage", this.funcListener);
  }



}
