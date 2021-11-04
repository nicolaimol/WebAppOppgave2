import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Bruker, BrukerUpdate } from 'src/app/interface/bruker';
import { AuthService } from 'src/app/service/auth.service';
import { BrukerService } from 'src/app/service/bruker.service';
import { ModalSlettComponent } from '../../../modal-slett/modal-slett.component';

@Component({
  selector: 'app-item-bruker',
  templateUrl: './item-bruker.component.html',
  styleUrls: ['./item-bruker.component.css']
})
export class ItemBrukerComponent implements OnInit {

  endre: boolean = false;

  @Input() bruker: BrukerUpdate
  validSlett = false;

  constructor(private brukerService: BrukerService, private modalService: NgbModal, private authService: AuthService) { }

  @Output() notifyParent = new EventEmitter<number>();

  ngOnInit() {
    // deaktiverer slett for aktiv bruker
    this.authService.user.subscribe(data => {
      this.validSlett = this.bruker.brukernavn == data;
    })
  }

  // toggler om endring skal vises
  toggle() {
    this.endre = !this.endre;
  }

  // sender update til backend
  update() {
    this.brukerService.updateBruker(this.bruker, this.bruker.id).subscribe(data => {
      console.log(data)
      this.endre = !this.endre
    }, error => {
      console.log(error)
    })
  }

  // slettemodal vises, om slett sendes id til foreldre component
  slett() {
    const modalRef = this.modalService.open(ModalSlettComponent);
    modalRef.componentInstance.message = "Slett brukeren " + this.bruker.brukernavn + "?"

    modalRef.result.then((result) => {
      if (result == "Lukk"){

      } else {
        this.notifyParent.emit(this.bruker.id);
      }
    })
    
  }

}
