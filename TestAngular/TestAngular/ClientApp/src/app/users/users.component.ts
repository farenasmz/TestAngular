import { Component, OnInit } from '@angular/core';
import { IUser } from './IUser';
import { UserService } from './user.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  users: IUser[];

  constructor(private userService: UserService) { }

  ngOnInit() {
    this.cargarData();
  }

  delete(product: IUser) {
    this.userService.deleteUser(product.id.toString())
      .subscribe(product => this.cargarData(),
        error => console.error(error));
  }

  cargarData() {
    this.userService.getUsers()
      .subscribe(Ws => this.users = Ws,
        error => console.error(error));
  }
}
