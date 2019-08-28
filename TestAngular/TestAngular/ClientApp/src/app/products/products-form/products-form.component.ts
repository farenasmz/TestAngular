import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ProductService } from '../product.service';
import { Router, ActivatedRoute } from '@angular/router';
import { IProduct } from '../IProduct';
import { AlertService } from '../../alert/alert.service';

@Component({
  selector: 'app-products-form',
  templateUrl: './products-form.component.html',
  styleUrls: ['./products-form.component.css']
})
export class ProductsFormComponent implements OnInit {

  constructor(private fb: FormBuilder,
    private productsService: ProductService,
    private router: Router,
    private activatedRoute: ActivatedRoute, private alert: AlertService) {
    this.alertService = alert;
  }

  alertService: AlertService;
  modoEdicion: boolean = false;
  formGroup: FormGroup;
  productId: number;
  ignorarExistenCambiosPendientes: boolean = false;

  existenCambiosPendientes(): boolean {
    if (this.ignorarExistenCambiosPendientes) { return false; };
    return !this.formGroup.pristine;
  }

  ngOnInit() {
    this.formGroup = this.fb.group({
      description: '',
      quantity: '',
      isActive:'true'
    });

    this.activatedRoute.params.subscribe(params => {
      if (params["id"] == undefined) {
        return;
      }

      this.modoEdicion = true;
      this.productId = params["id"];
      this.productsService.getProduct(this.productId.toString())
        .subscribe(product => this.cargarFormulario(product),
          error => this.alertService.ShowErrorAlert(error));
    });

  }

  cargarFormulario(product: IProduct) {
    this.formGroup.patchValue({
      description: product.description,
      quantity: product.quantity,
      isActive: product.isActive,
    });
  }

  save() {
    this.ignorarExistenCambiosPendientes = true;
    let product: IProduct = Object.assign({}, this.formGroup.value);

    if (this.modoEdicion) {
      // editar el registro
      product.id = this.productId;
      this.productsService.updateProduct(product)
        .subscribe(product => this.onSaveSuccess(),
          error => this.alertService.ShowErrorAlert(error));
    } else {
      // agregar el registro

      this.productsService.createProduct(product)
        .subscribe(persona => this.onSaveSuccess(),
          error => this.alertService.ShowErrorAlert(error));
    }
  }

  onSaveSuccess() {
    this.alertService.ShowSuccessAlert();
    this.router.navigate(["/products"]);
  }
}
