import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IProduct } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss']
})
export class ProductDetailComponent implements OnInit {
  product: IProduct;
  constructor(private shopService: ShopService, private activateRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.getProduct();
  }

  getProduct(){
                    // fonctionne aussi avec this.activateRoute.snapshot.params.id
    this.shopService.getProduct(this.activateRoute.snapshot.paramMap.get('id')).subscribe(response =>
      {
        this.product = response;
      }, error => {console.log(error); });
  }
}
