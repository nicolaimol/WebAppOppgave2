import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Bilde } from 'src/app/interface/bilde';
import { Reise } from 'src/app/interface/reise';
import { AuthService } from 'src/app/service/auth.service';
import { ImageService } from 'src/app/service/image.service';
import { ReiseService } from 'src/app/service/reise.service';

@Component({
  selector: 'app-lag-reise',
  templateUrl: './lag-reise.component.html',
  styleUrls: ['./lag-reise.component.css']
})
export class LagReiseComponent implements OnInit {

  image: Blob;

  bilder: Bilde[] = []

  reise: Reise = {
    strekning: "",
    prisPerGjest: 0,
    prisBil: 0,
    bildeLink: {
      id: 3,
      url: ""
    },
    info: "",
    maLugar: false
  };

  constructor(private reiseService: ReiseService,
    private imageService: ImageService,
    private router: Router,
    private authService: AuthService) { }

  ngOnInit() {
    // sender bruker til /admin om ikke logget inn, henter alle bilder ellers
    this.authService.auth.subscribe(auth => {
      if (!auth) this.router.navigate(['/admin'])
      else {
        this.imageService.getAllBilder().subscribe(bilder => {
          this.bilder = bilder;
          this.reise.bildeLink.url = "./res/Color_Hybrid.jpeg"
        })
      }
    })
    
  }

  // poster ny reise til server
  lagre(): void {
    this.reiseService.lagreReise(this.reise).subscribe(reise => {
      console.log('lagret')
      this.router.navigate(['/admin/reiser'])
    }, err => {
      console.log(err)
    })
  }

  updateImage(files) {
    this.image = files;
  }

  // sender bilde til server, og legger til i listen ved retur og settes aktivt
  upload() {

    let fileToUpload = <File>this.image[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);

    this.imageService.uploadImage(formData).subscribe(bilde => {
      
      this.reise.bildeLink = bilde;
      this.bilder.push(bilde);
    })
  }

  updateId(id: number) {
    this.reise.bildeLink.id = id;
  }

  avbryt() {
    this.router.navigate(['/admin/reiser'])
  }

}
