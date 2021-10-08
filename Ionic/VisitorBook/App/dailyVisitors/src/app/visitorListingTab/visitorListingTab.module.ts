import { IonicModule } from '@ionic/angular';
import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { VisitorListingTabPage } from './visitorListingTab.page';
import { ExploreContainerComponentModule } from '../explore-container/explore-container.module';

import { VisitorListingTabPageRoutingModule } from './visitorListingTab-routing.module';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';

@NgModule({
  imports: [
    IonicModule,
    CommonModule,
    FormsModule,
    ExploreContainerComponentModule,
    VisitorListingTabPageRoutingModule,
    NgxDatatableModule
  ],
  declarations: [VisitorListingTabPage]
})
export class VisitorListingTabPageModule {}
