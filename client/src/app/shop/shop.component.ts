import { Component, OnInit } from '@angular/core';
import { IBrand } from '../shared/models/brand';
import { IPagination } from '../shared/models/pagination';
import { IProduct } from '../shared/models/product';
import { IType } from '../shared/models/type';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {

  products: IProduct[];
  brands: IBrand[];
  types: IType[];
    // variables pour récupérer l'id si sélectionné
  brandSelected: number;
  typeSelected: number;

  constructor(private shopService: ShopService) { }

  ngOnInit() {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  getProducts(){
    this.shopService.getProducts(this.brandSelected, this.typeSelected).subscribe(response => {
      this.products = response.datas;
    }, error => {
      console.log(error);
    });
  }

/*
  #region
    ********************************
    * this.brand = response & this.type = response
    * -- on veut pouvoir réinitialiser le filtre !!!!
    * -- on crée donc un tableau auquel on ajoute un objet
    * -- avec l'id 0 , name= all puis
    * -- on y ajoute avec ...la totalité de la réponse--> response
  #endregion
*/

  getBrands(){// response est un tableau
    this.shopService.getBrands().subscribe(response => {
      this.brands = [{id: 0, name: 'All'}, ...response];
    }, error => {
      console.log(error);
    });
  }

  getTypes(){// response est un tableau
    this.shopService.getTypes().subscribe(response => {
      this.types = [{id: 0, name: 'All'}, ...response];
    }, error => {
      console.log(error);
    });
  }

  onBrandIdSelected(brandId: number){
    // on renseigne le brandId et on lance getProducts
    this.brandSelected = brandId;
    this.getProducts();

  }
  onTypeIdSelected(typeId: number){
    // on renseigne le brandId et on lance getProducts
    this.brandSelected = typeId;
    this.getProducts();
  }
}
