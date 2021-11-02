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
import { ListLugarerComponent } from './components/adminUI/list-lugarer/list-lugarer.component';
import { ItemLugarerComponent } from './components/adminUI/item-lugarer/item-lugarer.component';
import { LagLugarComponent } from './components/adminUI/lag-lugar/lag-lugar.component';
import { ModalSlettComponent } from './components/modal-slett/modal-slett.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FooterUserComponent } from './components/userUI/footer-user/footer-user.component';
import { AdminHomeComponent } from './components/adminUI/admin-home/admin-home.component';
import { ListBestillingerComponent } from './components/adminUI/list-bestillinger/list-bestillinger.component';
import { EndreBestillingComponent } from './components/adminUI/endre-bestilling/endre-bestilling.component';
import { ListLogComponent } from './components/adminUI/list-log/list-log.component';

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
    HentBestillingComponent,
    ListLugarerComponent,
    ItemLugarerComponent,
    LagLugarComponent,
    ModalSlettComponent,
    FooterUserComponent,
    AdminHomeComponent,
    ListBestillingerComponent,
    EndreBestillingComponent,
    ListLogComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    NgbModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent,  children: [
        { path: '', component: BestillComponent, },
        { path: 'reisende', component: ReisendeComponent },
        { path: 'bestilling', component: VisBestillingComponent },
      ]},
      { path: 'admin', component: AdminComponent, children: [
        { path: '', component: AdminHomeComponent},
        { path: 'reiser', component: VisreiserComponent},
        { path: 'reiser/hent/:id', component: EndreReiseComponent},
        { path: 'reiser/ny', component: LagReiseComponent},
        { path: 'lugar/:id', component: ListLugarerComponent},
        { path: 'bestillinger', component: ListBestillingerComponent},
        { path: 'bestillinger/:id', component: EndreBestillingComponent},
        { path: 'log', component: ListLogComponent},
      ]},




    ])
  ],
  providers: [],
  bootstrap: [AppComponent],
  entryComponents:[ModalSlettComponent]
})
export class AppModule { }
