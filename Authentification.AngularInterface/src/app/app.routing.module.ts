import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { APP_BASE_HREF } from '@angular/common';
import { UnauthenticatedCanLoadGuard } from './features/auth/guards/unauthenticated-can-load.guard';
import { UnauthenticatedLayoutComponent } from './layout/unauthenticated.layout/unauthenticated-layout.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: '/unauthenticated/auth/login'
  },
  {
    path: 'unauthenticated',
    canLoad: [UnauthenticatedCanLoadGuard],
    component: UnauthenticatedLayoutComponent,
    children: [
      { path: '', pathMatch: 'full', redirectTo: '/unauthenticated/auth/login' },
      { path: 'auth', loadChildren: () => import('./features/auth/auth.module').then(m => m.AuthModule) },
      { path: '**', pathMatch: 'full', redirectTo: '/unauthenticated/auth/login' }
    ]
  }
]

function getBaseHref() {
  return "";
}

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [{ provide: APP_BASE_HREF, useFactory: getBaseHref }]
})

export class AppRoutingModule { }

