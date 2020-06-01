import { Component, OnInit, ViewChild, ChangeDetectorRef, Input, ElementRef, EventEmitter,Output} from '@angular/core';
import { ConfirmWindowService } from './confirmModal-Service';
import { Router, ActivatedRoute, Params } from '@angular/router';
@Component({
    selector: 'confirm-modal',
    templateUrl: './confirmModal.html',
    providers: [ConfirmWindowService]
    //styleUrls: ['../../../src/app/dashboard/dashboard.component.css'],
})

export class ConfirmModalComponent implements OnInit {
   
    @Input() messageText: string = '';
    @Input() modalType: string;
    @Input() confirmButtonText: string;
    @ViewChild('confirmtemplate') showConfirmModal: ElementRef;
    @ViewChild('errorTemplate') showErrorModal: ElementRef;
    @ViewChild('successTemplate') showSuccessModal: ElementRef;


    @Output() onConfirm: EventEmitter<any> = new EventEmitter<any>();
    @Output() onDecline: EventEmitter<any> = new EventEmitter<any>();

    
    
    constructor(private _confirmModalService: ConfirmWindowService, private cd: ChangeDetectorRef) {
    }

    ngOnInit(): void {
        if (this.modalType == ModalType.Confirm.toString())
            this._confirmModalService.openModal(this.showConfirmModal);
        else if (this.modalType == ModalType.Error.toString())
            this._confirmModalService.openModal(this.showErrorModal);
        else if (this.modalType == ModalType.Success.toString())
            this._confirmModalService.openModal(this.showSuccessModal);
    }

    confirm(id: any) {
        this.onConfirm.emit();
        this.closeModal();
    }

    decline() {
        this.onDecline.emit();
        this.closeModal();
    }
    closeModal() {
        this._confirmModalService.closeModal();
    }
    
}

export enum ModalType {
    Confirm = "Confirm",
    Error = "Error",
    Success = "Success"
}





