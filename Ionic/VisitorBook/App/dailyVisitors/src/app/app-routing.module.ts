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
  },
  {
    path: 'visitor-details',
    loadChildren: () => import('./modals/visitor-details/visitor-details.module').then( m => m.VisitorDetailsPageModule)
  },
  {
    path: 'changepassword',
    loadChildren: () => import('./changepassword/changepassword.module').then( m => m.ChangepasswordPageModule),
    canLoad:[AuthGuard]
  },
  {
    path: 'searchuser',
    loadChildren: () => import('./modals/searchuser/searchuser.module').then( m => m.SearchuserPageModule)
  },
  {
    path: 'registeruser',
    loadChildren: () => import('./registeruser/registeruser.module').then( m => m.RegisteruserPageModule)
  }
];
@NgModule({
  imports: [ReactiveFormsModule,
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {}
