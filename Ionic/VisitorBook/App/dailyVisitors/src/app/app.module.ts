import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouteReuseStrategy } from '@angular/router';
import { SignaturePadModule } from 'angular2-signaturepad';
import { IonicModule, IonicRouteStrategy, NavParams } from '@ionic/angular';
import { SplashScreen } from '@ionic-native/splash-screen/ngx';
import { StatusBar } from '@ionic-native/status-bar/ngx';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ColumnMode } from '@swimlane/ngx-datatable';
import { Camera, CameraOptions } from '@ionic-native/camera/ngx'
import { VisitorDetailsTabPage } from './visitorDetailsTab/visitorDetailsTab.page';
import { from } from 'rxjs';
import { ReactiveFormsModule,ControlValueAccessor } from '@angular/forms';
import { HttpClientModule } from "@angular/common/http";
import { FileTransfer, FileTransferObject } from '@ionic-native/file-transfer';  
import { File } from '@ionic-native/file/ngx';
/* 
import { Ng2SearchPipeModule} from 'ng2-search-filter';
import {Ng2OrderModule} from 'ng2-order-pipe'
import { NgxPaginationModule} from 'ngx-pagination'; */

@NgModule({
  declarations: [AppComponent],
  entryComponents: [],
  imports: [BrowserModule,
     IonicModule.forRoot(), 
     AppRoutingModule,
     SignaturePadModule,
    ReactiveFormsModule,
    HttpClientModule
    /* ,
    Ng2SearchPipeModule,
    Ng2OrderModule,
    NgxPaginationModule, */
    ],
  providers: [
    StatusBar,
    SplashScreen,
    Camera,
    NavParams,
    VisitorDetailsTabPage, 
    FileTransferObject,
    File,
    { provide: RouteReuseStrategy, useClass: IonicRouteStrategy }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
