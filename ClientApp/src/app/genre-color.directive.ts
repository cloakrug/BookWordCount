import { Directive, ElementRef, Input, OnInit } from '@angular/core';
import GENRES from "../consts";

@Directive({
  selector: '[genreColor]'
})
export class GenreColorDirective implements OnInit {

  @Input('genreColor') genreColor: number = -1;

  private genreColorMap: Map<number, string> = new Map<number, string>();
  private genreTextColorMap: Map<number, string> = new Map<number, string>();

  constructor(private el: ElementRef) {
    GENRES.forEach(genre => {
      if (genre.color) {
        this.genreColorMap.set(genre.id, genre.color);
      }
      if (genre.textColor) {
        this.genreTextColorMap.set(genre.id, genre.textColor);
      }
    })
  }

  ngOnInit() {
    this.el.nativeElement.style.backgroundColor = this.genreColorMap.get(this.genreColor);

    const textColor = this.genreTextColorMap.get(this.genreColor);

    if (textColor === 'light') {
      this.el.nativeElement.classList.add('light-text'); 
    }
  }
}
