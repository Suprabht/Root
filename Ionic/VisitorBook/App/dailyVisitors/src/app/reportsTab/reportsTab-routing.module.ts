import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ReportsTabPage } from './reportsTab.page';

const routes: Routes = [
  {
    path: '',
    component: ReportsTabPage,
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReportsTabPageRoutingModule {}
