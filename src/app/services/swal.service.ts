import { Injectable } from '@angular/core';
import Swal, { SweetAlertIcon } from 'sweetalert2';
@Injectable({
  providedIn: 'root',
})
export class SwalService {
  constructor() {}

  callToast(title: string, text: string, icon: SweetAlertIcon) {
    Swal.fire({
      title: title,
      text: text,
      icon: icon,
      timer: 3000,
      showCancelButton: false,
      showCloseButton: false,
      showConfirmButton: false,
      showLoaderOnConfirm: true,
      toast: true,
      position: 'bottom-right',
    });
  }

  callSwal(
    title: string,
    text: string,
    confirmButtonText: string,
    icon: SweetAlertIcon,
    callback: () => void
  ) {
    Swal.fire({
      title: title,
      text: text,
      icon: icon,
      showConfirmButton: true,
      confirmButtonText: confirmButtonText,
      showCancelButton: true,
      cancelButtonText: 'Cancel',
    }).
    then((result) => {

      if (result.isConfirmed) {
        callback();
      }

    });
  }
}
