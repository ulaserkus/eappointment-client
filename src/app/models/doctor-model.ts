import { Constants } from '../constants';
import { DepartmentModel } from './department-model';

export class DoctorModel {
  id: string = '';
  firstName: string = '';
  lastName: string = '';
  fullName: string = '';
  department: DepartmentModel = new DepartmentModel();
  departmentValue:number = 0;
}
