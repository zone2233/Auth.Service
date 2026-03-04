import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { LoginServicesModule } from '../services/login.services.module';

@Injectable({
  providedIn: LoginServicesModule
})
export class CanActivateLoginComponentGuard {
  constructor() {
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    return true
  }
}

