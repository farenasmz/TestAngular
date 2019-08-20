import { Component, OnInit } from '@angular/core';
import { IProduct } from './IProduct';
import { ProductService } from './product.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  products: IProduct[];

  constructor(private productService: ProductService) { }

  ngOnInit() {
    this.cargarData();
  }

  delete(product: IProduct) {
    this.productService.deleteProduct(product.id.toString())
      .subscribe(product => this.cargarData(),
        error => console.error(error));
  }

  cargarData() {
    this.productService.getProducts()
      .subscribe(productsWs=> this.products = productsWs,
        error => console.error(error));
  }
}
