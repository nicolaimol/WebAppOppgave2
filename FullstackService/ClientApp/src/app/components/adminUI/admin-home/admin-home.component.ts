import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/service/auth.service';

@Component({
  selector: 'app-admin-home',
  templateUrl: './admin-home.component.html',
  styleUrls: ['./admin-home.component.css']
})
export class AdminHomeComponent implements OnInit {

  validUser: boolean = false;
  user: string = ""

  constructor(private authService: AuthService) { }

  ngOnInit() {
    // sjekker om brukeren er logget inn eller ikke
    this.authService.auth.subscribe(auth => {
        this.validUser = auth;
    })

    this.authService.changeUser(localStorage.getItem("user"))

    this.authService.user.subscribe(data => {
      this.user = data;
    })
  }
}
