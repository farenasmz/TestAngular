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
    ProductBookComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'register-login', component: RegisterComponent },
      { path: 'products', component: ProductsComponent },
      { path: 'products-add', component: ProductsFormComponent},
      { path: 'products-edit/:id', component: ProductsFormComponent  },
      { path: 'users', component: UsersComponent, canActivate: [AuthGuardService]},
      { path: 'users-edit/:id', component: UsersFormComponent, canActivate: [AuthGuardService]  },
      { path: 'users-add', component: UsersFormComponent, canActivate: [AuthGuardService]  },
      { path: 'product-book', component: ProductBookComponent},
    ])
  ],
  providers: [
    AuthGuardService,
    UserService,
    ProductService,
    ProductBookService,
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
