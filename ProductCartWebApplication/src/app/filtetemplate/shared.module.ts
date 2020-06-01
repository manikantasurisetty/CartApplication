
import {NgModule} from '@angular/core'
import {FiltetemplateComponent} from './filtetemplate.component';


import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@NgModule({
    declarations:[FiltetemplateComponent],
    imports:[FormsModule,ReactiveFormsModule,CommonModule],
    exports:[FiltetemplateComponent]
})
export class SharedModule{

}