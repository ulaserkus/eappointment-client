import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SwalService } from './swal.service';
import { Constants } from '../constants';

@Injectable({
  providedIn: 'root',
})
export class ErrorService {
  constructor(private swal: SwalService) {}

  errorHandler(error: HttpErrorResponse) {
    let errorMessage = 'An unknown error occurred!';

    if (error.error instanceof ErrorEvent) {
      errorMessage = `Error: ${error.error.message}`;
    } else {
      for (const key in error.error.errorMessages) {
        errorMessage = error.error.errorMessages[key] + "\n";
      }
    }

    this.swal.callToast('error', errorMessage, Constants.AlertIcons.error);
  }
}
