import { Component, OnInit } from '@angular/core';
import { Reise } from 'src/app/interface/reise';
import { ReiseService } from 'src/app/service/reise.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  validStrekning: boolean = false;
  validUtreise: boolean = false;
  skalHjem: boolean = false;
  validHjemreise: boolean = false;
  skalLugar: boolean = false;
  validLugar: boolean = false;
  skalBil: boolean = false;
  validBil: boolean = false;
  validAntallBarn: boolean = false;
  validAntallVoksen: boolean = true;

  validTotal = this.validStrekning && this.validUtreise
  && (this.validHjemreise || !this.skalHjem) 
  && (this.validLugar || !this.skalLugar) 
  && (this.validBil || !this.skalBil) && this.validAntallBarn 
  && this.validAntallVoksen


  reiser: Reise[] = []

  strekning: string = "Velg ferjestrekning"
  utreise: string;
  hjemreise: string;
  lugar: string;
  bil: string;
  antallBarn: number;
  antallVoksen: number = 1;

  constructor(private reiseService: ReiseService) {}

  ngOnInit(): void {
    this.reiseService.hentAlleReiser().subscribe(reiser => {
      this.reiser = reiser
    })
  }

  validerStrekning() {
    this.validStrekning = true;
  }

  validerUtreise() {
    const date = this.utreise;
    const hjemreise = this.hjemreise;
    const today = new Date(Date.now()).getDay() + new Date(Date.now()).getMonth() + new Date(Date.now()).getFullYear();
    const chosenDay = new Date(date).getDay() + new Date(date).getMonth() + new Date(date).getFullYear();
    if (new Date(date) > new Date(Date.now()) || today === chosenDay){
      if (hjemreise === "" || new Date(hjemreise) > new Date(date)){
    
        this.validUtreise = false;
      }
      else {
        this.validUtreise = true;
      }
      
    }
    else {
      this.validUtreise = false;
    }
  }

  validerHjemreise() {
    const date = this.hjemreise;
    console.log(date)
    const utreiseDate = this.utreise;
    if (date === ""){
      this.validHjemreise = false;
    
    }
    else if(new Date(date) > new Date(utreiseDate)){
      this.validUtreise = true;
      this.validHjemreise = true;
    }
    else {
      this.validHjemreise = false;
    }
  }

  setHjem() {
    console.log(this.skalHjem)
  }

  validerRegnummer() {
    const regexRegnummer = new RegExp(`^[A-Z]{2}\\s[1-9]{1}[0-9]{4}$`)
    if (regexRegnummer.test(this.bil)) {
      this.validBil = true
    } else {
      this.validBil = false;
    }
  }



}

