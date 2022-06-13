import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CategoryGateway } from './domain/models/category/gateway/category-gateway';
import { LoginGateway } from './domain/models/login/gateway/login-gateway';
import { RegisterGateway } from './domain/models/register/gateway/register-gateway';
import { CategoryApiService } from './infraestructure/driven-adapter/category-api/category-api.service';
import { LoginApiService } from './infraestructure/driven-adapter/login-api/login-api.service';
import { RegisterApiService } from './infraestructure/driven-adapter/register-api/register-api.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AuthInterceptorService } from './infraestructure/driven-adapter/auth-interceptor/auth-interceptor.service';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    NgbModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptorService,
      multi: true
    },
    {
      provide: LoginGateway,
      useClass: LoginApiService
    },
    {
      provide: RegisterGateway,
      useClass: RegisterApiService
    },
    {
      provide: CategoryGateway,
      useClass: CategoryApiService
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
