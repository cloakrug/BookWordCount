import { Component, Input, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormControl, FormGroup, UntypedFormControl, UntypedFormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { BookStatsModel } from '../models/bookstatsmodel';
import { BookService } from '../services/book.service';

@Component({
  selector: 'word-count-form',
  templateUrl: './word-count-form.component.html',
  styleUrls: ['./word-count-form.component.css']
})
export class WordCountFormComponent implements OnInit {
  @Input('bookId') bookId: string = '';
  
  // TODO: @Output when the form has been successfully submitted

  // TODO: make untyped typed
  public form: FormGroup = new FormGroup({
    cbxWordCount: new FormControl(true),
    cbxPageCount: new FormControl(false),
    cbxDuration: new FormControl(false),
    cbxDifficulty: new FormControl(false),
    edition: new FormControl(''),
    wordCount: new FormControl('', [Validators.pattern("^[0-9]*$"), Validators.min(1)]), // Ensure number 
    pageCount: new FormControl('', [Validators.pattern("^[0-9]*$"), Validators.min(1)]), 
    duration: new FormGroup({
      hours: new FormControl('', [Validators.pattern("^\s*[0-9]*\s*$")]), // Ensure number or whitespace
      minutes: new FormControl('', [Validators.pattern("^\s*[0-9]*\s*$")]),
    }, this.nonZeroTimeValidator()),      
    difficulty: new FormControl(0, [Validators.pattern("^[0-9]*$"), Validators.min(0)]),
  });
  
  constructor(private bookService: BookService) { }
  
  ngOnInit(): void {
    console.log('in WordCountFormComponent constructor');
    console.log('bookId: ', this.bookId)

    if (this.bookId === null || this.bookId === undefined || this.bookId.trim() === '') throw new Error('bookId was not passed');

    console.log('in WordCountFormComponent ngOnInit');
    this.form.controls["cbxWordCount"].valueChanges.subscribe(() => this.onCheckboxValueChange());
    this.form.controls["cbxPageCount"].valueChanges.subscribe(() => this.onCheckboxValueChange());
    this.form.controls["cbxDuration"].valueChanges.subscribe(() => this.onCheckboxValueChange());
    this.form.controls["cbxDifficulty"].valueChanges.subscribe(() => this.onCheckboxValueChange());
    this.onCheckboxValueChange(); // to ensure correct start state
  }

  private onCheckboxValueChange(): void {
    const checkboxNameToStatControl = new Map<string, string>();
    checkboxNameToStatControl.set('cbxWordCount', 'wordCount');
    checkboxNameToStatControl.set('cbxPageCount', 'pageCount');
    checkboxNameToStatControl.set('cbxDifficulty', 'difficulty');

    for (const [checkboxName, statControlName] of checkboxNameToStatControl) {
      if (this.form.controls[checkboxName].value === true) {
        this.form.controls[statControlName].addValidators(Validators.required);
      } else {
        this.form.controls[statControlName].removeValidators(Validators.required);
      }
    }

    this.form.get('duration')?.updateValueAndValidity();  // Needed because it's not automatically checked
  }

  
  
  public onSubmit(): void {
    console.log(this.form.value);
    console.log(this.form.controls['difficulty'].value)

    if (this.bookId === undefined || this.bookId === null) {
      throw new Error('bookId is not defined');
    }

    const durationGroup = this.form.get('duration');

    if (durationGroup === null || durationGroup === undefined) {
      throw new Error('durationGroup is not defined');
    }

    // Ensure that the form is valid
    if (this.form.valid) {

      const model: BookStatsModel = {
        BookId: this.bookId
      };

      if (this.form.get('cbxWordCount')?.value) {
        model.wordCount = this.form.get('wordCount')?.value;
      }

      if (this.form.get('cbxPageCount')?.value) {
        model.pageCount = this.form.get('pageCount')?.value;
      }

      if (this.form.get('cbxDuration')?.value) {
        model.durationInSeconds = this.getDurationInSeconds(durationGroup);
      }

      if (this.form.get('cbxDifficulty')?.value) {
        model.difficulty = this.form.get('difficulty')?.value;
      }

      this.bookService.addStats(model).subscribe(res => console.log(res));
    }
  }

  public onReset(): void {
    console.log('in onReset()')
  }

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

  private nonZeroTimeValidator(): ValidatorFn {
    return (durationGroup: AbstractControl): ValidationErrors | null => {
      if (!this.form?.get('cbxDuration')?.value) return null;

      const totalSeconds = this.getDurationInSeconds(durationGroup);

      if (totalSeconds === 0) {
        return { zeroTime: true };
      } else {
        return null;
      }
    };
  }

  private getDurationInSeconds(timeGroup: AbstractControl): number {
    let minutesStr: string = timeGroup?.get('minutes')?.value;
    let hoursStr: string = timeGroup?.get('hours')?.value;

    let minutes = isNaN(parseInt(minutesStr)) ? 0 : parseInt(minutesStr);
    let hours = isNaN(parseInt(hoursStr)) ? 0 : parseInt(hoursStr);

    return hours * 60 + minutes;
  }
}
