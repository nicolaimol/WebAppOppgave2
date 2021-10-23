import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Bilde } from 'src/app/interface/bilde';
import { Reise } from 'src/app/interface/reise';
import { ImageService } from 'src/app/service/image.service';
import { ReiseService } from 'src/app/service/reise.service';

@Component({
  selector: 'app-endre-reise',
  templateUrl: './endre-reise.component.html',
  styleUrls: ['./endre-reise.component.css']
})
export class EndreReiseComponent implements OnInit {

  bilder: Bilde[] = []
  image: Blob;
  //bilde: Bilde;

  id: number;
  reise: Reise = {
    id: 0,
    strekning: "",
    prisPerGjest: 0,
    prisBil: 0,
    bildeLink: {
      url: ""
    },
    info: "",
    maLugar: false
  };

  constructor(private route: ActivatedRoute, 
    private reiseService: ReiseService,
    private imageService: ImageService,
    private router: Router) { }

  ngOnInit() {

    this.imageService.getAllBilder().subscribe(bilder => {
      this.bilder = bilder;
    })

      this.id = Number(this.route.snapshot.paramMap.get('id'));
      this.reiseService.hentReiseById(this.id).subscribe(reise => {
        this.reise = reise
      }, error => {
        this.router.navigate(['/admin/reiser'])
      })
  }

  update():void {
    this.reiseService.updateReise(this.reise).subscribe(reise => {
      this.reise = reise;
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
