import { Injectable } from '@angular/core';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class SnackBarService {

  constructor(private snackBar:MatSnackBar) { }

  
  openSnackBar(message: string, action: string, snackBarConfig?: MatSnackBarConfig) {
    this.snackBar.open(message, action, snackBarConfig);
    // this.snackBar.open(message, action, {
    //   duration: 3000, 
    // });
  }
}
