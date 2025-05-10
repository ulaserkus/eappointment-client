import { Component, ElementRef, ViewChild } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  @ViewChild('passwordInput') passwordInput:
    | ElementRef<HTMLInputElement>
    | undefined;

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
}
