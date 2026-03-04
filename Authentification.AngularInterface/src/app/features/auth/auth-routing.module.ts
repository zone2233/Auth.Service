import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { CanActivateLoginComponentGuard } from './guards/can.activate.login.guard';

const routes: Routes = [
    { path: 'login', canActivate: [CanActivateLoginComponentGuard], component: LoginComponent },
];

@NgModule({
    imports: [ RouterModule.forChild(routes) ],
    exports: [ RouterModule ]
})
export class AuthRoutingModule {}
