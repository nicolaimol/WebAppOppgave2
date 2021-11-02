import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Lugar } from 'src/app/interface/lugar';
import { LugarService } from 'src/app/service/lugar.service';
import { ReiseService } from 'src/app/service/reise.service';

@Component({
  selector: 'app-item-lugarer',
  templateUrl: './item-lugarer.component.html',
  styleUrls: ['./item-lugarer.component.css']
})
export class ItemLugarerComponent implements OnInit {

  @Input() lugar: Lugar;
  @Output() notifyParent = new EventEmitter<number>();

  constructor(private lugarService: LugarService) { }

  ngOnInit() {
  }

  lagre() {
    this.lugarService.updateLugar(this.lugar).subscribe(lugar => {
      console.log(lugar)
    }, err => {
      console.log(err)
    })
  }

  slett() {
    this.notifyParent.emit(this.lugar.id);
  }

}
