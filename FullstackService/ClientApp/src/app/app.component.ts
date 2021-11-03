import { Component, OnDestroy, OnInit } from '@angular/core';
import { AuthService } from './service/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, OnDestroy {
  title = 'app';

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

  ngOnDestroy() {
    window.removeEventListener("storage", this.funcListener);
  }

}
