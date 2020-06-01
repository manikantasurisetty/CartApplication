import { Component, OnInit } from '@angular/core';
import { BsService } from 'src/shared-services/behaviourSubject';

@Component({
  selector: 'app-productinfo',
  templateUrl: './productinfo.component.html',
  styleUrls: ['./productinfo.component.scss']
})
export class ProductinfoComponent implements OnInit {
  product:any;
  productList:any[]=[];
  constructor( private bsService:BsService) { }

  ngOnInit() {
    this.bsService.selectedProduct.subscribe((res)=>{
this.product =res;
    });
    this.bsService.favouriteData.subscribe((productList)=>{
      if(productList){
            this.productList=productList     
      }
    })
  }

  addToCart(){
    this.mapSelectedProduct();
    this.bsService.updateFavouriteData(this.productList)
   
  }

  mapSelectedProduct(){
    this.productList.forEach((product)=>
    {
      if(product.id==this.product.id){           
        product.iscartAdded=true;

      }
      
     
     
    }

     
      )

  }

}
