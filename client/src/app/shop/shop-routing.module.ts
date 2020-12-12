import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ShopComponent } from './shop.component';
import { ProductDetailComponent } from './product-detail/product-detail.component';

const routes: Routes = [
  {path: '', component: ShopComponent},
  // on ajoute un alias qui sera utilisé dans shop-detail
  // pour le nom produit
     // data transmis --> tableau clé valeur
  {path: ':id', component: ProductDetailComponent, data: {breadcrumb: { alias: 'productDetails'}}},
]

@NgModule({
  declarations: [],
  imports: [
    // envoi des routes
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class ShopRoutingModule { }
