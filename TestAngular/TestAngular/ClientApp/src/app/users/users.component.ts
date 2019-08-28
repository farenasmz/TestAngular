import { Component, OnInit } from '@angular/core';
import { IUser } from './IUser';
import { UserService } from './user.service';
import { AlertService } from '../alert/alert.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  Datausers: IUser[];
  alertService: AlertService;

  constructor(private userService: UserService, private alert: AlertService) {
    this.alertService = alert;
  }

  ngOnInit() {
    this.cargarData();
  }

  delete(userid: number) {
    this.userService.deleteUser(userid.toString())
      .subscribe(product => this.cargarData(),
        error => this.alertService.ShowErrorAlert(error));
    this.alertService.ShowSuccessAlert();
  }

  cargarData() {
    this.userService.getUsers().subscribe((data: IUser[]) => {
      this.Datausers = data;
    }, error => this.alertService.ShowErrorAlert(error));
  }
}
