import {v4 as uuidv4 } from 'uuid'

export interface IBasket {
    id: string;
    items: IBasketItem[];
}

export interface IBasketItem {
    id: number;
    productName: string;
    price: number;
    qte: number;
    pictureUrl: string;
    brand: string;
    type: string;
}

export interface ITotalBasket{
    shiping: number;// frais de port
    subTotal: number;// total sans les frais de port
    total: number;
}

export class Basket implements IBasket{
    id = uuidv4();
    items: IBasketItem[] = [];
}
