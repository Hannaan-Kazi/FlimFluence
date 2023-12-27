export interface Movies {
  movieId:number;
  title : string;
  posterUrl? : string;
  summary : string;
  genre : string;
  releaseDate : Date;
  ratings? : number;
  image?: any
}
