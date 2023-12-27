import { DialogRef } from '@angular/cdk/dialog';
import { Component, Inject, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { RatingsService } from '../Services/ratings.service';
import { Ratings } from '../models/ratings-Model';

import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-rate',
  templateUrl: './rate.component.html',
  styleUrls: ['./rate.component.css'],
})
export class RateComponent implements OnInit {
  // star=3
  MovieId: number;
  ratingFor: FormGroup;
  star: number = 0;

  submitClicked=false
  // ratingForm= new FormGroup({
  //   // userId:new FormControl(),
  //   movieId:new FormControl(),
  //   comment:new FormControl(),
  //   rating1:new FormControl('',Validators.required)
  // });

  ngOnInit(): void {
    this.initForm();
  }

  initForm() {
    this.ratingFor = this.fb.group({
      ratin: ['', [Validators.min(2), Validators.required]],
      comment: ['', Validators.required],
    });
  }

  constructor(
    public dialogRef: MatDialogRef<RateComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private route: ActivatedRoute,
    private ratingService: RatingsService,
    private fb: FormBuilder
  ) {
    // this.route.paramMap.subscribe((params) => {
    //   console.log(route);

    // const id = params.get('id');
    // console.log('ID from route parameter:', id);
    this.MovieId = data.MovieId;
    console.log(this.MovieId);

    // })
  }

  OnSubmit() {
    this.submitClicked=true
    
    console.log(this.ratingFor);
    if(!this.star) return null

    if (this.ratingFor.valid) {console.log('valid')
    // return null

    var a: Ratings = {
      movieId: this.MovieId,
      rating1: this.ratingFor.value.ratin,
      comment: this.ratingFor.value.comment,
    };
    //   // this.ratingForm.value.userId=0
    //   this.ratingForm.value.movieId=this.MovieId
    console.log(a);

    this.ratingService.postRating(a).subscribe({
      next: (res) => {
        console.log(res);
      },
    });
  
    this.dialogRef.close();
  }
    else{
      console.warn(
      this.ratingFor.valid)

      console.log("form not submitted");
      
    }
    // console.log(this.ratingForm.value);
  }
}
