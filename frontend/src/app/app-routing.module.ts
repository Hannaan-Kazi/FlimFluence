import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { HomeComponent } from './home/home.component';
import { MovieComponent } from './movie/movie.component';
import { AddMovieComponent } from './add-movie/add-movie.component';
import { WatchLaterComponent } from './watch-later/watch-later.component';
import { WatchedComponent } from './watched/watched.component';
import { AllMoviesComponent } from './all-movies/all-movies.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: '', redirectTo: '/home', pathMatch: 'full' }, 
  { path: 'home', component: HomeComponent },
  {path:'signup', component: SignUpComponent},
  {path:'movie/:id', component:MovieComponent},
  {path:'newMovie/add', component:AddMovieComponent},
  {path:'WatchLater', component:WatchLaterComponent},
  {path:'Watched', component:WatchedComponent},
  // {path:'All', component:AllMoviesComponent}
  {path:'All', loadChildren:()=>import('./all-movies/all-movies.module').then(m=>m.AllMoviesModule)}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
