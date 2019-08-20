import { Component, OnInit } from '@angular/core';
import { IUser } from './IUser';
import { UserService } from './user.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  Datausers: IUser[];

  constructor(private userService: UserService) { }

  ngOnInit() {
    this.cargarData();
  }

  delete(user: IUser) {
    this.userService.deleteUser(user.Id.toString())
      .subscribe(product => this.cargarData(),
        error => console.error(error));
  }

  cargarData() {
   

    this.userService.getUsers().subscribe((data: IUser[]) => {
      this.Datausers = data;
      console.log("Ahora");
      console.log(this.Datausers);
    });

  }
}
