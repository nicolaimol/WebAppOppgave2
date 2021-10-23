import { Component, Input, OnInit } from '@angular/core';
import { Lugar } from 'src/app/interface/lugar';
import { ReiseService } from 'src/app/service/reise.service';

@Component({
  selector: 'app-item-lugarer',
  templateUrl: './item-lugarer.component.html',
  styleUrls: ['./item-lugarer.component.css']
})
export class ItemLugarerComponent implements OnInit {

  @Input() lugar: Lugar;

  constructor(private reiseService: ReiseService) { }

  ngOnInit() {
  }

  lagre() {
    this.reiseService.updateLugar(this.lugar).subscribe(lugar => {
      console.log(lugar)
    }, err => {
      console.log(err)
    })
  }

}
