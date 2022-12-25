import { Component, OnInit } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'word-count-form',
  templateUrl: './word-count-form.component.html',
  styleUrls: ['./word-count-form.component.css']
})
export class WordCountFormComponent implements OnInit {

  public form: UntypedFormGroup = new UntypedFormGroup({
    cbxWordCount: new UntypedFormControl(true),
    cbxPageCount: new UntypedFormControl(false),
    cbxTimeToRead: new UntypedFormControl(false),
    cbxDifficulty: new UntypedFormControl(false),
    edition: new UntypedFormControl(''),
    wordCount: new UntypedFormControl('', [ Validators.pattern("^[0-9]*$")]), // Ensure number 
    pageCount: new UntypedFormControl('', [ Validators.pattern("^[0-9]*$")]), // Ensure number
    timeToReadH: new UntypedFormControl('', [ Validators.pattern("^[0-9]*$")]), // A lot of sites don't have this already
    timeToReadM: new UntypedFormControl('', [ Validators.pattern("^[0-9]*$")]),
    difficulty: new UntypedFormControl(0, [Validators.pattern("^[0-9]*$")]),
    comment: new UntypedFormControl('', Validators.minLength(10)),
  });

  //public difficultySliderVal: number = 0;
  
  constructor() {
    console.log('in WordCountFormComponent constructor');
  }
  
  ngOnInit(): void {
    console.log('in WordCountFormComponent ngOnInit');
    //this.difficultySliderVal = this.form.controls['difficulty'].value;
  }

  public onSubmit(): void {
    console.log(this.form.value);

    console.log(this.form.controls['difficulty'].value)

    // Ensure that the form is valid
    if (this.form.valid) {
      
    }
  }

  public onReset(): void {
    console.log('in onReset()')
  }

  //public onSliderChange($event: any, sliderName: string) {
  //  console.log('in onSliderChange')
  //  console.log($event)
  //  switch (sliderName) {
  //    case 'difficulty':
  //      this.difficultySliderVal = parseInt($event.value);
  //      console.log('setting difficultySliderVal to: ' + this.difficultySliderVal)
  //      this.form?.get('difficulty')?.setValue(this.difficultySliderVal);
  //      break;
  //  }
  //}

  public getDifficultyStr(difficulty: number): string {
    let str = '';

    if (difficulty < 3) {
      str = 'Easy';
    } else if (difficulty < 6) {
      str = 'Moderately difficult';
    } else if (difficulty < 9) {
      str = 'Hard';
    } else {
      str = 'Very Hard';
    }

    return str;
  }

  // TODO: generate pipe (or directive) to make the text the correct color

}
