import { Component, OnInit } from '@angular/core';
import { IProduct } from './IProduct';
import { ProductService } from './product.service';
import { AlertService } from '../alert/alert.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  alertService: AlertService;
  products: IProduct[];

  constructor(private productService: ProductService, private alert: AlertService ) {
    this.alertService = alert;
  }

  ngOnInit() {
    this.cargarData();
  }

  delete(product: IProduct) {
    this.productService.deleteProduct(product.id.toString())
      .subscribe(product => this.cargarData(),
        error => this.alertService.ShowErrorAlert(error));
    this.alertService.ShowSuccessAlert();
  }

  resetProduct(product: IProduct) {
    this.productService.ResetProduct(product)
      .subscribe(product => this.cargarData(),
        error => this.alertService.ShowErrorAlert(error));
    this.alertService.ShowSuccessAlert();
  }

  cargarData() {
    this.productService.getProducts()
      .subscribe(productsWs => this.products = productsWs,
        error => this.alertService.ShowErrorAlert(error));
  }
}
