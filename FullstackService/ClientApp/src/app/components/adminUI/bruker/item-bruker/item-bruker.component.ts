import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Bruker, BrukerUpdate } from 'src/app/interface/bruker';
import { BrukerService } from 'src/app/service/bruker.service';

@Component({
  selector: 'app-item-bruker',
  templateUrl: './item-bruker.component.html',
  styleUrls: ['./item-bruker.component.css']
})
export class ItemBrukerComponent implements OnInit {

  endre: boolean = false;

  @Input() bruker: BrukerUpdate

  constructor(private brukerService: BrukerService) { }

  @Output() notifyParent = new EventEmitter<number>();

  ngOnInit() {
  }

  toggle() {
    this.endre = !this.endre;
  }

  update() {
    this.brukerService.updateBruker(this.bruker, this.bruker.id).subscribe(data => {
      console.log(data)
      this.endre = !this.endre
    }, error => {
      console.log(error)
    })
  }

  slett() {
    this.notifyParent.emit(this.bruker.id);
  }

}
