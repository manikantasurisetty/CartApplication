import { Component, OnInit,OnChanges, ViewChild } from '@angular/core';
import { BsService } from 'src/shared-services/behaviourSubject';
import {ActivatedRoute} from '@angular/router';
import {ProductsComponent} from '../products/products.component'

@Component({
  selector: 'app-listcomponent',
  templateUrl: './listcomponent.component.html',
  styleUrls: ['./listcomponent.component.scss']
})
export class ListcomponentComponent implements OnInit,OnChanges {
  product
  cartList
  favouriteList
  listName:string;
  products:any;
  @ViewChild(ProductsComponent) productsComponent:ProductsComponent
  constructor(private bsService:BsService,private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.activatedRoute.params.subscribe(routeParams => {
      this.listName = this.activatedRoute.snapshot.params['listName'];
      this.bsService.favouriteData.subscribe((products)=>{
        console.log(products)
              if(products){
          this.cartList=  [...products].filter(product=>product.iscartAdded);
          this.favouriteList= [...products].filter(product=>product.isVisited);
          if(this.listName.toLowerCase()=='cart'){
            this.products=this.cartList        

          }
          else{
            this.products=this.favouriteList
          }
          if(this.products && this.products.length>0){
            this.products.map(prod=>{
              prod.quantity=1
            prod.itemPrice=prod.cost
            return prod
            });
           
          }
          if(this.productsComponent){
            this.productsComponent.filteredProducts=this.products
          }
        
              }
            })
        
        

    });
   
    //console.log(this.listName)
      }

  ngOnChanges(value){
      console.log(value)
  }

}
