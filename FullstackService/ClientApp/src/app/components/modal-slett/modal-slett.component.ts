import { Component, OnInit } from '@angular/core';
import {NgbActiveModal} from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-modal-slett',
  templateUrl: './modal-slett.component.html',
  styleUrls: ['./modal-slett.component.css']
})
export class ModalSlettComponent implements OnInit {

  constructor(public modal: NgbActiveModal) { }

  ngOnInit() {
  }

}
