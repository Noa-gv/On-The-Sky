import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'prettyDate',
  standalone: true
})
export class PrettyDatePipe implements PipeTransform {
  transform(value: string | Date): string {
    if (!value) return '';

    const date = new Date(value);

    const options: Intl.DateTimeFormatOptions = {
      day: 'numeric',
      month: 'long',
      year: 'numeric',
      hour: '2-digit',
      minute: '2-digit',
      hour12: false 
    };

    const formatted = date.toLocaleString('en-GB', options);

    return formatted.replace(',', ' at');
  }
}
