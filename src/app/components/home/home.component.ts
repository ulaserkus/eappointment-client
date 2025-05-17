import { Component } from '@angular/core';
import { Constants } from '../../constants';
import { DoctorModel } from '../../models/doctor-model';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule, DatePipe } from '@angular/common';
import { DxSchedulerModule } from 'devextreme-angular';
import { HttpService } from '../../services/http.service';
import { AppointmentModel } from '../../models/appointment-model';
import { CreateAppointmentModel } from '../../models/createappointment-model';
import { FormValidateDirective } from 'form-validate-angular';
import { PatientModel } from '../../models/patient-model';
import { SwalService } from '../../services/swal.service';

declare const $: any;

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    FormsModule,
    CommonModule,
    DxSchedulerModule,
    FormValidateDirective,
  ],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  providers: [DatePipe],
})
export class HomeComponent {
  departments = Constants.Departments.sort((a, b) =>
    a.name.localeCompare(b.name)
  );
  doctors: DoctorModel[] = [];
  appointments: AppointmentModel[] = [];

  selectedDepartmentValue: number = 0;
  selectedDoctorId: string | null = null;
  createAppointmentModel: CreateAppointmentModel = new CreateAppointmentModel();

  constructor(
    private httpService: HttpService,
    private date: DatePipe,
    private swal: SwalService
  ) {}

  onAppointmentFormOpening(e: any) {
    e.cancel = true;
    this.createAppointmentModel.startDate =
      this.date.transform(e.appointmentData.startDate, 'dd.MM.yyyy HH:mm') ??
      '';
    this.createAppointmentModel.endDate =
      this.date.transform(e.appointmentData.endDate, 'dd.MM.yyyy HH:mm') ?? '';
    this.createAppointmentModel.doctorId = this.selectedDoctorId!;
    $('#addModal').modal('show');
  }

  create(form: NgForm) {
    if (form.valid) {
      this.httpService.post<CreateAppointmentModel>(
        'Appointments/Create',
        this.createAppointmentModel,
        (res) => {
          if (res.isSuccessful) {
            this.swal.callToast(
              'Success',
              'Appointment created successfully',
              Constants.AlertIcons.success
            );
            this.getAllAppointments();
            this.createAppointmentModel = new CreateAppointmentModel();
            this.selectedDepartmentValue = 0;
            this.selectedDoctorId = null;
            $('#addModal').modal('hide');
          }
        }
      );
    }
  }

  getPatient() {
    if (
      !this.createAppointmentModel.identityNumber ||
      this.createAppointmentModel.identityNumber.length !== 11
    ) {
      this.clearPatientFields();
      return;
    }
    this.httpService.post<PatientModel>(
      'Appointments/GetPatientByIdentityNumber',
      { identityNumber: this.createAppointmentModel.identityNumber },
      (res) => {
        if (!res.data) {
          this.clearPatientFields();
          return;
        }
        this.createAppointmentModel.patientId = res.data.id;
        this.createAppointmentModel.firstName = res.data.firstName;
        this.createAppointmentModel.lastName = res.data.lastName;
        this.createAppointmentModel.city = res.data.city;
        this.createAppointmentModel.town = res.data.town;
        this.createAppointmentModel.fullAddress = res.data.fullAddress;
      }
    );
  }

  private clearPatientFields() {
    this.createAppointmentModel.patientId = null;
    this.createAppointmentModel.firstName = '';
    this.createAppointmentModel.lastName = '';
    this.createAppointmentModel.city = '';
    this.createAppointmentModel.town = '';
    this.createAppointmentModel.fullAddress = '';
  }

  getAllDoctors() {
    this.selectedDoctorId = null;
    if (this.selectedDepartmentValue === 0) {
      this.doctors = [];
      return;
    }
    this.httpService.post<DoctorModel[]>(
      'Appointments/GetAllDoctorsByDepartment',
      { departmentValue: +this.selectedDepartmentValue },
      (res) => {
        this.doctors = res.data || [];
      }
    );
  }

  getAllAppointments() {
    if (!this.selectedDoctorId || this.selectedDoctorId === 'null') {
      this.appointments = [];
      return;
    }

    this.httpService.post<AppointmentModel[]>(
      'Appointments/GetAllByDoctorId',
      { doctorId: this.selectedDoctorId },
      (res) => {
        this.appointments = res.data || [];
      }
    );
  }

  trackByDepartment(index: number, department: any) {
    return department.value;
  }
  trackByDoctor(index: number, doctor: any) {
    return doctor.id;
  }

  onAppointmentUpdating(e: any) {
    e.cancel = true;

    const updatedAppointment = {
      id: e.oldData.id,
      startDate: this.date.transform(e.newData.startDate, 'dd.MM.yyyy HH:mm'),
      endDate: this.date.transform(e.newData.endDate, 'dd.MM.yyyy HH:mm'),
    };

    this.httpService.post('Appointments/UpdateAppointment', updatedAppointment, (res) => {
      if (res.isSuccessful) {
        this.swal.callToast(
          'Success',
          'Appointment updated successfully',
          Constants.AlertIcons.success
        );
        this.getAllAppointments();
      }
    });
  }
  
  onAppointmentDeleting(e: any) {
    e.cancel = true;
    this.swal.callSwal(
      'Are you sure?',
      "You won't be able to revert this!",
      'Delete',
      Constants.AlertIcons.warning,
      () => {
        this.httpService.post<AppointmentModel>(
          'Appointments/DeleteAppointmentById',
          { id: e.appointmentData.id },
          (res) => {
            if (res.isSuccessful) {
              this.swal.callToast(
                'Success',
                'Appointment deleted successfully',
                Constants.AlertIcons.success
              );
              this.getAllAppointments();
            }
          }
        );
      }
    );
  }
}
