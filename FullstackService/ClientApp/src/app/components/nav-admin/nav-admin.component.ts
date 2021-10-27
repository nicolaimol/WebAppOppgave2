import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/service/auth.service';

@Component({
  selector: 'app-nav-admin',
  templateUrl: './nav-admin.component.html',
  styleUrls: ['./nav-admin.component.css']
})
export class NavAdminComponent implements OnInit {

  hei:boolean = false;

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit() {
    this.authService.auth.subscribe(auth => {
      this.hei = auth;
    })
  }

  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  loggUt(){
    this.authService.loggUt().subscribe(u => {
      this.authService.changeAuth(false);
      localStorage.setItem('loggInn', "false");
    })
  }

}
