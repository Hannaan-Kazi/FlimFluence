import { Component, OnChanges, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { LoginComponent } from '../login/login.component';
import { MoviesService } from '../movies.service';
import { Movies } from '../models/movies-Model';
import { UserService } from '../Services/user.service';
import { FormControl } from '@angular/forms';
import { BehaviorSubject, Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { NgFor, AsyncPipe } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../Services/auth.service';
import { SignUpComponent } from '../sign-up/sign-up.component';
import { AddMovieComponent } from '../add-movie/add-movie.component';
import { SnackBarService } from '../Services/snack-bar.service';
@Component({
  selector: 'app-tool-bar',
  templateUrl: './tool-bar.component.html',
  styleUrls: ['./tool-bar.component.css'],
})
export class ToolBarComponent implements OnChanges, OnInit {

  data: any;
  loggedIn: any;
  myControl = new FormControl<string | Movies>('');
  options: Movies[] = [];
  filteredOptions: Observable<Movies[]> | undefined;
  isAdmin: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  authz=this.authService.isAdmin()
  // filteredOptions: any;

  ngOnChanges() {}
  private _filter(value: string): Movies[] {
    console.log(value, 'valur');

    const filterValue = value.toLowerCase();
    console.log(this.options, 'in _filter');

    return this.options.filter((option: Movies) =>
      option.title.toLowerCase().includes(filterValue)
    );
  }
  displayFn(movie: Movies): string {
    return movie && movie.title ? movie.title : '';
  }

  ngOnInit(): void {
    //this.getItems()?this.loggedIn =true:null

    // console.log(this.filteredOptions,'filtered');
    // console.log(this.options, "options");

    console.log('is logged in', this.userService.loggedIn.value);

    this.userService.loggedIn.subscribe((data) => {
      this.loggedIn = data;
    });

    console.log('variable', this.loggedIn);
  }

  setFilter() {
    console.log('debug');

    this.filteredOptions = this.myControl.valueChanges.pipe(
      startWith(''),
      map((value) => {
        const title = typeof value === 'string' ? value : value?.title;
        return title ? this._filter(title) : this.options.slice();
        // console.log(opt);
        // return opt;
      })
    );
  }

  constructor(
    public dialog: MatDialog,
    public movieService: MoviesService,
    public userService: UserService,
    public router:Router,
    private authService:AuthService,
    private snackBarService:SnackBarService
  ) {
    this.movieService.getPosts().subscribe({
      next: (response: Movies[]) => {
        this.options = response;
        this.setFilter();
      },
      error: (error: any) => console.log(error),
    });
    console.log(this.loggedIn, 'value in constructor');

    if (this.getItems()){ this.userService.loggedIn.next(true);
    console.log(this.userService.loggedIn.value);
    this.userService.getAllUsers(this.getItems()).subscribe({
      next: (res: any) => {
        console.log(res);
      },
    });
  }
    //console.log(this.data)
  }

  getItems() {
    return localStorage.getItem('AuthJwtToken');
  }

  logOut() {
    //this.loggedIn=false;
    this.userService.loggedIn.next(false);

    console.log(' after log out this.userService.loggedIn ', this.loggedIn);
    console.log('this.loggedIn', this.loggedIn);
    
     localStorage.removeItem('AuthJwtToken');
    this.authz=this.authService.isAdmin()
    this.isAdmin.next(this.authz)
    this.authService.refresh()
    this.snackBarService.openSnackBar("Logged Out successfully", "OK",{duration:3000})

    return null
  }

  OnClickOption(id){
    this.router.navigate(['movie',id])

  }
  OpenSignUpDialog() {
    const dialogRef=this.dialog.open(SignUpComponent, {
      width: '450px',
      disableClose: true,
    });

      dialogRef.afterClosed().subscribe((result) => {
      console.log(
        `after close dialog (login) this.userService.loggedIn ${this.userService.loggedIn}`
      );
      this.authz=this.authService.isAdmin()

    });
  }

  AddMovie(){
    
    const dialogRef=this.dialog.open(AddMovieComponent,{
      // width: '400px',
      minHeight:'400px',
      minWidth:'400px',
     
      disableClose: true,
      data: {
        MovieId: 0,
      },

    })}

  openDialog() {
    const dialogRef = this.dialog.open(LoginComponent, {
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
      console.warn(result, "login result after closed")
      this.authService.refresh()
      console.log("logged in",this.authService.isAdmin());
      
      this.authz=this.authService.isAdmin()

    });
  }


  toWatchLater(){
    this.router.navigate(['WatchLater'])
  }
  toWatched(){
    this.router.navigate(['Watched'])
  }
}
