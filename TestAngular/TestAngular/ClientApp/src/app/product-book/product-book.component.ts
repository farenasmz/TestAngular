import { Component, OnInit } from '@angular/core';
import { IProduct } from '../products/IProduct';
import { ProductService } from '../products/product.service';
import { ProductBookService } from './product-book.service';

@Component({
  selector: 'app-product-book',
  templateUrl: './product-book.component.html',
  styleUrls: ['./product-book.component.css']
})
export class ProductBookComponent implements OnInit {

  constructor(private productService: ProductService, private book: ProductBookService) { }

  products: IProduct[];

  ngOnInit() {
    this.cargarData();
  }

  bookProduct(productID: number) {
    this.book.BookProduct(productID)
      .subscribe(product => this.cargarData(),
        error => console.error(error));
  }

  cargarData() {
    this.productService.getProducts()
      .subscribe(productsWs => this.products = productsWs,
        error => console.error(error));
  }

}
