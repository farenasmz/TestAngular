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

  delete(userid: number) {
    this.userService.deleteUser(userid.toString())
      .subscribe(product => this.cargarData(),
        error => alert(error));
  }

  cargarData() {
   

    this.userService.getUsers().subscribe((data: IUser[]) => {
      this.Datausers = data;
      console.log("Ahora");
      console.log(this.Datausers);
    });

  }
}
