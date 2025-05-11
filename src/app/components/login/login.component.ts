import { Component, ElementRef, ViewChild } from '@angular/core';
import { LoginModel } from '../../models/login.model';
import { FormsModule, NgForm } from '@angular/forms';
import { FormValidateDirective } from 'form-validate-angular';
import { HttpService } from '../../services/http.service';
import { LoginResponseModel } from '../../models/login-response';
import { Router } from '@angular/router';
import { SwalService } from '../../services/swal.service';
import { Constants } from '../../constants';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, FormValidateDirective],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  login: LoginModel = new LoginModel();

  @ViewChild('passwordInput') passwordInput:
    | ElementRef<HTMLInputElement>
    | undefined;

  constructor(private http: HttpService, private router: Router,private swalService : SwalService) {}

  showOrHidePassword() {
    if (this.passwordInput === undefined) {
      return;
    }
    // Check if the password input is of type password
    if (this.passwordInput.nativeElement.type === 'password') {
      this.passwordInput.nativeElement.type = 'text';
    } else {
      this.passwordInput.nativeElement.type = 'password';
    }
  }

  signIn(form: NgForm) {
    if (form.valid) {
      this.http.post<LoginResponseModel>('auth/login', this.login, (res) => {

        if (!res.isSuccessful) {
          this.swalService.callToast(
            'error',
            res.errorMessages?.join(', ') || 'An error occurred',
            Constants.AlertIcons.error
          );
        }
        
        this.swalService.callToast(
          'success',
          'Login successful',
          Constants.AlertIcons.success
        );
        
        localStorage.setItem('token', res.data!.token);
        this.router.navigate(['/']);
      });
    }
  }
}
