import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BasketService } from 'src/app/basket/basket.service';
import { IProduct } from 'src/app/shared/models/product';
import { BreadcrumbService } from 'xng-breadcrumb';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss']
})
export class ProductDetailComponent implements OnInit {
  product: IProduct;
  quantity = 1;

  constructor(
    private shopService: ShopService, 
    private basketService: BasketService,
    private activateRoute: ActivatedRoute,
    private breadcrumbService: BreadcrumbService) { 
      breadcrumbService.set('@productDetails','')
    };

  ngOnInit(): void { 
    this.getProduct();
  }

  getProduct(){
    // l'id produit est récupéré avec snapshot.paramMap
      // fonctionne aussi avec this.activateRoute.snapshot.params.id
 
    this.shopService.getProduct(+(this.activateRoute.snapshot.paramMap.get('id'))).subscribe((response) =>
    {
      this.product = response;
      // console.log(this.product.description+' component -- product')
      // console.log(this.product.name+' component -- product')
      // console.log(this.product.price+' component -- product')
      // console.log(this.product.pictureUrl+' component -- product')
      //ici on récupère le nom qu'on met dans alias
      // tableau clé valeur
      this.breadcrumbService.set('@productDetails', response.name)
    }, error => {console.log(error); });
  }

  incrementQte(){
    this.quantity++;
  }

  decrementQte(){
    if(this.quantity > 1) this.quantity--;
  }
  addItemToBasket(){
    this.basketService.addItemToBasket(this.product, this.quantity);
  }
}
