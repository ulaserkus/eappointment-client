import { SweetAlertIcon } from 'sweetalert2';
import { DepartmentModel } from './models/department-model';

export class Constants {
  public static readonly API_URL: string = 'https://localhost:7022/api';

  public static readonly AlertIcons: Record<SweetAlertIcon, SweetAlertIcon> = {
    success: 'success',
    error: 'error',
    warning: 'warning',
    info: 'info',
    question: 'question',
  };

  public static readonly Departments: DepartmentModel[] = [
    { name: 'KBB', value: 1 },
    { name: 'DENTAL', value: 2 },
    { name: 'INTERNAL_MEDICINE', value: 3 },
    { name: 'OPHTHALMOLOGY', value: 4 },
    { name: 'ORTHOPEDICS', value: 5 },
    { name: 'PSYCHIATRY', value: 6 },
    { name: 'UROLOGY', value: 7 },
    { name: 'NEUROLOGY', value: 8 },
    { name: 'GYNECOLOGY', value: 9 },
    { name: 'CARDIOLOGY', value: 10 },
    { name: 'DERMATOLOGY', value: 11 },
    { name: 'ENT', value: 12 },
    { name: 'PEDIATRICS', value: 13 },
    { name: 'EMERGENCY', value: 14 },
    { name: 'PHYSIOTHERAPY', value: 15 },
    { name: 'RADIOLOGY', value: 16 },
    { name: 'ONCOLOGY', value: 17 },
    { name: 'GASTROENTEROLOGY', value: 18 },
    { name: 'ENDOCRINOLOGY', value: 19 },
    { name: 'NEPHROLOGY', value: 20 },
    { name: 'RHEUMATOLOGY', value: 21 },
    { name: 'ALLERGY', value: 22 },
    { name: 'INFECTIOUS_DISEASES', value: 23 },
    { name: 'ANESTHESIOLOGY', value: 24 },
    { name: 'SURGERY', value: 25 },
    { name: 'PLASTIC_SURGERY', value: 26 },
    { name: 'VASCULAR_SURGERY', value: 27 },
    { name: 'THORACIC_SURGERY', value: 28 },
    { name: 'NEUROSURGERY', value: 29 },
    { name: 'TRANSFUSION_MEDICINE', value: 30 },
    { name: 'PATHOLOGY', value: 31 },
  ];
}
