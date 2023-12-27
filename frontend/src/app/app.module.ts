import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { JwtModule } from '@auth0/angular-jwt';
import { DatePipe } from '@angular/common';

import { BrowserModule } from '@angular/platform-browser';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatDatepickerModule } from '@angular/material/datepicker';
import {MatNativeDateModule} from '@angular/material/core';
import { MatSortModule} from '@angular/material/sort';


import { MatGridListModule } from '@angular/material/grid-list';
import { MatButtonModule } from '@angular/material/button';
import{MatInputModule} from '@angular/material/input'
import { MatDialogModule} from '@angular/material/dialog';
import {MatCardModule} from '@angular/material/card';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatPaginatorModule} from '@angular/material/paginator';
import { MatTableModule} from '@angular/material/table';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import {MatSelectModule} from '@angular/material/select';




import { ReactiveFormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout'




import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { ToolBarComponent } from './tool-bar/tool-bar.component';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { LoginComponent } from './login/login.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CarouselComponent } from './carousel/carousel.component';
import { RecommendationsComponent } from './recommendations/recommendations.component';
import { HomeComponent } from './home/home.component';
import { MovieComponent } from './movie/movie.component';
import { RateComponent } from './rate/rate.component';
import { AddMovieComponent } from './add-movie/add-movie.component';
import { WatchLaterComponent } from './watch-later/watch-later.component';
import { WatchedComponent } from './watched/watched.component';
// import { AllMoviesComponent } from './all-movies/all-movies.component';

@NgModule({
  declarations: [
    AppComponent,
    ToolBarComponent,
    LoginComponent,
    SignUpComponent,
    CarouselComponent,
    RecommendationsComponent,
    HomeComponent,
    MovieComponent,
    RateComponent,
    AddMovieComponent,
    WatchLaterComponent,
    WatchedComponent,
    // AllMoviesComponent
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NoopAnimationsModule,
    MatIconModule,
    MatMenuModule,
    MatToolbarModule,
    MatSidenavModule,
    MatListModule,
    MatButtonModule,
    MatInputModule,
    ReactiveFormsModule,
    MatDialogModule,
    HttpClientModule,
    MatDatepickerModule,
    MatNativeDateModule,
    NgbModule,
    MatGridListModule,
    MatCardModule,
    FlexLayoutModule,
    MatAutocompleteModule,
    MatFormFieldModule,
    MatPaginatorModule,
    MatTableModule,
    MatSortModule,
    MatSelectModule,
    MatSnackBarModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: () => {
          return localStorage.getItem('AuthJwtToken');
        },
        allowedDomains: ['localhost'],
        disallowedRoutes: [''],
      },
    }), 
    

  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
