import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { UserService } from '../user.service';
import { Router, ActivatedRoute } from '@angular/router';
import { IUser } from '../IUser';
import { AlertService } from '../../alert/alert.service';

@Component({
  selector: 'app-users-form',
  templateUrl: './users-form.component.html',
  styleUrls: ['./users-form.component.css']
})
export class UsersFormComponent implements OnInit {

  constructor(private fb: FormBuilder,
    private Service: UserService,
    private router: Router,
    private activatedRoute: ActivatedRoute, private alert: AlertService) {
    this.alertService = this.alert;
  }

  alertService: AlertService;
  modoEdicion: boolean = false;
  formGroup: FormGroup;
  userID: number;
  ignorarExistenCambiosPendientes: boolean = false;

  existenCambiosPendientes(): boolean {
    if (this.ignorarExistenCambiosPendientes) { return false; };
    return !this.formGroup.pristine;
  }

  ngOnInit() {
    this.formGroup = this.fb.group({
      email: '',
      password: '',
      isActive: '',
      rol: ''
    });

    this.activatedRoute.params.subscribe(params => {
      if (params["id"] == undefined) {
        return;
      }

      this.modoEdicion = true;
      this.userID = params["id"];
      this.Service.getUser(this.userID.toString())
        .subscribe(product => this.cargarFormulario(product),
          error => this.router.navigate(["/Account"]));
    });

  }

  cargarFormulario(user: IUser) {
    this.formGroup.patchValue({
      email: user.email,
      password: user.password,
      isActive: user.isActive,
      rol: user.rol
    });
  }

  save() {
    this.ignorarExistenCambiosPendientes = true;
    let user: IUser = Object.assign({}, this.formGroup.value);
    console.table(user);

    if (this.modoEdicion) {
      // editar el registro
      user.id = this.userID;
      this.Service.updateUser(user)
        .subscribe(user => this.onSaveSuccess(),
          error => this.alertService.ShowErrorAlert(error));
    } else {
      // agregar el registro.
      if (user.password == "") {
        this.alertService.ShowErrorAlert("Password is required");
        return;
      }
      user.isActive = true;
      this.Service.createUser(user)
        .subscribe(persona => this.onSaveSuccess(),
          error => this.alertService.ShowErrorAlert(error));
    }
  }

  onSaveSuccess() {
    this.alertService.ShowSuccessAlert()
    this.router.navigate
    this.router.navigate(["users"]);
  }
}
