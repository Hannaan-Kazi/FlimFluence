import { Component, OnInit, Renderer2 } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'project-front-end';
constructor(private renderer: Renderer2){

}

ngOnInit() {
  this.renderer.setStyle(document.body, 'background-color', '#0b0c2a');
}

}
