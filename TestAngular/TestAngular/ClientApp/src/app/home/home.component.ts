import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  message: string;

  constructor() {
    this.message = localStorage.getItem("currentUser");
  }
}
