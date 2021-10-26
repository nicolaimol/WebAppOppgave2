import { Component, OnInit } from '@angular/core';
import { Bruker } from 'src/app/interface/bruker';
import { BrukerService } from 'src/app/service/bruker.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  username: string = "";
  password: string = "";

  constructor(private brukerService: BrukerService) { }

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
    },
    err => console.log(err));
  }

}
