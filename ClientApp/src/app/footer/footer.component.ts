import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'footer-component',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent {

  currentYear = new Date().getFullYear();

  constructor() { }

}
