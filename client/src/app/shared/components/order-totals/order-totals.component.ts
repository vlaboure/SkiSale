import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { BasketService } from 'src/app/basket/basket.service';
import { ITotalBasket } from '../../models/basket';

@Component({
  selector: 'app-order-totals',
  templateUrl: './order-totals.component.html',
  styleUrls: ['./order-totals.component.scss']
})
export class OrderTotalsComponent implements OnInit {

  totalBasket$: Observable<ITotalBasket>;
  constructor(private basketService: BasketService) { }

  ngOnInit(): void {
    this.totalBasket$ = this.basketService.totalBasket$;
  }

}
