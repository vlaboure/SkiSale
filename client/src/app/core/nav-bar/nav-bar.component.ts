import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { BasketService } from 'src/app/basket/basket.service';
import { IBasket } from 'src/app/shared/models/basket';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {

  constructor(private basketService: BasketService) { }
  //#region 
  /*
  * on crée un observable dans navbar qui récupére la valeur de
  * l'observable basket$ dans basketService
  */
  basket$: Observable<IBasket>;
  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
  }

}
