import { Component, OnInit } from '@angular/core';
import { BestillingInfo } from 'src/app/interface/bestilling';
import { Lugar } from 'src/app/interface/lugar';
import { Reise } from 'src/app/interface/reise';
import { BestillingInfoService } from 'src/app/service/bestilling-info.service';
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
  maLugar: boolean = false;
  antallLugarer: number;
  skalBil: boolean = false;
  validBil: boolean = false;
  validAntallBarn: boolean = true;
  validAntallVoksen: boolean = true;

  validTotal:boolean = false; 


  reiser: Reise[] = []
  reiseItem: Reise;
  lugarer: Lugar[] = []
  lugarItem: Lugar;

  strekning: number;
  utreise: string;
  hjemreise: string;
  lugar: number;
  bil: string;
  antallBarn: number = 0;
  antallVoksen: number = 1;
  pris = 0;

  constructor(private reiseService: ReiseService, private bestillingInfoService: BestillingInfoService) {}

  ngOnInit(): void {
    this.reiseService.hentAlleReiser().subscribe(reiser => {
      this.reiser = reiser
    })
  }

  setPris() {
    let pris = 0;
    const rute = this.reiseItem
    pris += this.reiseItem.prisPerGjest
    
    const lugar = this.lugarItem;
    let antall_barn = Number(this.antallBarn);
    let antall_voksen = Number(this.antallVoksen);
    // antall_lugarer depends on if the user has chosen lugar, and that the lugar is valid
    let antall_lugarer = this.skalLugar && this.lugarItem != null ?
        Math.ceil((antall_voksen + antall_barn)/lugar.antall):
        0
    this.antallLugarer = antall_lugarer;

    pris *= antall_voksen + (0.5*antall_barn);

    pris += antall_lugarer !== 0 ? Number(antall_lugarer*(lugar.pris)) : 0

    pris += this.skalBil ? rute.prisBil : 0

    pris *= this.skalHjem ? 2 : 1
    this.pris = pris;

    this.validTotal = this.validStrekning && this.validUtreise
    && (this.validHjemreise || !this.skalHjem) 
    && (this.validLugar || !this.skalLugar) 
    && (this.validBil || !this.skalBil) && this.validAntallBarn 
    && this.validAntallVoksen
  }

  validerStrekning() {
    this.reiseService.hentAlleLugererById(this.strekning).subscribe(lugarer => {
      this.lugarer = lugarer;
    })

    const reise = this.reiser.filter(reiseIn => reiseIn.id == this.strekning)
    this.maLugar = this.skalLugar = reise[0].maLugar;
    this.reiseItem = reise[0];
    this.validStrekning = true;

    this.setPris()
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
    this.setPris()
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

    this.setPris()
  }

  validerLugar() {
    this.validLugar = true;
    this.lugarItem = this.lugarer.filter(lugarIn => lugarIn.id == this.lugar)[0]

    this.setPris()
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

    this.setPris()
  }

  validerVoksne() {
    this.validAntallVoksen = Number(this.antallVoksen) >= 1;

    this.setPris()
  }

  validerBarn() {
    this.validAntallBarn = Number(this.antallBarn) >= 0;

    this.setPris()
  }

  videre() {
    const bestilling: BestillingInfo = {
      utreiseDato: this.utreise,
      hjemreiseDato: this.skalHjem ? null: this.hjemreise,
      pris: this.pris,
      registreringsnummer: this.skalBil? this.bil : null,
      antallLugarer: this.antallLugarer,
      lugar: this.skalLugar ? this.lugarItem: null,
      antall_barn: this.antallBarn,
      antall_voksen: this.antallVoksen,
      reiseId: this.strekning,
    }

    this.bestillingInfoService.changeBestilling(bestilling);
  }



}

