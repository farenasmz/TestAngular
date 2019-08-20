import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup } from '@angular/forms';
import { IUser } from '../../users/IUser';
import { ProductPasswordService } from '../product-password.service';

@Component({
  selector: 'app-product-password',
  templateUrl: './product-password.component.html',
  styleUrls: ['./product-password.component.css']
})
export class ProductPasswordComponent implements OnInit {

  constructor(private fb: FormBuilder,
    private Service: ProductPasswordService,
    private router: Router,
    private activatedRoute: ActivatedRoute) { }

  formGroup: FormGroup;
  userID: number;
  productId: number;

  ngOnInit() {
    this.formGroup = this.fb.group({
      email: '',
      password: '',
    });


    this.activatedRoute.params.subscribe(params => {
      if (params["id"] == undefined) {
        return;
      }
      this.productId = params["id"];
    });
    
  }

  cargarFormulario(user: IUser) {
    this.formGroup.patchValue({
      email: localStorage.getItem("currentUset"),
      password: user.password,
      isActive: user.isActive,
    });
  }

  save() {
    let user: IUser = Object.assign({}, this.formGroup.value);
    this.Service.ResetProduct(this.productId, user)
      .subscribe(persona => this.onSaveSuccess(),
        error => console.error(error));
  }

  onSaveSuccess() {
    this.router.navigate
  }

}
