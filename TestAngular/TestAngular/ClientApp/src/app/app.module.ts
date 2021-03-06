import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { RegisterComponent } from './account/register/register.component';
import { AuthGuardService } from './services/auth-guard.service';
import { AccountService } from './account/account.service';
import { LogInterceptorService } from './services/log-interceptor.service';
import { AuthInterceptorService } from './services/auth-interceptor.service';
import { ProductsComponent } from './products/products.component';
import { ProductService } from './products/product.service';
import { ProductsFormComponent } from './products/products-form/products-form.component';
import { UsersComponent } from './users/users.component';
import { UserService } from './users/user.service';
import { UsersFormComponent } from './users/users-form/users-form.component';
import { ProductBookComponent } from './product-book/product-book.component';
import { ProductBookService } from './product-book/product-book.service';
import { ProductPasswordComponent } from './products/product-password/product-password.component';
import { ProductPasswordService } from './products/product-password.service';
import { LogComponent } from './logs/log/log.component';
import { LogService } from './logs/log.service';
import { RegularGuardService } from './services/regular-guard.service';
import { AlertComponent } from './alert/alert.component';
import { AlertService } from './alert/alert.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    RegisterComponent,
    ProductsComponent,
    ProductsFormComponent,
    UsersComponent,
    UsersFormComponent,
    ProductBookComponent,
    ProductPasswordComponent,
    LogComponent,
    AlertComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent, canActivate: [RegularGuardService] },
      { path: 'register-login', component: RegisterComponent },
      { path: 'products', component: ProductsComponent, canActivate: [AuthGuardService]},
      { path: 'products-add', component: ProductsFormComponent, canActivate: [AuthGuardService]  },
      { path: 'products-edit/:id', component: ProductsFormComponent, canActivate: [AuthGuardService] },
      { path: 'users', component: UsersComponent, canActivate: [AuthGuardService] },
      { path: 'users-edit/:id', component: UsersFormComponent, canActivate: [AuthGuardService] },
      { path: 'users-add', component: UsersFormComponent, canActivate: [AuthGuardService] },
      { path: 'product-book', component: ProductBookComponent, canActivate: [RegularGuardService] },
      { path: 'products-password/:id', component: ProductPasswordComponent, canActivate: [AuthGuardService] },
      { path: 'logs', component: LogComponent, canActivate: [AuthGuardService] },
    ])
  ],
  providers: [
    AuthGuardService,
    RegularGuardService,
    UserService,
    AlertService,
    ProductService,
    ProductBookService,
    ProductPasswordService,
    LogService,
    AccountService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: LogInterceptorService,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptorService,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
