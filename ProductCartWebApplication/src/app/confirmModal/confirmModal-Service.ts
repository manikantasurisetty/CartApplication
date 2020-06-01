import { Injectable, TemplateRef, ElementRef, ViewChild } from '@angular/core';
import { BsModalService,ModalOptions } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';

@Injectable()
export class ConfirmWindowService {
    modalRef: BsModalRef;
    modalOptions: ModalOptions;
    @ViewChild('confirmtemplate') showConfirmModal: ElementRef;
    @ViewChild('errorTemplate') showErrorModal: ElementRef;
    @ViewChild('successTemplate') showSuccessModal: ElementRef;

    constructor(private modalService: BsModalService) { }

    openModal(confirmModal:any) {
        this.modalRef = this.modalService.show(confirmModal, Object.assign({},
            this.modalOptions, { class: 'delete-modal-top' }));
    }

    closeModal() {
        this.modalRef.hide();
    }
}