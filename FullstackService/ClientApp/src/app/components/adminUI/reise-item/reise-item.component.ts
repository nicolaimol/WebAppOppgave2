import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { Reise } from 'src/app/interface/reise';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap'
import { ModalSlettComponent } from '../../modal-slett/modal-slett.component';
import {ReiseService} from '../../../service/reise.service';

@Component({
  selector: 'app-reise-item',
  templateUrl: './reise-item.component.html',
  styleUrls: ['./reise-item.component.css']
})
export class ReiseItemComponent implements OnInit {

  @Input() reise: Reise
  @Output() notifyParent = new EventEmitter<Reise>();
  
  constructor(private router: Router, private modalService: NgbModal, private reiseService: ReiseService) { }

  ngOnInit() {
  }

  endre() {
    this.router.navigate([`/admin/reiser/hent/${this.reise.id}`]);
  }
  fjern() {
    const modalRef = this.modalService.open(ModalSlettComponent)
    modalRef.result.then((result) => {
      if(result === "Lukk"){

      }else{
        this.reiseService.slettReise(this.reise.id).subscribe(data => { 
          this.notifyParent.emit(data);
        }, err => {
          console.log(err);
        })
      }
    })
  }

}
