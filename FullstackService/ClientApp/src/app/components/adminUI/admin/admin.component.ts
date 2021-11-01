import { Component, OnInit, OnDestroy } from '@angular/core';
import { AuthService } from 'src/app/service/auth.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit, OnDestroy {



  constructor(private authService: AuthService) {
    
  }

  ngOnInit() {
    
  }

  ngOnDestroy(){
    
  }



}
