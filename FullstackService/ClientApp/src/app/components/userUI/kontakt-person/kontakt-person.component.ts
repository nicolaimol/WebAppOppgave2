import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { KontaktPerson, Post } from 'src/app/interface/kunde';
import { ReiseService } from 'src/app/service/reise.service';

@Component({
  selector: 'app-kontakt-person',
  templateUrl: './kontakt-person.component.html',
  styleUrls: ['./kontakt-person.component.css']
})
export class KontaktPersonComponent implements OnInit {

  @Input() person: KontaktPerson;
  @Input() avreise: string;
  @Output() notifyParent: EventEmitter<KontaktPerson> = new EventEmitter();

  showInput: boolean = false;

  validFornavn: boolean = false;
  validEtternavn: boolean = false;
  validFoedselsdato: boolean = false;
  validAdresse: boolean = false;
  validPost: boolean = false;
  validTelefon: boolean = false;
  validEpost: boolean = false;

  validTotal: boolean = false;
  validTotalOut: boolean = false;

  post: Post = {
    postNummer: '',
    postSted: ''
  }

  constructor(private reiseService: ReiseService) { }

  ngOnInit() {
    
  }

  avbryt() {
    this.showInput = false;
  }

  submit() {
    this.validTotalOut = true
    this.showInput = false;
    this.notifyParent.emit(this.person);
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
    if (diffYear >= 18) {
      this.validFoedselsdato = true;
    } else {
      this.validFoedselsdato = false;
      console.log(diffYear)
    }

    this.validerTotal();
  }

  validerAdresse() {
    const regAdresse = new RegExp("^[A-ZØÆÅ][a-zøæå]+[ ]([a-zøæåA-Zøæå]+[ ])*[0-9]{1,4}[A-ZÆØÅ]{0,1}$")
    if (regAdresse.test(this.person.adresse)) {
      this.validAdresse = true;
    } else {
      this.validAdresse = false;
    }

    this.validerTotal();
  }

  validerPostNummer() {
    const regPostnummer = new RegExp(`^[0-9]{4}$`)
    if (regPostnummer.test(this.post.postNummer)) {
      this.reiseService.HentPostByPostnummer(this.post.postNummer).subscribe(data => {
        this.post = data;
        this.validPost = true;
        this.person.post = this.post
      }, error => {
        this.validPost = false;
        this.post.postSted = ''
      })
    }
    else {
      this.validPost = false;
      this.post.postSted = ''
    }

    this.validerTotal()
  }

  validerTelefon() {
    const regTelefon = new RegExp(`^[49][0-9]{7}$`);
    if (regTelefon.test(this.person.telefon)) {
      this.validTelefon = true;
    } else {
      this.validTelefon = false;
    }

    this.validerTotal();
  }

  validerEpost() {
    const regEpost = new RegExp(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/)
    if (regEpost.test(this.person.epost)) {
      this.validEpost = true;
    } else {
      this.validEpost = false;
    }

    this.validerTotal();
  } 

  validerTotal() {
    this.validTotal = this.validFornavn && this.validEtternavn && this.validAdresse
    && this.validPost && this.validTelefon && this.validEpost;
  }
}
