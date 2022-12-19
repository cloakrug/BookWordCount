import { Directive, ElementRef, Input, OnChanges, SimpleChanges } from '@angular/core';

@Directive({
  selector: '[difficultyColor]'
})
export class DifficultyColorDirective implements OnChanges {
  @Input() difficultyColor: string = "";

  constructor(private element: ElementRef) { }

  ngOnChanges(changes: SimpleChanges) {
    if (changes.difficultyColor.currentValue?.toLowerCase() === 'easy') {
      this.element.nativeElement.style.color = '#5ee078'; // green
    } else if (changes.difficultyColor.currentValue?.toLowerCase() === 'medium') {
      this.element.nativeElement.style.color = 'orange';
    } else if (changes.difficultyColor.currentValue?.toLowerCase() === 'hard') {
      this.element.nativeElement.style.color = '#de354b'; // light red
    } else if (changes.difficultyColor.currentValue?.toLowerCase() === 'very hard') {
      this.element.nativeElement.style.color = '#b31b2f'; // dark red
    }

    this.element.nativeElement.style.fontWeight = 'bold';
  }
}
