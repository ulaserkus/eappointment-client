import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { PatientModel } from '../../models/patient-model';
import { HttpService } from '../../services/http.service';
import { SwalService } from '../../services/swal.service';
import { FormsModule, NgForm } from '@angular/forms';
import { Constants } from '../../constants';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormValidateDirective } from 'form-validate-angular';
import { PatientPipe } from '../../pipe/patient.pipe';

@Component({
  selector: 'app-patient',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    FormsModule,
    FormValidateDirective,
    PatientPipe,
  ],
  templateUrl: './patients.component.html',
  styleUrl: './patients.component.css',
})
export class PatientsComponent implements OnInit {
  patients: PatientModel[] = [];
  patientToCreate: PatientModel = new PatientModel();
  patientToUpdate: PatientModel = new PatientModel();

  @ViewChild('addModelCloseBtn')
  addModalCloseButton!: ElementRef<HTMLButtonElement>;
  @ViewChild('updateModelCloseBtn')
  updateModalCloseButton!: ElementRef<HTMLButtonElement>;
  search: string = '';

  constructor(
    private httpService: HttpService,
    private swalService: SwalService
  ) {}

  ngOnInit() {
    this.getAll();
  }

  getAll() {
    this.httpService.post<PatientModel[]>('patients/getall', {}, (res) => {
      this.patients = res.data!;
    });
  }

  addPatient(form: NgForm) {
    if (!form.valid) {
      return;
    }

    this.httpService.post<string>(
      'patients/create',
      this.patientToCreate,
      (res) => {
        if (res.isSuccessful) {
          this.addModalCloseButton.nativeElement.click();
          this.patientToCreate = new PatientModel();
          form.resetForm();
          this.swalService.callToast(
            'success',
            'Patient added successfully',
            Constants.AlertIcons.success
          );
        }
        this.getAll();
      }
    );
  }

  getPatient(doctor: PatientModel) {
    this.patientToUpdate = { ...doctor };
  }

  updatePatient(form: NgForm) {
    if (!form.valid) {
      return;
    }
    this.httpService.post<string>(
      'patients/update',
      this.patientToUpdate,
      (res) => {
        if (res.isSuccessful) {
          this.swalService.callToast(
            'success',
            'Patient updated successfully',
            Constants.AlertIcons.success
          );
        }
        this.updateModalCloseButton.nativeElement.click();
        this.patientToUpdate = new PatientModel();
        form.resetForm();
        this.getAll();
      }
    );
  }

  deletePatient(patientId: string) {
    this.swalService.callSwal(
      'Are you sure?',
      "You won't be able to revert this!",
      'Yes, delete it!',
      Constants.AlertIcons.warning,
      () => {
        this.httpService.post<string>(
          'patients/delete',
          { id: patientId },
          (res) => {
            if (res.isSuccessful) {
              this.swalService.callToast(
                'success',
                'Patient deleted successfully',
                Constants.AlertIcons.success
              );
            }
            this.getAll();
          }
        );
      }
    );
  }
}
