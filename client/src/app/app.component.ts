import { OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { BasketService } from './basket/basket.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})

export class AppComponent implements OnInit{
  title = 'skinet';


  constructor(private basketService: BasketService){}

  ngOnInit(): void {
    //pour charger le pannier au démarrage
    const basketId = localStorage.getItem('basket_id');
    if(basketId) 
      {this.basketService.getBasket(basketId)
      .subscribe(() => {console.log('pannier chargé'); }, err =>{
        console.log(err);
      }); }
  }
}
