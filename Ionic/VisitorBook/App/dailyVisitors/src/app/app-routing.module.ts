import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms'
import { AuthGuard } from './auth/auth.guard';

const routes: Routes = [
  {
    path: '',
    redirectTo:'tabs',
    pathMatch: 'full'
  },
  {
    path: 'auth',
    loadChildren: () => import('./auth/auth.module').then( m => m.AuthPageModule)
  },
  {
    path:'tabs',
    loadChildren:() => import('./tabs/tabs.module').then(m=>m.TabsPageModule),
    canLoad:[AuthGuard]
    
  },
  {
    path:'visitorDetailsTab',
    loadChildren:() =>import('./visitorDetailsTab/visitorDetailsTab.module').then(m=>m.VisitorDetailsTabPageModule),
    canLoad:[AuthGuard]
  }
];
@NgModule({
  imports: [ReactiveFormsModule,
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {}
