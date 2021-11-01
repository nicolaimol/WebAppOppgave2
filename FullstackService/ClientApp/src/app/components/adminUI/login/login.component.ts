import { Component, OnInit } from '@angular/core';
import { Bruker } from 'src/app/interface/bruker';
import { AuthService } from 'src/app/service/auth.service';
import { BrukerService } from 'src/app/service/bruker.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  username: string = "";
  password: string = "";

  constructor(private brukerService: BrukerService, private authService: AuthService) { }

  ngOnInit() {
  }

  login() {
    const b:Bruker = {
      brukernavn:this.username,
      passord:this.password
    };
    
    this.brukerService.validerBruker(b).subscribe(data => {
      console.log(data);
      sessionStorage.setItem("auth", JSON.stringify(data));
      this.authService.changeAuth(true);
      localStorage.setItem('loggInn', "true");
      sessionStorage.setItem('user', data.brukernavn)
      this.authService.changeUser(data.brukernavn);

    },
    err => console.log(err));
  }

}
