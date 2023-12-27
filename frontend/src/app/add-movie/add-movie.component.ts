import { Component, Inject, OnInit, Optional } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { TestService } from '../Services/test.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MoviesService } from '../movies.service';
import { Movies } from '../models/movies-Model';

@Component({
  selector: 'app-add-movie',
  templateUrl: './add-movie.component.html',
  styleUrls: ['./add-movie.component.css'],
})
export class AddMovieComponent implements OnInit {
  form: FormGroup;
  // onload = '';
  onload :string
  load: any;
  remove: boolean = false;
  fileName = '';
  selectedImage: File | null = null;
  imageBase64: any;
  formData = new FormData();
  movieIdToUpdate: number;
  movieToUpdate: any;
  isUpdating: boolean = false;

  constructor(
    private fb: FormBuilder,
    public testService: TestService,
    public movieService: MoviesService,
    public dialogRef: MatDialogRef<AddMovieComponent>,
     @Inject(MAT_DIALOG_DATA) public dataToUpdate?: any
  ) {
    console.log(dataToUpdate);
    
    console.warn("hiiii",dataToUpdate.movieId);
    if(dataToUpdate.movieId>0)
      this.movieIdToUpdate = dataToUpdate.movieId;

    console.log(this.movieIdToUpdate);
    
  }
  
  
  ngOnInit(): void {
    if(this.movieIdToUpdate)
      this.movieToUpdate = this.getDataToUpdate();
    
    this.form = this.fb.group({
      title: [''],
      releaseDate: [''],
      genre: [''],
      summary: [''],
      image: [],
      posterUrl: [''],
    });
    // this.form = this.fb.group({
    //   title: ['', Validators.required],
    //   releaseDate: ['', Validators.required],
    //   genre: ['', Validators.required],
    //   summary: ['', Validators.required],
    //   pic: ['']
    // });

    if (this.movieIdToUpdate != null) {
      this.isUpdating = true;

      // Patch the form with the data
    }
  }
  patchingValue(movieToUpdate:Movies){

    const byteCharacters = atob( movieToUpdate.image ); // Decode base64
    const byteNumbers = new Array(byteCharacters.length);
    for (let i = 0; i < byteCharacters.length; i++) {
    byteNumbers[i] = byteCharacters.charCodeAt(i);
    }
    const byteArray = new Uint8Array(byteNumbers);
    const blob = new Blob([byteArray], { type: 'image/jpeg' }); // Set the appropriate image type
 
    // Create a File object
    const file = new File([blob], movieToUpdate.title, { type: 'image/jpeg' });
    const reader = new FileReader();
    reader.onloadend = () => {
      this.onload = reader.result as string;
      
    }
    reader.readAsDataURL(file);
    
    this.remove=true

    this.form.patchValue({
      title: movieToUpdate.title,
      summary: movieToUpdate.summary,
      genre: movieToUpdate.genre,
      posterUrl: movieToUpdate.posterUrl,
      releaseDate: movieToUpdate.releaseDate.toString().split("T")[0],
      image:file
    });
  }
  
  getDataToUpdate() {
    console.log(this.movieIdToUpdate);
    
    this.movieService.getMovieById(this.movieIdToUpdate).subscribe({
      next: (res) => {
        console.log('getDataToUpdate', res);

        this.patchingValue(res)
        return res;
      },
      error: (err) => {
        console.error(err);
        return null;
      },
    });
  }

  objectToFormData(obj) {
    const formData = new FormData();

    for (const key in obj) {
      if (obj.hasOwnProperty(key)) {
        console.log(key, ' is set to', obj[key]);

        formData.append(key, obj[key]);
      }
    }

    return formData;
  }

  // debugger
  onSubmit(): void {

    // console.log(this.form.value);
    // return null
    
    // const file: File =this.selectedImage ;
    //   formData.append('file', file);

    //   const formData = new FormData();
    // console.log(this.form.value);
    // console.log("selected image on submit",this.selectedImage);

    // this.form.value.image=this.selectedImage;
    // console.log("image value",this.form.value.image);

    // console.log("form value",this.form.value);
    // const formData = new FormData();
    this.formData.append('title', this.form.value.title);
    this.formData.append('posterUrl', this.form.value.posterUrl);
    this.formData.append('genre', this.form.value.genre);
    this.formData.append('summary', this.form.value.summary);
    this.formData.append('releaseDate', this.form.value.releaseDate);
    if(this.movieIdToUpdate) this.formData.append('movieId', this.movieIdToUpdate.toString());
    console.warn(this.formData);
    
    // formData.append("Image", this.form.value.image)
    // formData.append("image",this.form.value.image.substring(this.form.value.image.indexOf('base64,'+7),this.form.value.image.length))
    // console.log(formData["image"]);
    // console.log(formData["title"]);
    // console.log("the fd",formData);.index
    // console.log("type of",typeof(formData));

    if(this.isUpdating){
      console.log(this.formData);
       
      this.movieService.updateMovie(this.formData,this.movieIdToUpdate).subscribe({
        next:(res)=>{
            console.log(res);
            
        },
        error:(err)=>{
            console.log(err);
        }
      })
    }else{

      
      this.testService.addMovie(this.formData).subscribe({
        next: (res: any) => {
        // const byteCharacters = atob(res); // Decode base64
        // const byteNumbers = new Array(byteCharacters.length);
        // for (let i = 0; i < byteCharacters.length; i++) {
        //   byteNumbers[i] = byteCharacters.charCodeAt(i);
        // }
        // const byteArray = new Uint8Array(byteNumbers);
        // const blob = new Blob([byteArray], { type: 'image/jpeg' }); // Set the appropriate image type
        
        // // Create a File object
        // const file = new File([blob], 'something', { type: 'image/jpeg' });
        // var reader = new FileReader();
        // reader.readAsDataURL(file);
        // this.onload = res;
        console.log(res, 'added');
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
    // this.testService.uploadImage(this.selectedImage).subscribe({
    //   next:(res:any)=>{
    //     console.log(res, "added");
    //   }
    // })
  }

  // onImgSelected(event) {
  //   if(event) console.log(event);
  //   var reader=new FileReader()
  //   reader.readAsDataURL(event.target.files[0]);

  //   this.selectedImage = event.target.files[0] ;
  //   reader.onload=(event:any)=>{
  //     console.log("result " ,event.target.result);

  //     this.onload=event.target.result;
  //     this.selectedImage=event.target.result;
  //     console.log("Selected Image",this.selectedImage);

  //     this.remove=true;

  //   }

  // }

  onImgSelected(event: any, field: string): void {
    const file = event.target.files[0];
    this.formData.append(field, file, file.name);
    console.log(file);
    if (event) console.log(event);
    var reader = new FileReader();
    reader.readAsDataURL(event.target.files[0]);

    console.log('event.target.files[0]', event.target.files[0]);

    this.selectedImage = event.target.files[0];
    reader.onload = (event: any) => {
      console.log('result ', event.target.result);

      this.onload = event.target.result;
      this.selectedImage = event.target.result;
      // console.log("Selected Image",this.selectedImage);

      this.remove = true;
    };
    // if (file) {
    //   this.convertImageToBase64(file);
    // }
  }

  convertImageToBase64(file: File): void {
    const reader = new FileReader();

    reader.onload = (e: any) => {
      // The onload event will be triggered once the file is read
      this.imageBase64 = e.target.result; // This is your base64 string
    };

    reader.readAsDataURL(file);
  }

  // onFileSelected(event){
  //   if(event){
  //     console.log(event);
  //     var reader=new FileReader()
  //     reader.readAsDataURL(event.target.files[0]);
  //     reader.onload=(event:any)=>{
  //       console.log("result " ,event.target.result);

  //       this.onload=event.target.result;
  //       this.selectedImage=event.target.result;
  //       console.log("Selected Image",this.selectedImage);

  //       this.remove=true;
  //     }
  //   }

  // }
  removePreview() {
    this.onload = null;
    this.remove = false;
  }
}
