import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AccountService } from '../account/account.service';

@Injectable()
export class RegularGuardService implements CanActivate {
  constructor(private accountService: AccountService,
    private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    if (this.accountService.estaLogueado()) {
      return true;
    } else {
      this.router.navigate(['/register-login']);
      return false;
    }

  }
}
