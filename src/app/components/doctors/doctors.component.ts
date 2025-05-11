import { Component, ElementRef, ViewChild } from '@angular/core';
import { RouterLink } from '@angular/router';
import { HttpService } from '../../services/http.service';
import { DoctorModel } from '../../models/doctor-model';
import { CommonModule } from '@angular/common';
import { DepartmentModel } from '../../models/department-model';
import { Constants } from '../../constants';
import { FormsModule, NgForm } from '@angular/forms';
import { FormValidateDirective } from 'form-validate-angular';

@Component({
  selector: 'app-doctors',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule, FormValidateDirective],
  templateUrl: './doctors.component.html',
  styleUrl: './doctors.component.css',
})
export class DoctorsComponent {
  doctors: DoctorModel[] = [];
  departments: DepartmentModel[] = Constants.Departments.sort((a, b) =>
    a.name.localeCompare(b.name)
  );
  createDoctor: DoctorModel = new DoctorModel();

  @ViewChild('addModelCloseBtn') closeModelBtn!: ElementRef<HTMLButtonElement>;

  constructor(private httpService: HttpService) {}

  ngOnInit() {
    this.getAll();
  }

  getAll() {
    this.httpService.post<DoctorModel[]>('doctors/getall', {}, (res) => {
      this.doctors = res.data!;
    });
  }

  addDoctor(form: NgForm) {
    if (!form.valid) {
      return;
    }

    this.httpService.post<string>(
      'doctors/create',
      this.createDoctor,
      (res) => {
        this.getAll();
        if (res.isSuccessful) {
          this.closeModelBtn.nativeElement.click();
          this.createDoctor = new DoctorModel();
          form.resetForm();
        }
      }
    );
  }
}
