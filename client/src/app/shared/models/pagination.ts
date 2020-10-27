import {IProduct} from './product';
export interface IPagination {
    pageIndex: number;
    itemsPerPage: number;
    count: number;
    datas: IProduct[];
  }
