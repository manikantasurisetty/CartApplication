import { Component, OnInit,Input ,ChangeDetectorRef} from '@angular/core';
import { AppService } from '../appservice';
import {urls} from '../urls';
import {BsService} from '../../shared-services/behaviourSubject'
import { Router } from '@angular/router';
@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {
  url :any;
  filteredProducts:any;
  @Input() isList:boolean;
  @Input() products:any;
  @Input() groupFilters: Object;
  totalPrice:number;
tempArray:any[]=[];
  
  constructor(private product:AppService,private bsService:BsService,private router:Router,private cd: ChangeDetectorRef) { }

  ngOnInit() {
    this.filteredProducts=this.products;
   // this.tempArray=Object.assign(this.tempArray, this.filteredProducts);
    this.totalPrice=this.getTotalPrice();
    // this.url =urls.products;
    // this.product.get(this.url.get).subscribe( (res)=>{
    //   this.products=res;
    //   this.products.map((product)=>{
    //     let myProduct=product;
    //     product.isVisited=false;
    //     product.iscartAdded=false;
    //     return product
    //   })
    // },()=>{})
  }
  ngOnChanges(): void {
    // if (this.groupFilters) this.filterUserList(this.groupFilters, this.products);
    }
    
  addToCart(product:any,type){

    if(type=='favourite'){
      product.isVisited=!product.isVisited
    }
    else {
      if(type=='cart')
      product.iscartAdded=!product.iscartAdded;
    }
  
    
   
    this.bsService.updateFavouriteData(this.filteredProducts);
  }

  removeFromCart(product:any){
    let index=this.filteredProducts.findIndex(prod=>prod.id==product.id);
    this.filteredProducts.splice(index,1);
 
    let propertyName;
    if(this.router.url=='/list/cart'){
      propertyName='iscartAdded'
    }
    else if(this.router.url=='/list/favourite'){
      propertyName='isVisited'

    }
    this.updateCartData(product,propertyName);
    this.updatePrice(product);
   
  }
  updateCartData(product,propertyName){
    let modifiedProducts;
    this.bsService.favouriteData.subscribe((products)=>{
      modifiedProducts=products;
          products.map((prod)=>{
            if(prod.id==product.id){
              
              product[propertyName]=false;
            }
           
          })})
          this.bsService.updateFavouriteData(modifiedProducts)
 
  }
  updatePrice(product){

    this.totalPrice=this.totalPrice-(product.quantity*product.itemPrice)
  }
  openSelectedProduct(product:any){
    this.bsService.updateSelectedProduct(product);
    this.router.navigateByUrl('/productinfo')
  }
  
//filtercode 
  filterUserList(filters: any, users: any): void {
    this.filteredProducts = this.products; //Reset User List
    const keys = Object.keys(filters);
    const filterUser = user => {
    let result = keys.map(key => {
    // if (!~key.indexOf('cost') && !~key.indexOf('skills')) {
    // if(user[key]) {
    // return String(user[key]).toLowerCase().startsWith(String(filters[key]).toLowerCase())
    // } else {
    // return false;
    // }
    // }
    return true;
    });
    // To Clean Array from undefined if the age is passed so the map will fill the gap with (undefined)
  //  result = result.filter(it => it !== undefined);
    // Filter the Age out from the other filters
    if (filters['Min'] && filters['Max']) {
    if (user['cost']) {
    if (+user['cost'] >= +filters['Min'] && +user['cost'] <= +filters['Max']) {
    result.push(true);
    } else {
    result.push(false);
    }
    } else {
    result.push(false);
    }
    }
    
    if(filters['skills'] && filters['skills'].length>0){
    
      let keepGoing=true;
      let length=filters['skills'].length;
      filters['skills'].forEach((element ,index,array)=> {
      
        if(keepGoing){
    
          if(element['name']==user['type']){
            result.push(true);
            keepGoing = false;
            
          }
          else{
            if(index==length-1){
              result.push(false);
            }
        
            keepGoing=true;
          }
    
        }
       
      
      })
        
    
    }
    let value=result.reduce((acc, cur: any) => { return acc & cur }, 1)
    console.log(value)
    return value;
    
    }
    
    this.filteredProducts = this.products.filter(filterUser);
  
    }
    
    dataChanged(product){
     
      product.cost=product.quantity * product.itemPrice;
      this.totalPrice=this.getTotalPrice()

    
    }
    getTotalPrice(){
      if(this.filteredProducts && this.filteredProducts.length>1)
     return  this.filteredProducts.reduce((prod,acc)=>{return parseFloat(prod.cost)+parseFloat(acc.cost)})
   else{
    if(this.filteredProducts){
      return this.filteredProducts[0].cost
    }
   }
    }
    successText = "order has been success";
    successmodel :boolean = false;
    orderProduct(prod:any){
this.successmodel = true;
this.cd.detectChanges();

    }

}
