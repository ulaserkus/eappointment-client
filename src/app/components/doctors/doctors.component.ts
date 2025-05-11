import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { HttpService } from '../../services/http.service';
import { DoctorModel } from '../../models/doctor-model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-doctors',
  standalone: true,
  imports: [CommonModule,RouterLink],
  templateUrl: './doctors.component.html',
  styleUrl: './doctors.component.css'
})
export class DoctorsComponent {
  doctors : DoctorModel[] = [];
  constructor(private httpService: HttpService) { 
  }

  ngOnInit() {
    this.getAll();
  }

  getAll() {
    this.httpService.post<DoctorModel[]>('doctors/getall', {}, (res) => {
      this.doctors = res.data!; 
    });
  }
}
