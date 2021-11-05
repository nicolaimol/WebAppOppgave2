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

  // variabler tilkoblet inputfeltene i html
  username: string = "";
  password: string = "";

  constructor(private brukerService: BrukerService, private authService: AuthService) { }

  ngOnInit() {
  }

  // funksjonen for å logge inn
  login() {

    // lager bruker med brukernavn og passord fra inputfeltene
    const b:Bruker = {
      brukernavn:this.username,
      passord:this.password
    };
    
    // Endrer slik at du blir autorisert. Når du blir autorisert settes denne brukeren.
    // Printer alle eventuelle errorer
    this.brukerService.validerBruker(b).subscribe(data => {
      //sessionStorage.setItem("auth", JSON.stringify(data));
      this.authService.changeAuth(true);
      this.authService.changeUser(data.brukernavn);
      localStorage.setItem('user', data.brukernavn)
      localStorage.setItem('loggInn', "true");
    },
    err => console.log(err));
  }

}
