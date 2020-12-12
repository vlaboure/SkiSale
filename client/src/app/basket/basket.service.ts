import { HttpClient } from '@angular/common/http';
import { isNgTemplate } from '@angular/compiler';
import { Injectable } from '@angular/core';
import { FaStackItemSizeDirective } from '@fortawesome/angular-fontawesome';
import { BehaviorSubject } from 'rxjs';
import {map} from 'rxjs/operators'
import { environment } from 'src/environments/environment';
import { Basket, IBasket, IBasketItem, ITotalBasket } from '../shared/models/basket';
import { IProduct } from '../shared/models/product';


@Injectable({
  providedIn: 'root'
})
export class BasketService {
  baseUrl = environment.apiUrl;
  private basketSource = new BehaviorSubject<IBasket>(null);
  private totalBasketSource = new BehaviorSubject<ITotalBasket>(null);
  //$ pour préciser que c'est un observable
  basket$ = this.basketSource.asObservable();
  totalBasket$ = this.totalBasketSource.asObservable();
 
  constructor(private http: HttpClient) { }

  // tslint:disable-next-line: typedef
  getBasket(id: string){
    return this.http.get(`${this.baseUrl}basket?id=${id}`)
    .pipe(
      map((basket: IBasket) => {
        this.basketSource.next(basket);
        this.calculateTotalBasket();
    }));
  }

  setBasket(basket: IBasket){
    return this.http.post(`${this.baseUrl}basket/`,basket)
    .subscribe((response: IBasket) => {
      this.basketSource.next(response);
      this.calculateTotalBasket();
    }, error => {console.log(error)});
  }

// méthode pour récupérer le pannier courant
  getActiveBasket(){
    return this.basketSource.value;
  }

  addItemToBasket(item: IProduct, quantity = 1){
    const itemToAdd : IBasketItem = this.mapProductToBasket(item, quantity);  
    const basket = this.getActiveBasket() ?? this.createBasket();
    //on ajoute au tableau items si n'existe pas sinon on incrémente qte
    basket.items = this.addOrUpdate(basket.items, itemToAdd , quantity);
    // envoi du pannier à l'api
    this.setBasket(basket);
  }

  addOrUpdate(items: IBasketItem[], itemToAdd: IBasketItem, quantity: number): IBasketItem[] {
    // on a un type IBaset avec un id et des items
    const itemInd = items.findIndex(b => b.id === itemToAdd.id);
    if (itemInd !== -1){
      items[itemInd].qte += quantity;
    }else{
      itemToAdd.qte = quantity;
      items.push(itemToAdd);
    }
    return items;
  }

  mapProductToBasket(item: IProduct, quantity: number): IBasketItem {
    // pour copier les champs dans l'item basket
    return {
      id: item.id,
      productName: item.name,
      price: item.price,
      qte: quantity,
      pictureUrl: item.pictureUrl,
      brand: item.productBrand,
      type: item.productType
    };
  }

  createBasket(): IBasket {
    const basket = new Basket();
    // ajouter un item avec id basket dans local storage
    localStorage.setItem('basket_id', basket.id);
    return basket;
  }

  calculateTotalBasket(){
    const basket = this.getActiveBasket();
    const shiping = 0;
    const subTotal = basket.items.reduce((previous,current) => (current.price * current.qte) + previous , 0);
    const total = subTotal + shiping;
    this.totalBasketSource.next({shiping, total, subTotal})
  }

  incrementQte(item: IBasketItem){
    const basket = this.getActiveBasket();
    const index = basket.items.findIndex(a => a.id === item.id);
    basket.items[index].qte++;
    this.setBasket(basket);
  }

  decrementQte(item: IBasketItem){
    const basket = this.getActiveBasket();
    const index = basket.items.findIndex(a => a.id === item.id);    
   //si la qte dans le pannier est inférieur à 0 on supprime l'artile
   //sinon on enregistre
    (basket.items[index].qte--) > 0 
      ? this.setBasket(basket)
      : this.removeItemFromBasket(item);
  }
  removeItemFromBasket(item: IBasketItem) {
    //#region 
    /*
    * on commence par vérifier si on trouve l'article
    * si oui si le nombre d'article hors item.id est > 0 --> enregistrement
    *                                                <=0 on supprime le pannier
    */
    //#endregion
    const basket = this.getActiveBasket();
    if (basket.items.some(x => x.id === item.id)){
      basket.items = basket.items.filter(x => x.id !== item.id);
      if(basket.items.length > 0){
        this.setBasket(basket);
      } else this.deleteBasket(basket);
    }
  }

  deleteBasket(basket: IBasket): unknown {
    return this.http.delete(`${this.baseUrl}/basket?id=${basket.id}`)
  }
}
