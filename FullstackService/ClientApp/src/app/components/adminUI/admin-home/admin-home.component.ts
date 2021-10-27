import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/service/auth.service';

@Component({
  selector: 'app-admin-home',
  templateUrl: './admin-home.component.html',
  styleUrls: ['./admin-home.component.css']
})
export class AdminHomeComponent implements OnInit {

  validUser: boolean = false;

  constructor(private authService: AuthService) { }

  ngOnInit() {
    this.authService.auth.subscribe(auth => {
      this.validUser = auth;
    })
  }



}
