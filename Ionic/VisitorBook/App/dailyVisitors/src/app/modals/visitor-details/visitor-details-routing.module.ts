import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { VisitorDetailsPage } from './visitor-details.page';

const routes: Routes = [
  {
    path: '',
    component: VisitorDetailsPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class VisitorDetailsPageRoutingModule {}
