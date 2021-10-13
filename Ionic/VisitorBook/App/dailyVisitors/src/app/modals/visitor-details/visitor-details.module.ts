import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { VisitorDetailsPageRoutingModule } from './visitor-details-routing.module';

import { VisitorDetailsPage } from './visitor-details.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    VisitorDetailsPageRoutingModule
  ],
  declarations: [VisitorDetailsPage]
})
export class VisitorDetailsPageModule {}
