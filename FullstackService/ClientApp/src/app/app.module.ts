import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/userUI/home/home.component';
import { LoginComponent } from './components/adminUI/login/login.component';
import { AdminComponent } from './components/adminUI/admin/admin.component';
import { VisreiserComponent } from './components/adminUI/visreiser/visreiser.component';
import { ReiseItemComponent } from './components/adminUI/reise-item/reise-item.component';
import { ReisendeComponent } from './components/userUI/reisende/reisende.component';
import { BestillComponent } from './components/userUI/bestill/bestill.component';
import { NavAdminComponent } from './components/nav-admin/nav-admin.component';
import { KontaktPersonComponent } from './components/userUI/kontakt-person/kontakt-person.component';
import { KundeComponent } from './components/userUI/kunde/kunde.component';
import { EndreReiseComponent } from './components/adminUI/endre-reise/endre-reise.component';
import { LagReiseComponent } from './components/adminUI/lag-reise/lag-reise.component';
import { VisBestillingComponent } from './components/userUI/vis-bestilling/vis-bestilling.component';
import { HentBestillingComponent } from './components/userUI/hent-bestilling/hent-bestilling.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    LoginComponent,
    AdminComponent,
    VisreiserComponent,
    ReiseItemComponent,
    ReisendeComponent,
    BestillComponent,
    NavAdminComponent,
    KontaktPersonComponent,
    KundeComponent,
    EndreReiseComponent,
    LagReiseComponent,
    VisBestillingComponent,
    HentBestillingComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent,  children: [
        { path: '', component: BestillComponent, },
        { path: 'reisende', component: ReisendeComponent },
        { path: 'bestilling', component: VisBestillingComponent },
      ]},
      { path: 'admin', component: AdminComponent, children: [
        { path: 'reiser', component: VisreiserComponent},
        {path: 'reiser/hent/:id', component: EndreReiseComponent},
        {path: 'reiser/ny', component: LagReiseComponent},
      ]},




    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
