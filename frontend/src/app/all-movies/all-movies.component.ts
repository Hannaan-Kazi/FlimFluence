import { AfterViewInit, Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Ratings } from '../models/ratings-Model';
import { WatchLaterService } from '../Services/watch-later.service';
import { Movies } from '../models/movies-Model';
import { MoviesService } from '../movies.service';
import { Router } from '@angular/router';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { AuthService } from '../Services/auth.service';
import { AddMovieComponent } from '../add-movie/add-movie.component';
import { MatDialog } from '@angular/material/dialog';
import { DatePipe } from '@angular/common';
import { BehaviorSubject, Subject } from 'rxjs';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-all-movies',
  templateUrl: './all-movies.component.html',
  styleUrls: ['./all-movies.component.css'],
  providers: [DatePipe],
  // encapsulation: ViewEncapsulation.None,
})
export class AllMoviesComponent implements OnInit, AfterViewInit {
  selectedSortColumn: any;

  sortData(event: any): void {
    console.log(event);

    const [sortBy, sortOrder] = event.value.split('_');
    console.log(sortBy, sortOrder);

    this.dataSource.data = this.dataSource.data.sort((a, b) => {
      const comparison = this.compare(a, b, sortBy, sortOrder);
      return sortOrder === 'desc' ? -comparison : comparison;
    });
  }

  compare(a: Movies, b: Movies, sortBy: string, sortOrder: string): number {
    // Compare function based on the selected property
    if (sortBy === 'name') {
      return a.title.localeCompare(b.title);
    } else if (sortBy === 'rating') {
      return a.ratings - b.ratings;
    } else if (sortBy === 'releaseDate') {
      return a.releaseDate
        .toLocaleString()
        .localeCompare(b.releaseDate.toLocaleString());
      return a.ratings - b.ratings;
    } else {
      return 0;
    }
  }

  @ViewChild(MatPaginator) paginator: MatPaginator;
  // @ViewChild(MatSort) sort: MatSort;
  authz: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  isAdmin: boolean;
  // authz: boolean;

  onCardClick(id: any) {
    console.log('Clicked on card with id:', id);
    this.router.navigate(['/movie', id]);
  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    console.log(this.dataSource.data.values);
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  // dataSource = new MatTableDataSource<Ratings>([]);
  // displayedColumns: String[] = ['userId', 'movieId', 'operate'];
  dataSource = new MatTableDataSource<Movies>([]);
  displayedColumn: String[] = ['posterUrl', 'title'];
  mov: Movies[] = [];
  movies: any;

  updateDisplayedColumns(): void {
    console.log("column",this.isAdmin);
    
    this.displayedColumn = this.isAdmin
      ? ['posterUrl', 'title', 'operate']
      : ['posterUrl', 'title'];
  }
  constructor(
    private wLService: WatchLaterService,
    private moviesService: MoviesService,
    private router: Router,
    private authService: AuthService,
    private movieservice: MoviesService,
    private dialog: MatDialog,
    private datePipe: DatePipe
  ) {
    this.authService.isAdmin();
    this.authService.bahotAlag().subscribe((e)=>{
      this.isAdmin =e
      this.updateDisplayedColumns();
      console.log("e",this.isAdmin);
    })
    this.updateDisplayedColumns();
    this.moviesService.getPosts().subscribe({
      next: (res) => {
        this.dataSource.data = res;
        console.warn(this.dataSource.data);
      },
    });
    
  }
  formatDate(dateString: string): string {
    const date = new Date(dateString);

    return this.datePipe.transform(date, 'yyyy-MM-dd');
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    // this.dataSource.sort = this.sort;
  }
  ngOnInit(): void {}

  updateMovie(id: number) {
    console.log(id);

    const dialogRef = this.dialog.open(AddMovieComponent, {
      width: '400px',
      disableClose: true,
      data: {
        movieId: id,
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      // this.movieservice.getRatingByMovieId(this.MovieId).subscribe({
      //   next: (result: Ratings[]) => {
      //     this.dataSource.data = result;
      //   },
      // });
      console.log(result);
    });
  }

  onDeleteClick(arg: number) {
    this.movieservice.delMovie(arg).subscribe({
      next: (res) => {
        console.log(res);
      },
    });
  }
}
