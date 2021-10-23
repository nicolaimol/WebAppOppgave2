import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Lugar } from 'src/app/interface/lugar';
import { ReiseService } from 'src/app/service/reise.service';

@Component({
  selector: 'app-lag-lugar',
  templateUrl: './lag-lugar.component.html',
  styleUrls: ['./lag-lugar.component.css']
})
export class LagLugarComponent implements OnInit {

  @Input() id: number;
  @Output() notifyParent = new EventEmitter<Lugar>();

  lugar: Lugar = {
      id: 0,
      reiseId: 0,
      type: "",
      pris: 0,
      antall: 0
    };

  constructor(private reiseService: ReiseService) { }

  ngOnInit() {
    this.lugar = {
      id: 0,
      reiseId: this.id,
      type: "",
      pris: 0,
      antall: 0
    };
  }

  lagre() {
    console.log(this.lugar);
    this.reiseService.lagreLugar(this.lugar).subscribe(lugar => {
      this.notifyParent.emit(lugar);
    }, err => {
      console.log(err);
    })
  }
}
