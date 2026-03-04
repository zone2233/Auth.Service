import { Injectable } from '@angular/core';
import { Route } from '@angular/router';

@Injectable({
    providedIn: 'root'
})
export class UnauthenticatedCanLoadGuard  {
  constructor() { }

    canLoad(route: Route, segments: import("@angular/router").UrlSegment[]) {
      return true;
    }
}
