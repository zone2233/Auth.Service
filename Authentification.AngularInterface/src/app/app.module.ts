import { NgModule } from "@angular/core";
import { AppComponent } from "../app.component";
import { AppRoutingModule } from "./app.routing.module";
import { CoreModule } from "./core/core.module";
import { Title } from "@angular/platform-browser";

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    AppRoutingModule,
    CoreModule
  ],
  bootstrap: [
    AppComponent
  ],
  providers: [
    Title
  ]
})


export class AppModule {}
