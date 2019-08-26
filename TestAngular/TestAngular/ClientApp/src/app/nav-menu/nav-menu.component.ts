import { Component } from '@angular/core';
import { AccountService } from '../account/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  constructor(private accountService: AccountService,
    private router: Router) { }

  isExpanded = false;
  currentUser = localStorage.getItem("currentUser");
  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  logout() {
    this.accountService.logout();
    this.router.navigate(['/']);
    this.currentUser = "";
  }

  estaLogueado() {
    return this.accountService.estaLogueado();
  }
}
