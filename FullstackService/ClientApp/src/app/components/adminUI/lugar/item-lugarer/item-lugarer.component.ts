import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Lugar } from 'src/app/interface/lugar';
import { LugarService } from 'src/app/service/lugar.service';
import { ReiseService } from 'src/app/service/reise.service';
import { ModalSlettComponent } from '../../../modal-slett/modal-slett.component';

@Component({
  selector: 'app-item-lugarer',
  templateUrl: './item-lugarer.component.html',
  styleUrls: ['./item-lugarer.component.css']
})
export class ItemLugarerComponent implements OnInit {

  @Input() lugar: Lugar;
  @Output() notifyParent = new EventEmitter<number>();

  constructor(private lugarService: LugarService, private modalService: NgbModal) { }

  ngOnInit() {
  }

  // lagrer endringer til lugar i databasen
  lagre() {
    this.lugarService.updateLugar(this.lugar).subscribe(lugar => {
      console.log(lugar)
    }, err => {
      console.log(err)
    })
  }

  // sletter valgt lugar fra databasen
  slett() {
    const modalRef = this.modalService.open(ModalSlettComponent);
    modalRef.componentInstance.message = "Slett lugar " + this.lugar.type

    modalRef.result.then((result) => {
      if (result == "Lukk"){

      } else {
        this.notifyParent.emit(this.lugar.id);
      }
    })

    
  }

}
