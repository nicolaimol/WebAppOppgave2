import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Kunde, KundeObj } from 'src/app/interface/kunde';

@Component({
  selector: 'app-kunde',
  templateUrl: './kunde.component.html',
  styleUrls: ['./kunde.component.css']
})
export class KundeComponent implements OnInit {

  @Input() person: Kunde;
  @Input() avreise: string;
  @Input() index: number;
  @Input() type: string;
  @Output() notifyParent: EventEmitter<KundeObj> = new EventEmitter();

  showInput: boolean = false;

  validFornavn: boolean = false;
  validEtternavn: boolean = false;
  validFoedselsdato: boolean = false;

  validTotal: boolean = false;
  validTotalOut: boolean = false;

  constructor() { }

  ngOnInit() {
  }

  submit() {
    this.notifyParent.emit({
      kunde: this.person,
      index: this.index,
      type: this.type
    });
    this.validTotalOut = true;
    this.showInput = false;
  }

  validerFornavn() {
    const regNavn = new RegExp(`^([A-ZÆØÅ]{1}[a-zæøå]{0,}\\s{0,1}){1,}$`);
    if (regNavn.test(this.person.fornavn)) {
      this.validFornavn = true;
    } else {
      this.validFornavn = false;
    }

    this.validerTotal();
  }

  validerEtternavn() {
    const regNavn = new RegExp(`^[A-ZÆØÅ]{1}[a-zæøå]{0,}$`);
    if (regNavn.test(this.person.etternavn)) {
      this.validEtternavn = true;
    } else {
      this.validEtternavn = false;
    }

    this.validerTotal();

  }

  validerFoedselsdato() {
    let date = this.person.foedselsdato;
    let dateObj = new Date(this.avreise).getTime() - new Date(date).getTime()
    let diff = new Date(dateObj)
    diff.setDate(diff.getDate() - 1)
    let diffYear = Math.abs(diff.getUTCFullYear() - 1970);
    if (this.type === 'voksen') {
      if (diffYear >= 18) {
        this.validFoedselsdato = true;
      } else {
        this.validFoedselsdato = false;
      }
    } else {
      if (diffYear >= 0 && diffYear < 18) {
        this.validFoedselsdato = true;
      } else {
        this.validFoedselsdato = false;
      }
    }
    

    this.validerTotal();
  }

  avbryt() {
    this.showInput = false;
  }

  validerTotal() {
    this.validTotal = this.validFornavn && this.validEtternavn;
  }

}
