import { PatientModel } from './patient-model';

export class AppointmentModel {
  id: string = '';
  title: string = '';
  startDate: Date = new Date();
  endDate: Date = new Date();
  patient: PatientModel = new PatientModel();
}
