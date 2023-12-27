import { Component, ViewChild } from '@angular/core';
import { MoviesService } from '../movies.service';
import { Movies } from '../models/movies-Model';
import { NgbCarousel } from '@ng-bootstrap/ng-bootstrap';
import { ActivatedRoute, Router } from '@angular/router';
import { WatchedService } from '../Services/watched.service';

@Component({
  selector: 'app-carousel',
  templateUrl: './carousel.component.html',
  styleUrls: ['./carousel.component.css'],
})
export class CarouselComponent {
  currentRate: number = 3.5;

  fromDb: any;
  paused = false;
  pauseOnHover = true;

  @ViewChild('carousel', { static: true })
  carousel!: NgbCarousel;
Math: any;

  constructor(private moviesService: MoviesService, private router:Router, private watchedService: WatchedService) {
    this.moviesService.getPosts().subscribe({
      next: (result: Movies[]) => {
        this.fromDb = result.slice(0,3); 
      },
    });
  }
  OnWatchClick(id: number) {
    this.watchedService.addToWL(id).subscribe({
      next:()=>{alert("👍")},
    }
    )
  }
  onCardClick(id:number){
    console.log('Clicked on card with id:', id);
    this.router.navigate(['/movie',id])
  }
  togglePaused() {
    if (this.paused) {
      this.carousel.cycle();
    } else {
      this.carousel.pause();
    }
    this.paused = !this.paused;
  }
}
