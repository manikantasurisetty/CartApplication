import { Injectable } from '@angular/core';
import {BehaviorSubject} from 'rxjs';


@Injectable()

export class BsService{
    public favouriteData = new BehaviorSubject<any>(null);
    public selectedProduct=new BehaviorSubject<any>(null);

    updateFavouriteData(product:any){
        this.favouriteData.next(product);

    }

    updateSelectedProduct(product:any){
        this.selectedProduct.next(product);
    }
    currentCartData = this.selectedProduct.asObservable(); 
    currentFavouritData = this.favouriteData.asObservable(); 



}