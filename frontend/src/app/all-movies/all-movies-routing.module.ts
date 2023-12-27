import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AllMoviesComponent } from './all-movies.component';


const routes: Routes = [
  {
    path: '',
    component: AllMoviesComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AllMoviesRoutingModule { }