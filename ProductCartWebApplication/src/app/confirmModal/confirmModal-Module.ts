import { NgModule, Directive } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { BsDropdownModule, ModalModule } from 'ngx-bootstrap';
import { ConfirmModalComponent } from './confirmModal-Component';
import { ConfirmWindowService } from './confirmModal-Service';

@NgModule({
    imports: [CommonModule,
        FormsModule,
        ReactiveFormsModule,
        BsDropdownModule.forRoot(),
        ModalModule.forRoot(),
    ],
    exports: [ConfirmModalComponent],
    declarations: [ConfirmModalComponent],
    bootstrap: [ConfirmModalComponent],
    providers: [ConfirmWindowService]

})
export class ConfirmModalModule {
    //static routing = routing;
}