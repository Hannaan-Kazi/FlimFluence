import { DialogRef } from '@angular/cdk/dialog';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { UserService } from '../Services/user.service';
import { formatDate } from '@angular/common';
import { AuthService } from '../Services/auth.service';
import { MatDialog } from '@angular/material/dialog';
import { LoginComponent } from '../login/login.component';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css'],
})
export class SignUpComponent implements OnInit {
  registrationForm!: FormGroup;
  authz=this.authService.isAdmin()

  constructor(
    private fb: FormBuilder,
    public dialog: DialogRef,
    public matDialog:MatDialog,
    public userService: UserService,
    public authService: AuthService
  ) {}

  ngOnInit(): void {
    this.registrationForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required]],
      password: ['', [Validators.required]],
      dateOfBirth: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      pictureUrl: '#',
    });
  }

  openDialog() {
    const dialogRef = this.matDialog.open(LoginComponent, {
      width: '400px',
      height: '400px',
      // width: 'auto',
      // height: 'auto',
      disableClose: true, // to prevent closing by clicking outside the modal
    });

    dialogRef.afterClosed().subscribe((result) => {
      console.log(
        `after close dialog (login) this.userService.loggedIn ${this.userService.loggedIn}`
      );
      console.log("logged in",this.authService.isAdmin());
      

    });
  }

  onSubmit() {

    alert(this.registrationForm.value)
    console.log(this.registrationForm.value);
    
    return null
    if (this.registrationForm.valid) {
      this.registrationForm.value.dateOfBirth = formatDate(
        this.registrationForm.value.dateOfBirth,
        'yyyy-MM-dd',
        'en_US'
      );

      if(this.authz){
        this.userService.AdminSignUp(this.registrationForm.value, localStorage.getItem('AuthJwtToken') ).subscribe({
          next: (o) => {
            console.log('Registration successful!' + o.toString());
          },
          error: (err) => {
            console.log(err);
          },
        });
      }

      this.userService.UserSignUp(this.registrationForm.value).subscribe({
        next: (o) => {
          console.log('Registration successful!' + o.toString());
        },
        error: (err) => {
          console.log(err);
        },
      });
      // console.log('Registration successful!');
      console.log(this.registrationForm.value);
      this.dialog.close();
    } else {
      alert('Form is invalid. Please fill in all required fields.');
    }
  }
}
