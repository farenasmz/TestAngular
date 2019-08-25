import { Injectable } from '@angular/core';
import swal from 'sweetalert2';

@Injectable()
export class AlertService {

  constructor() { }

  ShowSuccessAlert() {
    swal.fire({
      type: 'success',
      title: 'Your work has been saved',
      showConfirmButton: false,
      timer: 1500
    })
  }

  ShowSuccessAlertMessage(message: string) {
    swal.fire({
      type: 'success',
      title: message,
      showConfirmButton: false,
      timer: 1500
    })
  }

  ShowErrorAlert(message) {
    swal.fire({
      type: 'error',
      title: message.error,
      showConfirmButton: false,
      timer: 2000
    })
  }
}
