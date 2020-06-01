import { Component, OnInit } from '@angular/core';
import { AppService } from '../appservice';
import {urls} from '../urls';
import {BsService} from '../../shared-services/behaviourSubject';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cheppales',
  templateUrl: './cheppales.component.html',
  styleUrls: ['./cheppales.component.scss']
})
export class CheppalesComponent implements OnInit {


  url :any;
  products:any;
  constructor(private product:AppService,private bsService:BsService,private router:Router) { }

  ngOnInit() {
    this.url =urls.cheppales;
    this.product.get(this.url.get).subscribe( (res)=>{
      this.products=res;
      this.products.map((product)=>{
        let myProduct=product;
        product.isVisited=false;
        product.iscartAdded=false;
        return product
      })
    },()=>{})
  }

  addToCart(product:any,type){

    if(type=='favourite'){
      product.isVisited=!product.isVisited
    }
    else {
      if(type=='cart')
      product.iscartAdded=!product.iscartAdded;
    }
  
    
   
    this.bsService.updateFavouriteData(this.products);
  }

  openSelectedProduct(product:any){
    this.bsService.updateSelectedProduct(product);
    this.router.navigateByUrl('/productinfo')
  }
}
