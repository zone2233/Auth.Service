import { RouterModule } from "@angular/router";
import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { HttpClientModule } from "@angular/common/http";
import { UnauthenticatedLayoutComponent } from "../layout/unauthenticated.layout/unauthenticated-layout.component";

@NgModule({
  declarations: [
    UnauthenticatedLayoutComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule,
  ],
  exports: [
    HttpClientModule,
    RouterModule
  ]
})


export class CoreModule { }
