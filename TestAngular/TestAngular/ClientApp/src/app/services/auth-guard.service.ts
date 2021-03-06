import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AccountService } from '../account/account.service';

@Injectable()
export class AuthGuardService implements CanActivate {

  constructor(private accountService: AccountService,
    private router: Router) { }

  rol: string;

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    this.rol = localStorage.getItem("rol");
    if (this.accountService.estaLogueado() && this.rol == "-1") {
      return true;
    } else {
      this.router.navigate(['/register-login']);
      return false;
    }
  }

}
