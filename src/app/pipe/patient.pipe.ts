import { Pipe, PipeTransform } from '@angular/core';
import { PatientModel } from '../models/patient-model';

@Pipe({
  name: 'patient',
  standalone: true,
})
export class PatientPipe implements PipeTransform {
  transform(value: PatientModel[], search: string): PatientModel[] {
    if (!search) {
      return value;
    }

    return value.filter(
      (patient) =>
        patient.fullName.toLocaleLowerCase().includes(search.toLowerCase()) ||
        patient.identityNumber
          .toLocaleLowerCase()
          .includes(search.toLowerCase()) ||
        patient.city.toLocaleLowerCase().includes(search.toLowerCase()) ||
        patient.town.toLocaleLowerCase().includes(search.toLowerCase()) ||
        patient.fullAddress.toLocaleLowerCase().includes(search.toLowerCase())
    );
  }
}
