import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root',
})
export class NotificationService {

  constructor(private snackBar: MatSnackBar) { }

  notify(message: string, action: string = 'Close', duration: number = 3000): void {
    this.snackBar.open(message, action, {
      duration: duration,
    });
  }

  notifyErrorResponse(errorObj: any, action: string = 'Close', duration: number = 3000): void {
    let message = '';
    if (errorObj.errors)
      message = Object.values(errorObj.errors).map(e => e).join(', ');
    else
      message = errorObj.message;

    if (message.length <= 0)
      message = "An error occurred, please try again.";

    this.snackBar.open(message, action, {
      duration: duration,
    });
  }
}
