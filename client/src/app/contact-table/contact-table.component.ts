import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ContactService } from '../services/contact.service';
import { ContactResponse } from '../models/contact-response.model';
import { NgForm } from '@angular/forms';
import { ContactRequest } from '../models/contact-request.model';
import moment from 'moment';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-contact-table',
  templateUrl: './contact-table.component.html',
  styleUrl: './contact-table.component.css',
})
export class ContactTableComponent implements OnInit, OnDestroy {
  contacts: ContactResponse[] = [];
  currentContact: ContactResponse | null = null;

  editableRow: number = -1;
  deletableRow: number = -1;

  filter: string = '';

  @ViewChild('form')
  form!: NgForm;

  private subscription = new Subscription();

  constructor(private readonly contactService: ContactService) {}

  ngOnInit(): void {
    this.subscription.add(
      this.contactService.contacts$.subscribe((contacts) => {
        this.contacts = contacts;
      })
    );
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  onEdit(row: number) {
    this.editableRow = row;
    const contact = this.contacts[this.editableRow - 1];
    this.currentContact = Object.assign({}, contact);
  }

  onEditConfirm() {
    const id = this.contacts[this.editableRow - 1].id;
    let contactRequest: ContactRequest = {
      name: this.form.value.name,
      dateOfBirth: moment(this.form.value.dateOfBirth, 'dd.MM.YYYY').toDate(),
      married: this.form.value.married,
      phone: this.form.value.phone,
      salary: this.form.value.salary,
    };

    this.contactService.editContact(id, contactRequest);

    this.editableRow = -1;
  }

  onEditCancel() {
    this.contacts[this.editableRow - 1] = this.currentContact!;
    this.editableRow = -1;
  }

  onDelete(row: number) {
    this.deletableRow = row;
  }

  onDeleteConfirm() {
    const id = this.contacts[this.deletableRow - 1].id;
    this.contactService.deleteContact(id);

    this.deletableRow = -1;
  }

  onDeleteCancel() {
    this.deletableRow = -1;
  }
}
