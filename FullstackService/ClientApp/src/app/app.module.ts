import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { CounterComponent } from './components/counter/counter.component';
import { FetchDataComponent } from './components/fetch-data/fetch-data.component';
import { LoginComponent } from './components/login/login.component';
import { AdminComponent } from './components/admin/admin.component';
import { VisreiserComponent } from './components/visreiser/visreiser.component';
import { ReiseItemComponent } from './components/reise-item/reise-item.component';
import { ReisendeComponent } from './components/reisende/reisende.component';
import { BestillComponent } from './components/bestill/bestill.component';
import { NavAdminComponent } from './components/nav-admin/nav-admin.component';
import { KontaktPersonComponent } from './components/kontakt-person/kontakt-person.component';
import { KundeComponent } from './components/kunde/kunde.component';
import { EndreReiseComponent } from './components/endre-reise/endre-reise.component';
import { LagReiseComponent } from './components/lag-reise/lag-reise.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
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
    LagReiseComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent,  children: [
        { path: '', component: BestillComponent, },
        { path: 'reisende', component: ReisendeComponent },
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
