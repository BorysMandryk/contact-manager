import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { ContactResponse } from '../models/contact-response.model';
import { environment } from '../../environments/environment';
import { ContactRequest } from '../models/contact-request.model';

@Injectable({
  providedIn: 'root'
})
export class ContactService {
  contacts$ = new BehaviorSubject<ContactResponse[]>([]);

  private url = environment.apiBaseUrl;

  constructor(private readonly httpClient: HttpClient) {
    this.getContacts();
   }

  getContacts() {
    this.httpClient.get<ContactResponse[]>(`${this.url}/Contact`).subscribe(res => {
      this.contacts$.next(res);
    });
  }

  uploadCsv(file: File) {
    if (!file) {
      console.error('File is null');
    }
    
    const formData = new FormData();
    formData.append('file', file, file.name);
    this.httpClient.post<ContactResponse[]>(`${this.url}/Contact`, formData).subscribe(res => {
      this.contacts$.next(res);
    });
  }

  editContact(id: number, updatedContact: ContactRequest) {
    this.httpClient.put(`${this.url}/Contact/${id}`, updatedContact).subscribe(() => {
      this.getContacts();
    });
  }

  deleteContact(id: number) {
    return this.httpClient.delete(`${this.url}/Contact/${id}`).subscribe(() => {
      this.getContacts();
    });
  }
}
