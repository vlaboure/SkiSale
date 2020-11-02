import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IBrand } from '../shared/models/brand';
import { IPagination } from '../shared/models/pagination';
import { IProduct } from '../shared/models/product';
import { shopParams } from '../shared/models/shopParams';
import { IType } from '../shared/models/type';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  @ViewChild('search', {static: true})searchTerm: ElementRef;
  products: IProduct[];
  brands: IBrand[];
  types: IType[];
  totalCount: number;
    // variables pour récupérer l'id si sélectionné
  shopParams = new shopParams();
  sortOptions = [
    {name: 'Alphabetique', value: 'name'},
    {name: 'Prix min-max', value: 'priceAsc'},
    {name: 'Prix max-min', value: 'priceDesc'},
  ];

  constructor(private shopService: ShopService) { }

  ngOnInit() {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  getProducts(){
    this.shopService.getProducts(this.shopParams).subscribe(response => {
      this.products = response.datas;
      this.shopParams.pageIndex = response.pageIndex;
      this.shopParams.itemsPerPage = response.itemsPerPage;
      this.totalCount = response.count;
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
    this.shopParams.brandId = brandId;
    this.shopParams.pageIndex = 1;
    // this.shopParams.search = '';
    // this.searchTerm.nativeElement.value = '';
    this.getProducts();

  }
  onTypeIdSelected(typeId: number){
    // on renseigne le brandId et on lance getProducts
    this.shopParams.typeId = typeId;
    this.shopParams.pageIndex = 1;
    // this.shopParams.search = '';
    // this.searchTerm.nativeElement.value = '';
    this.getProducts();
  }
  onSortSelected(sort: string){
    this.shopParams.sort = sort;
    this.getProducts();
  }

  onPageChange(event: any){
      ////#region
        // on a sorti le paging et utilisé @Output pour l'event
        // pagerComponent retourne donc une page et plus un event
        // on remplace donc
        // this.shopParams.pageIndex = event.page par this.shopParams.pageIndex = event
    ////#endregion
    if (event !== this.shopParams.pageIndex){
      this.shopParams.pageIndex = event;
      this.getProducts();
    }

  }

  onSearch(){
    this.shopParams.search = this.searchTerm.nativeElement.value;
    this.shopParams.pageIndex = 1;
    this.getProducts();
  }
  onReset(){
    this.searchTerm.nativeElement.value = '';
    this.shopParams = new shopParams();
    this.getProducts();
  }
}
