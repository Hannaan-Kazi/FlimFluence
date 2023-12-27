import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MoviesService } from '../movies.service';
import { Movies } from '../models/movies-Model';
import { Ratings } from '../models/ratings-Model';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { RateComponent } from '../rate/rate.component';
import { MatDialog } from '@angular/material/dialog';
import { LoginComponent } from '../login/login.component';
import { DialogRef } from '@angular/cdk/dialog';
import { WatchedService } from '../Services/watched.service';
import { DomSanitizer } from '@angular/platform-browser';
import { log } from 'console';
import { TestService } from '../Services/test.service';
import { AddMovieComponent } from '../add-movie/add-movie.component';
import { AuthService } from '../Services/auth.service';
import { SnackBarService } from '../Services/snack-bar.service';

@Component({
  selector: 'app-movie',
  templateUrl: './movie.component.html',
  styleUrls: ['./movie.component.css'],
})
export class MovieComponent implements AfterViewInit {
// reader.onloadend = () => {
//   reader.readAsDataURL(this.movieFromDb.image);
//   this.movieFromDb.image=reader.result
// };
// reader.readAsDataURL(blob);
//   const buffer = Buffer.from(movie.image);
// const base64String = buffer.toString('base64');
// console.log("b64",base64String);
// let objectURL = 'data:image/jpeg;base64,' + movie.image;
OnWatchLaterClick(arg0: number) {
throw new Error('Method not implemented.');
}

  loggedIn: any;
  MovieId: number;
  movieFromDb: Movies;
  displayedColumns: string[] = ['comment', 'rating'];
  ratings: Ratings[];
  dataSource = new MatTableDataSource<Ratings>([]);
  testImage:any;
  authz=this.authService.isAdmin()


  @ViewChild(MatPaginator) paginator: MatPaginator;

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }
  OnWatchClick(id: number) {
    this.watchedService.addToWL(id).subscribe({
      next:()=>{
        // this.snackBarservice.openSnackBar("you must be logged in","OK")

        alert("👍")},
    }
    )
  }

  getImage(){

    this.route.paramMap.subscribe((params) => {
      const id = params.get('id');
      console.log('ID from route parameter:', id);
      this.MovieId = parseInt(id);})

    this.testService.getImageFromMovieTable(this.MovieId).subscribe((e)=>{
      console.warn(e);
      console.log("apna wala",e.image);
      this.movieFromDb=e
      this.movieFromDb.image='data:image/jpeg;base64,'+ e.image
      this.setImage();    
      console.log("after set");
        
    })
  }

  delMovie(arg: number) {
    this.movieservice.delMovie(arg).subscribe({
      next:(res)=>{
        console.log(res);
        
      }
    })
    }
  constructor(
    private route: ActivatedRoute,
    public movieservice: MoviesService,
    public dialog: MatDialog,
    private watchedService :WatchedService,
    private sanitizer: DomSanitizer,
    private testService: TestService,
    private authService: AuthService,
    private snackBarservice:SnackBarService
  ) {

   
    this.authz=this.authService.isAdmin()

    this.getImage();

  }


  setImage(){
    
    this.route.paramMap.subscribe((params) => {
      const id = params.get('id');
      console.log('ID from route parameter:', id);
      this.MovieId = parseInt(id);

      this.movieservice.getMovieById(this.MovieId).subscribe({
        next: (movie: any) => {
          console.log("movie",movie);

          this.movieFromDb = movie;
          if(movie.image){
            console.log("image exists");
            this.movieFromDb.image = movie.image;

          }
          

          if(movie.image){
            console.log("image exists");

            
            
            // const blob = new Blob([movie.image], { type: 'image/jpeg' });
            // const reader = new FileReader();
            // reader.onloadend = () => {
            //   reader.readAsDataURL(this.movieFromDb.image);
            //   this.movieFromDb.image=reader.result
            // };
            // reader.readAsDataURL(blob);

          //   const buffer = Buffer.from(movie.image);
          // const base64String = buffer.toString('base64');
          // console.log("b64",base64String);
          
          // let objectURL = 'data:image/jpeg;base64,' + movie.image;
          let objectURL = 'data:image/jpeg;base64,' + movie.image;
          console.log("objUrl", objectURL);

          console.log("movie.image", movie.image);
          
          
         this.movieFromDb.image = this.sanitizer.bypassSecurityTrustUrl(objectURL);
         console.log(this.movieFromDb.image);
         
            //  this.movieFromDb.image = movie.image;
            //  console.log(movie.image,"movieImage");
             
          }
          this.movieFromDb.ratings = movie.ratings * 1;
          console.log(this.movieFromDb);
        },
      });

      this.movieservice.getRatingByMovieId(this.MovieId).subscribe({
        next: (result: Ratings[]) => {
          this.ratings = result;
          console.log(result, 'result');

          this.dataSource.data = result;
        },
      });
    });
  }


  updateMovie(id:number){
    console.log(id);
    
    const dialogRef=this.dialog.open(AddMovieComponent,{
      width: '400px',
      disableClose: true,
      data: {
        movieId: id,
      },

    })

    dialogRef.afterClosed().subscribe((result) => {
      this.movieservice.getRatingByMovieId(this.MovieId).subscribe({
        next: (result: Ratings[]) => {
          this.dataSource.data = result;
        },
      });
    });
  }


  openDialog() {
    if (localStorage.getItem('AuthJwtToken')) {
      const dialogRef = this.dialog.open(RateComponent, {
        width: '400px',
        disableClose: true,
        data: {
          MovieId: this.MovieId,
        },
      });

      dialogRef.afterClosed().subscribe((result) => {
        this.movieservice.getRatingByMovieId(this.MovieId).subscribe({
          next: (result: Ratings[]) => {
            this.dataSource.data = result;
          },
        });
      });
      
    } else {
      const dialogRef =this.dialog.open(LoginComponent, {
        width: '400px',
      height: '400px',
        disableClose: true, 
      });

      this.snackBarservice.openSnackBar("you must be logged in","OK")


      
      dialogRef.afterClosed().subscribe((result) => {
        this.movieservice.getRatingByMovieId(this.MovieId).subscribe({
          next: (result: Ratings[]) => {
            this.dataSource.data = result;
          },
        });
      });
    }
  }
}
