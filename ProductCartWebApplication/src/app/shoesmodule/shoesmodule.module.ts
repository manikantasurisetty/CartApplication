import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {ShoesComponent} from './shoes/shoes.component';
import { RouterModule, Routes } from '@angular/router';
import {AppService} from '../appservice';
import {SharedModule} from '../filtetemplate/shared.module';


const routes: Routes = [
  { path: '', component: ShoesComponent }
];

export const UserRoutes = RouterModule.forChild(routes);

@NgModule({
  declarations: [ShoesComponent],
  imports: [
   
    CommonModule,
    SharedModule,
    UserRoutes

 

  ],
  providers:[AppService],


})
export class ShoesmoduleModule { }

