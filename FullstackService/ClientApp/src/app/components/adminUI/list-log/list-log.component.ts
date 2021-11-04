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
    // sender bruker til /admin om den ikke er logget inn
    // henter logg hvis innlogget
    // setter standar filter valg
    this.authService.auth.subscribe(auth => {
      if (!auth) {
          this.router.navigate(["/admin"])
      }
      else {
        this.logService.hentAlleLog().subscribe(logs => {
        this.logs = logs;
        this.filter();
      })
      }
    })

    
  }

  filter() {
    // filterer på valg av kollone og tekst i input
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
                  return (this.printDate(l.datoEndret).includes(this.sok) )
              })
              break;
          default:
              this.logView = this.logs;
              break;
        }
    }
  }

  // formaterer dato til søkbart format
  printDate(date: Date): string{
    date = new Date(date);
    return `${date.getDate() > 10 ? date.getDate(): "0" + date.getDate()}.${date.getMonth() + 1}.${date.getFullYear()} ${date.getHours() > 10 ? date.getHours() : date.getHours() + 10}:${date.getMinutes() > 10 ? date.getMinutes() : "0" + date.getMinutes()}:${date.getSeconds() > 10 ? date.getSeconds() : "0" + date.getSeconds() }`
  }

}
