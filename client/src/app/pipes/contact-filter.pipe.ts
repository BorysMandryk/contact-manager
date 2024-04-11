import { Pipe, PipeTransform } from '@angular/core';
import { ContactResponse } from '../models/contact-response.model';

@Pipe({
  name: 'contact'
})
export class ContactFilterPipe implements PipeTransform {

  transform(values: ContactResponse[], filter: string): ContactResponse[] {
    if (!filter || filter.length === 0) {
      return values;
    }

    if (values.length === 0) {
      return values;
    }

    return values.filter((value: ContactResponse) => {
      const nameFound =
        value.name.toLowerCase().indexOf(filter.toLowerCase()) !== -1;
      const phoneFound =
        value.phone.toLowerCase().indexOf(filter.toLowerCase()) !== -1;

      if (nameFound || phoneFound) {
        return value;
      }

      return null;
    });
  }

}
