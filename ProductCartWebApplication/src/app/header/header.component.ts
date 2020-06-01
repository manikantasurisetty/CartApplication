import { Component, OnInit } from '@angular/core';
import {headerTabs} from './headerTabs';
import {BsService} from '../../shared-services/behaviourSubject'
import {Router} from '@angular/router';
@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  headerMenu:any;

  cartList:any=[];
  favouriteList:any=[];
  constructor(private bsService:BsService,private route:Router) { }

  ngOnInit() {
      this.headerMenu=headerTabs;
      this.bsService.favouriteData.subscribe((products)=>{

        if(products){
  
    this.cartList=  [...products].filter(product=>product.iscartAdded);
    this.favouriteList= [...products].filter(product=>product.isVisited);

        }
      })

  }
  navigateToCart(route){
    this.route.navigateByUrl(`/list/${route}`)
  }
  navigateTo(header){
    this.headerMenu.map( (item) => {
if(item.id == header.id){
  item.isActive = true
}
else{ item.isActive = false}
return item
    })
    this.route.navigateByUrl(`/${header.name.toLowerCase()}`)

  }

}
