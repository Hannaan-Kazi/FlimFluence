import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Ratings } from '../models/ratings-Model';
import { WatchLaterService } from '../Services/watch-later.service';
import { Movies } from '../models/movies-Model';
import { MoviesService } from '../movies.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-watch-later',
  templateUrl: './watch-later.component.html',
  styleUrls: ['./watch-later.component.css'],
})
export class WatchLaterComponent implements OnInit {
  onCardClick(id: any) {
    console.log('Clicked on card with id:', id);
    this.router.navigate(['/movie', id]);
  }

  dataSource = new MatTableDataSource<Ratings>([]);
  displayedColumns: String[] = ['userId', 'movieId', 'operate'];
  dataSourc = new MatTableDataSource<Movies>([]);
  displayedColumn: String[] = ['posterUrl', 'title', 'operate'];
  mov: Movies[] = [];
  movies: any;

  constructor(
    private wLService: WatchLaterService,
    private moviesService: MoviesService,
    private router: Router
  ) {
    this.wLService.getWl().subscribe({
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
  ngOnInit(): void {}

  onDeleteClick(id: number) {
    this.wLService.delWl(id).subscribe({
      next:(res:any)=>{ console.log(res);
      }
    }
    // ,{
    //   error:(err:any)=>{}
    // }
    )
  }
}
