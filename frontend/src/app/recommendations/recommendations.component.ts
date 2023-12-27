import { Component } from '@angular/core';
import { MoviesService } from '../movies.service';
import { Movies } from '../models/movies-Model';
import { Router } from '@angular/router';
import { WatchLaterService } from '../Services/watch-later.service';

@Component({
  selector: 'app-recommendations',
  templateUrl: './recommendations.component.html',
  styleUrls: ['./recommendations.component.css'],
})
export class RecommendationsComponent {
  current = 5;
  fromDb: Movies[];
  trend: Movies[];
  
  constructor(private moviesService: MoviesService,private router: Router, private watchLaterService:WatchLaterService) {
    this.moviesService.getPosts().subscribe({
      next: (result: Movies[]) => {
        this.fromDb = result.slice(3, 12);
        console.log(this.fromDb,'from db');
        
        this.trend = result.slice(9, 11);
      },
    });
  }

  // OnWatchClick(id: number) {
  //   this.watchedService.addToWL(id).subscribe({
  //     next:()=>{alert("👍")},
  //   }
  //   )
  // }
  onCardClick(id:number){
    console.log('Clicked on card with id:', id);
    this.router.navigate(['/movie',id])
  }
  OnSaveClick(id: number) {
    this.watchLaterService.addToWL(id).subscribe({
      next:()=>{alert("👍")},
    }
    )
  }
}
