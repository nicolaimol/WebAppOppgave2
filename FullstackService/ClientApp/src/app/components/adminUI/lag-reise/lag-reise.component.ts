import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Bilde } from 'src/app/interface/bilde';
import { Reise } from 'src/app/interface/reise';
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
      url: ""
    },
    info: "",
    maLugar: false
  };

  constructor(private reiseService: ReiseService, private imageService: ImageService, private router: Router) { }

  ngOnInit() {
    this.imageService.getAllBilder().subscribe(bilder => {
      this.bilder = bilder;
      this.reise.bildeLink.url = "./res/Color_Hybrid.jpeg"
    })
  }

  lagre(): void {
    this.reiseService.lagreReise(this.reise).subscribe(reise => {
      console.log('lagret')
    }, err => {
      console.log(err)
    })
  }

  updateImage(files) {
    this.image = files;
  }

  upload() {

    let fileToUpload = <File>this.image[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);

    this.imageService.uploadImage(formData).subscribe(url => {
      console.log(url.url)
      this.reise.bildeLink = url.url;
      this.bilder.push({url: url});
    })
  }

  avbryt() {
    this.router.navigate(['/admin/reiser'])
  }

}
