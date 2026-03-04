import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginServicesModule } from './services/login.services.module';
import { LoginComponent } from './login/login.component';
import { AuthRoutingModule } from './auth-routing.module';


@NgModule({
  declarations: [LoginComponent],
    imports: [
        CommonModule,
        AuthRoutingModule,
        //MaterialsModule,
        LoginServicesModule
    ]
})
export class AuthModule { }
