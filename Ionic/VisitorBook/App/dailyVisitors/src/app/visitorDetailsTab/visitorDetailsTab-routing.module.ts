import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { VisitorDetailsTabPage } from './visitorDetailsTab.page';

const routes: Routes = [
  {
    path: '',
    component: VisitorDetailsTabPage,
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class visitorDetailsTabPageRoutingModule {}
