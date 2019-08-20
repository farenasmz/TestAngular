import { Component, OnInit } from '@angular/core';
import { IProduct } from '../products/IProduct';
import { ProductService } from '../products/product.service';
import { ProductBookService } from './product-book.service';
import { IBookProduct } from './IBookProduct';

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

  PlusbookProduct(productID: number) {
    this.book.BookProduct(productID, 1)
      .subscribe(product => this.cargarData(),
        error => console.error(error));
  }

  MinusbookProduct(productID: number) {
    this.book.BookProduct(productID, -1)
      .subscribe(product => this.cargarData(),
        error => console.error(error));
  }

  cargarData() {
    this.productService.getProducts()
      .subscribe(productsWs => this.products = productsWs,
        error => console.error(error));
  }

}
