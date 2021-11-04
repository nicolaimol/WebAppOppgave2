import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Bestilling } from 'src/app/interface/bestilling';
import { BestillingService } from 'src/app/service/bestilling.service';

@Component({
  selector: 'app-betal',
  templateUrl: './betal.component.html',
  styleUrls: ['./betal.component.css']
})
export class BetalComponent implements OnInit {

  constructor(private router: Router, private bestillingService: BestillingService) { }

  kortholder: string = "";
  validKortholder: boolean = true;
  kortnum: string = "";
  validKortnum: boolean = true;
  mm: string = "";
  validMmm: boolean = true;
  aa: string = "";
  cvv: string = "";
  validCvv: boolean = true;

  done: boolean = false;

  ordre: Bestilling;

  ngOnInit() {
    this.ordre = JSON.parse(sessionStorage.getItem("ordre"))
  }

  
  
  
  tall = ['1', '2', '3', '4', '5', '6', '7', '8', '9', '0', 'ArrowRight']

  sendAar(event) {
    if (this.mm.length === 2 && this.tall.includes(event.key)){
      document.getElementById('aar').focus()
        //$("#aar").focus();
    }
  }
  sendMaaned(event){
    if (this.aa.length === 0 && (event.key === 'Backspace' || event.key === 'ArrowLeft')){
        //$("#maaned").focus();
        document.getElementById("maaned").focus();
    }
  }

  betal() {
    if (this.valider()) {
      this.done = true;

      this.bestillingService.sendBestilling(this.ordre).subscribe(bestilling => {
      console.log(bestilling)
      sessionStorage.setItem("bestilling", JSON.stringify(bestilling))
      this.router.navigate(['/bestilling'])
    })
    }
  }

  valider(): boolean {
    const regNavn = new RegExp(`^([A-ZÆØÅ]{1}[a-zæøå]{0,}\\s{0,1}){2,}$`);
    const regKort = new RegExp(`(^4[0-9]{12}(?:[0-9]{3})?$)|(^(?:5[1-5][0-9]{2}|222[1-9]|22[3-9][0-9]|2[3-6]
[0-9]{2}|27[01][0-9]|2720)[0-9]{12}$)|(3[47][0-9]{13})|(^3(?:0[0-5]|[68][0-9])[0-9]{11}$)|(^6(?:011|5[0-9]{2})[0-9]
{12}$)|(^(?:2131|1800|35\d{3})\d{11}$)`)
    const regCvv = new RegExp(`^[0-9]{3}$`)

    this.validKortholder = regNavn.test(this.kortholder);
    this.validKortnum = regKort.test(this.kortnum);
    this.validCvv = regCvv.test(this.cvv);
    this.validMmm = (Number(this.aa) + 2000 > new Date().getFullYear()
            && Number(this.mm) <= 12 
            && Number(this.mm) > 0) 
        || (Number(this.aa) + 2000 === new Date().getFullYear() 
            && Number(this.mm) <= 12 
            && Number(this.mm) > new Date().getMonth())

    return this.validKortholder && this.validKortnum && this.validCvv && this.validMmm;
  }

}
