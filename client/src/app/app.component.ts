import { Component } from '@angular/core';
import { ContactService } from './services/contact.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  file!: File;

  constructor(private readonly contactService: ContactService) {}

  onFileSelected(event: Event) {
    this.file = (event.target as HTMLInputElement).files![0];
  }

  onUpload(event: Event) {
    this.contactService.uploadCsv(this.file);
  }
}
