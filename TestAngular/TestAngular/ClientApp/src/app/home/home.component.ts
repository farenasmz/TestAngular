import { Component } from '@angular/core';
import { AccountService } from '../account/account.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  message: string;

  constructor(private accountService: AccountService) {
    this.message = localStorage.getItem("currentUser");
  }

  logout() {
    this.accountService.logout();
    this.message = "";
  }

  estaLogueado() {
    return this.accountService.estaLogueado();
  }
}
