
import {ProductinfoComponent} from './productinfo/productinfo.component';
import { HomeComponent } from './home/home.component';
import {OrderComponent} from './order/order.component';
import {OrderlisComponent} from './orderlis/orderlis.component';
import {Allproducts} from './allproducts/allproducts.component';
import { Routes } from '@angular/router';
import { ListcomponentComponent } from './listcomponent/listcomponent.component';
export const routes: Routes = [
    {path:'', component:HomeComponent},
    {path:'home', component:HomeComponent},
    {path:'productinfo', component:ProductinfoComponent},
    {path:'order', component:OrderComponent},
    {path:'orderlist',component:OrderlisComponent},
    {path:'men',component:Allproducts},
    {path :'women',component:Allproducts},
    {path :"chapals" , component:Allproducts},
    {path:'jewelery', component:Allproducts},
    {path:'list/:listName',component:ListcomponentComponent},
   { path: 'shoes', loadChildren: './shoesmodule/shoesmodule.module#ShoesmoduleModule' },
];