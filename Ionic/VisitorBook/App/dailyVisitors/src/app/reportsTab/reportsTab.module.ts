import { IonicModule } from '@ionic/angular';
import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ReportsTabPage } from './reportsTab.page';
import { ExploreContainerComponentModule } from '../explore-container/explore-container.module';

import { ReportsTabPageRoutingModule } from './reportsTab-routing.module'

@NgModule({
  imports: [
    IonicModule,
    CommonModule,
    FormsModule,
    ExploreContainerComponentModule,
    RouterModule.forChild([{ path: '', component: ReportsTabPage }]),
    ReportsTabPageRoutingModule,
  ],
  declarations: [ReportsTabPage]
})
export class ReportsTabPageModule {}
