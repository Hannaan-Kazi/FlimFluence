import { DialogRef } from '@angular/cdk/dialog';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { UserService } from '../Services/user.service';
import { SignUpComponent } from '../sign-up/sign-up.component';
import { JwtHelperService } from '@auth0/angular-jwt';
import {MatSnackBar} from '@angular/material/snack-bar';
import { SnackBarService } from '../Services/snack-bar.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
loginForm:FormGroup


  // loginForm = new FormGroup({
  //   Email: new FormControl('', Validators.email),
  //   Password: new FormControl(''),
  // });


  
  loginError: string= null;
passError: string=null;

  // isModalOpen: boolean = false;

  constructor(
    private formBuilder: FormBuilder,
    public dialog: DialogRef,
    public userService: UserService,
    public matDialog: MatDialog,
    private snackBarService: SnackBarService,
    private jwtHelper:JwtHelperService
  ) {
    this.loginForm=this.formBuilder.group({
      Email:['',[Validators.email]],
      Password:['']
    })
    //console.log(this.loginForm);

  }

  ngOnInit() {
    //this.openDialog()
  }
  onSubmit() {

    // console.log(this.loginForm.value);
    // alert(this.loginForm.value)
    // return null
    
    var a = this.userService.Userlogin(this.loginForm.value).subscribe({
      next: (response: string) => {
        console.log(response);
        if (response == 'Invalid Email.'|| response == 'Wrong Password.' ) {
          this.loginError=response
          // this.snackBarService.openSnackBar("Invalid Credentials", "Try Again",{duration:3000,panelClass: ['loginFailed-snackbar']})
          this.snackBarService.openSnackBar("Invalid Credentials", "Try Again", {
            // duration: 3000,
            panelClass: ['red-snackbar'] 
          })

          return response;
        }
        // else if(response == 'Wrong Password.'){
        //   this.passError=response
        //   return response;

        // } 
        else {
          console.log("res",response);
          

          this.saveToLocalStorage(response);
          const token = localStorage.getItem('AuthJwtToken');
    const decodedToken = this.jwtHelper.decodeToken(token);
    console.log("decoded", decodedToken);
    
    console.log(
      decodedToken ? decodedToken.sub : '');
      this.userService.loggedIn.next(true);
      // console.log(
        //   'on submit this.userService.loggedIn ',
        //   this.userService.loggedIn
        // );
        this.dialog.close();
        this.snackBarService.openSnackBar("Welcome", "OK",{duration:3000})

          return 'Welcome';
        }
      },
      error: (error: any) => {
        this.dialog.close();
        console.error({ 'login failed': error });
      },
    });
    console.log('the value' + JSON.stringify(this.loginForm.value));
    console.log(a);

  }

  saveToLocalStorage(token: string): void {
    // localStorage.setItem(key, JSON.stringify(value));
    localStorage.setItem('AuthJwtToken', 'Bearer ' + token);
  }

  getItem(key: string): any {
    const item = localStorage.getItem(key);
    return item ? JSON.parse(item) : null;
  }

  removeItem(key: string): void {
    localStorage.removeItem(key);
  }

  OpenSignUpDialog() {
    this.matDialog.open(SignUpComponent, {
      width: '450px',
      disableClose: true,
    });
  }
}
