import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { WatchedService } from '../Services/watched.service';
import { MoviesService } from '../movies.service';
import { MatTableDataSource } from '@angular/material/table';
import { Movies } from '../models/movies-Model';
import { Ratings } from '../models/ratings-Model';

@Component({
  selector: 'app-watched',
  templateUrl: './watched.component.html',
  styleUrls: ['./watched.component.css']
})
export class WatchedComponent {
  onCardClick(id: any) {
    console.log('Clicked on card with id:', id);
    this.router.navigate(['/movie', id]);
  }
  onDeleteClick(id: number) {
    this.watchedService.delWl(id).subscribe({
      next:(res:any)=>{ console.log(res);
      }
    }
    // ,{
    //   error:(err:any)=>{}
    // }
    )
  }

  dataSource = new MatTableDataSource<Ratings>([]);
  displayedColumns: String[] = ['userId', 'movieId', 'operate'];
  dataSourc = new MatTableDataSource<Movies>([]);
  displayedColumn: String[] = ['posterUrl', 'title', 'operate'];
  mov: Movies[] = [];
  movies: any;

  constructor(private watchedService: WatchedService,
    private moviesService: MoviesService,
    private router: Router){
      this.watchedService.getWl().subscribe({
        next: (res: any) => {
          for (let mid of res) {
            console.log(res, 'RES');
            this.moviesService.getMovieById(mid.movieId).subscribe({
              next: (res) => {
                this.mov.push(res);
                this.dataSourc.data = this.mov;
              },
            });
          }
          // console.log(res, 'RES');
          console.warn("this movie",this.movies)
          // console.log(this.dataSourc.data);
  
          this.dataSource = res;
        },
      });
    }
}
