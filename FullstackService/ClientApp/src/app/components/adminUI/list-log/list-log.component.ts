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
  logView: Log[] = [];
  sok: string = "";
  filterValg: string = "brukernavn";

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
      console.log(logs)
      this.logs = logs;
      this.filter();
    })
  }

  filter() {
    if(this.sok === "") {
      this.logView = this.logs;
    }
    else {
        switch (this.filterValg) {
          case "brukernavn": 
              this.logView = this.logs.filter(l => {
                  return l.bruker.brukernavn.toLocaleLowerCase().startsWith(this.sok.toLocaleLowerCase(), 0)
              })
              break;
          case "beskrivelse":
              this.logView = this.logs.filter(l => {
                  return l.beskrivelse.toLocaleLowerCase().includes(this.sok.toLocaleLowerCase())
              })
              break;
          case "tid":
              this.logView = this.logs.filter(l => {
                  return (l.datoEndret.toLocaleString("dd:MM:yyyy HH:mm:ss").includes(this.sok) )
              })
              break;
          default:
              this.logView = this.logs;
              break;
        }
    }
  }

}
