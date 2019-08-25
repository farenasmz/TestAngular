import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { IUserInfo } from '../user-info';
import { AccountService } from '../account.service';
import { Router } from '@angular/router';
import { forEach } from '@angular/router/src/utils/collection';
import { AlertService } from '../../alert/alert.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})

export class RegisterComponent implements OnInit {

  alertService: AlertService;
  public ErrorMessage: string;
  public currentUser: string;
  public rol: number;

  constructor(private fb: FormBuilder,
    private accountService: AccountService,
    private router: Router, private alert: AlertService) {
    this.alertService = alert;
  }
  formGroup: FormGroup;

  ngOnInit() {
    this.formGroup = this.fb.group({
      email: '',
      password: '',
      rol: '0',
    });
  }

  loguearse() {
    let userInfo: IUserInfo = Object.assign({}, this.formGroup.value);
    this.accountService.login(userInfo).subscribe(token => this.recibirToken(token),
      error => this.alertService.ShowErrorAlert(error));
    this.currentUser = userInfo.email;
    this.rol = userInfo.rol;
  }

  registrarse() {
    let userInfo: IUserInfo = Object.assign({}, this.formGroup.value);
    if (userInfo.rol == 0) {
      this.alertService.ShowErrorAlert("Rol required!");
      return;
    }

    this.accountService.create(userInfo).subscribe(token => this.recibirToken(token),
      error => this.alertService.ShowErrorAlert(error));
    this.alertService.ShowSuccessAlert();
    localStorage.setItem("currentUser", userInfo.email);
    localStorage.setItem("rol", String(userInfo.rol));
  }

  recibirToken(token) {
    localStorage.setItem('token', token.token);
    localStorage.setItem('tokenExpiration', token.expiration);
    localStorage.setItem('currentUser', this.currentUser);
    localStorage.setItem('rol', String(this.rol));
    this.router.navigate([""]);
  }

  manejarError(error) {
    if (error && error.error) {      
      this.ErrorMessage = (error.error[0].description);
    }
  }
}
