import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { VisitorListingTabPage } from './visitorListingTab.page';
import { ColumnMode } from '@swimlane/ngx-datatable';
const routes: Routes = [
  {
    path: '',
    component: VisitorListingTabPage,
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class VisitorListingTabPageRoutingModule {}
