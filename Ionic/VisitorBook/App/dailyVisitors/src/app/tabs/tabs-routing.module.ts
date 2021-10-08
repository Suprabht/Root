import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '../auth/auth.guard';
import { TabsPage } from './tabs.page';

const routes: Routes = [
  {
    path: '',
    component: TabsPage,
    children: [
      {
        path: 'visitorDetailsTab',
        loadChildren: () => import('../visitorDetailsTab/visitorDetailsTab.module').then(m => m.VisitorDetailsTabPageModule),
        canLoad:[AuthGuard]
      },
      {
        path: 'visitorListingTab',
        loadChildren: () => import('../visitorListingTab/visitorListingTab.module').then(m => m.VisitorListingTabPageModule),
        canLoad:[AuthGuard]
      },
      {
        path: 'reportsTab',
        loadChildren: () => import('../reportsTab/reportsTab.module').then(m => m.ReportsTabPageModule),
        canLoad:[AuthGuard]
      },
      {
        path: '',
        redirectTo: '/tabs',
        pathMatch: 'full'
      }
    ]
  },
  {
    path: '',
    redirectTo: '/tabs',
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TabsPageRoutingModule {}
