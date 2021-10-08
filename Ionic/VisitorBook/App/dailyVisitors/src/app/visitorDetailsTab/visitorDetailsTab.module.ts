import { IonicModule } from '@ionic/angular';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { VisitorDetailsTabPage } from './visitorDetailsTab.page';
import { ExploreContainerComponentModule } from '../explore-container/explore-container.module';
import { SignaturePadModule } from 'angular2-signaturepad';

import { visitorDetailsTabPageRoutingModule } from './visitorDetailsTab-routing.module';

@NgModule({
  imports: [
    IonicModule,
    CommonModule,
    FormsModule,
    ExploreContainerComponentModule,
    visitorDetailsTabPageRoutingModule,
    SignaturePadModule,
    ReactiveFormsModule
  ],
  declarations: [VisitorDetailsTabPage]
})
export class VisitorDetailsTabPageModule {}
