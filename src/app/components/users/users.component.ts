import { Component, ElementRef, ViewChild } from '@angular/core';
import { Constants } from '../../constants';
import { FormsModule, NgForm } from '@angular/forms';
import { HttpService } from '../../services/http.service';
import { SwalService } from '../../services/swal.service';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormValidateDirective } from 'form-validate-angular';
import { UserPipe } from '../../pipe/user.pipe';
import { UserModel } from '../../models/user-model';
import { RoleModel } from '../../models/role-model';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [   CommonModule,
    RouterLink,
    FormsModule,
    FormValidateDirective,
    UserPipe],
  templateUrl: './users.component.html',
  styleUrl: './users.component.css',
})
export class UsersComponent {
  users: UserModel[] = [];
  userToCreate: UserModel = new UserModel();
  userToUpdate: UserModel = new UserModel();
  roles : RoleModel[] = [];

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
    this.getAllRoles();
  }

  getAll() {
    this.httpService.post<UserModel[]>('users/getall', {}, (res) => {
      this.users = res.data!;
    });
  }

  getAllRoles() {
    this.httpService.post<RoleModel[]>('users/getallroles', {}, (res) => {
      this.roles = res.data!;
    });
  }

  add(form: NgForm) {
    if (!form.valid) {
      return;
    }
    this.httpService.post<string>(
      'users/create',
      this.userToCreate,
      (res) => {
        if (res.isSuccessful) {
          this.addModalCloseButton.nativeElement.click();
          this.userToCreate = new UserModel();
          form.resetForm();
          this.swalService.callToast(
            'success',
            'User added successfully',
            Constants.AlertIcons.success
          );
        }
        this.getAll();
      }
    );
  }

  get(user: UserModel) {
    this.userToUpdate = { ...user };
    console.log(this.userToUpdate);
  }

  update(form: NgForm) {
    if (!form.valid) {
      return;
    }
    this.httpService.post<string>(
      'users/update',
      this.userToUpdate,
      (res) => {
        if (res.isSuccessful) {
          this.swalService.callToast(
            'success',
            'User updated successfully',
            Constants.AlertIcons.success
          );
        }
        this.updateModalCloseButton.nativeElement.click();
        this.userToUpdate = new UserModel();
        form.resetForm();
        this.getAll();
      }
    );
  }

  delete(userId: string) {
    this.swalService.callSwal(
      'Are you sure?',
      "You won't be able to revert this!",
      'Yes, delete it!',
      Constants.AlertIcons.warning,
      () => {
        this.httpService.post<string>(
          'users/delete',
          { id: userId },
          (res) => {
            if (res.isSuccessful) {
              this.swalService.callToast(
                'success',
                'User deleted successfully',
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
