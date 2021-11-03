import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Log } from 'src/app/interface/log';
import { AuthService } from 'src/app/service/auth.service';
import { LogService } from 'src/app/service/log.service';

@Component({
  selector: 'app-list-log',
  templateUrl: './list-log.component.html',
  styleUrls: ['./list-log.component.css']
})
export class ListLogComponent implements OnInit {

  logs: Log[] = [];

  constructor(private logService: LogService, 
              private authService: AuthService, 
              private router: Router) { }

  ngOnInit() {

    this.authService.auth.subscribe(auth => {
      if (!auth) {
          this.router.navigate(["/admin"])
      }
      else {

      }
    })

    this.logService.hentAlleLog().subscribe(logs => {
      this.logs = logs;
    })
  }

}
