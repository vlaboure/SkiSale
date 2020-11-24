import { Component, NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NotFoundComponent } from './core/not-found/not-found.component';
import { ServerErrorComponent } from './core/server-error/server-error.component';
import { TestErrorComponent } from './core/test-error/test-error.component';
import { HomeComponent } from './home/home.component';
import { ProductDetailComponent } from './shop/product-detail/product-detail.component';
import { ShopComponent } from './shop/shop.component';

const routes: Routes = [
  {path: '', component: HomeComponent , data:{breadcrumb:'home'}},
  {path: 'test-error', component: TestErrorComponent, data:{breadcrumb:'Test Error'}},
  {path: 'not-found', component: NotFoundComponent, data:{breadcrumb:'Not Found'}},
  {path: 'server-error', component: ServerErrorComponent, data:{breadcrumb:'Data Error'}},
  {path: 'shop', loadChildren: () => import('./shop/shop.module').
  then(m => m.ShopModule), data:{breadcrub:'Shop'}},
  {path: 'basket', loadChildren: () => import('./basket/basket.module').
  then(m => m.BasketModule), data:{breadcrub:'basket'}},
  {path: '**', redirectTo: 'not-found', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
