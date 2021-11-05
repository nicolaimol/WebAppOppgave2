import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Bilde } from 'src/app/interface/bilde';
import { Reise } from 'src/app/interface/reise';
import { AuthService } from 'src/app/service/auth.service';
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
    private router: Router,
    private authService: AuthService) { }

  ngOnInit() {

    // flytter til /admin om bruker ikke er logget inn, henter alle bilder ellers
    this.authService.auth.subscribe(auth => {
      if (!auth) this.router.navigate(['/admin'])
      else {

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
    })

    
  }
  // sender oppdatert reise til server
  update():void {
    this.reiseService.updateReise(this.reise).subscribe(reise => {
      this.router.navigate(['/admin/reiser'])
      this.reise = reise;
    })
  }

  updateImage(files) {
    this.image = files;
  }

  updateId(id:number) {
    this.reise.bildeLink.id = id;
  }

  // sender bilde til server
  upload() {
    let fileToUpload = <File>this.image[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);

    this.imageService.uploadImage(formData).subscribe(bilde => {
      this.bilder.push(bilde);
      console.log(bilde)
      this.reise.bildeLink = bilde;
    })
  }


  avbryt() {
    this.router.navigate(['/admin/reiser'])
  }

}
