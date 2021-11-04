import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Lugar } from 'src/app/interface/lugar';
import { LugarService } from 'src/app/service/lugar.service';
import { ReiseService } from 'src/app/service/reise.service';

@Component({
  selector: 'app-lag-lugar',
  templateUrl: './lag-lugar.component.html',
  styleUrls: ['./lag-lugar.component.css']
})
export class LagLugarComponent implements OnInit {

  @Input() id: number;
  @Output() notifyParent = new EventEmitter<Lugar>();

  // Denne er her fram til den hentes fra database
  lugar: Lugar = {
      id: 0,
      reiseId: 0,
      type: "",
      pris: 0,
      antall: 0
    };

  constructor(private lugarService: LugarService) { }

  ngOnInit() {
    this.lugar = {
      id: 0,
      reiseId: this.id,
      type: "",
      pris: 0,
      antall: 0
    };
  }

  // lagrer ny lugar til databasen
  lagre() {
    console.log(this.lugar);
    this.lugarService.lagreLugar(this.lugar).subscribe(lugar => {
      this.notifyParent.emit(lugar);
    }, err => {
      console.log(err);
    })
  }
}
