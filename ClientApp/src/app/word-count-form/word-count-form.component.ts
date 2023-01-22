import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormGroup, UntypedFormControl, UntypedFormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';

@Component({
  selector: 'word-count-form',
  templateUrl: './word-count-form.component.html',
  styleUrls: ['./word-count-form.component.css']
})
export class WordCountFormComponent implements OnInit {
  // TODO: @Output when the form has been successfully submitted

  // TODO: make untyped typed
  public form: UntypedFormGroup = new UntypedFormGroup({
    cbxWordCount: new UntypedFormControl(true),
    cbxPageCount: new UntypedFormControl(false),
    cbxTimeToRead: new UntypedFormControl(false),
    cbxDifficulty: new UntypedFormControl(false),
    edition: new UntypedFormControl(''),
    wordCount: new UntypedFormControl('', [Validators.pattern("^[0-9]*$"), Validators.min(1)]), // Ensure number 
    pageCount: new UntypedFormControl('', [Validators.pattern("^[0-9]*$"), Validators.min(1)]), 
    time: new UntypedFormGroup({
      hours: new UntypedFormControl('', [Validators.pattern("^\s*[0-9]*\s*$")]), // Ensure number or whitespace
      minutes: new UntypedFormControl('', [Validators.pattern("^\s*[0-9]*\s*$")]),
    }, this.nonZeroTimeValidator()),      
    difficulty: new UntypedFormControl(0, [Validators.pattern("^[0-9]*$"), Validators.min(0)]),
  });
  
  constructor() {
    console.log('in WordCountFormComponent constructor');
  }
  
  ngOnInit(): void {
    console.log('in WordCountFormComponent ngOnInit');
    this.form.controls["cbxWordCount"].valueChanges.subscribe(() => this.onCheckboxValueChange());
    this.form.controls["cbxPageCount"].valueChanges.subscribe(() => this.onCheckboxValueChange());
    this.form.controls["cbxTimeToRead"].valueChanges.subscribe(() => this.onCheckboxValueChange());
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

    this.form.get('time')?.updateValueAndValidity();  // Needed because it's not automatically checked
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
    return (timeGroup: AbstractControl): ValidationErrors | null => {
      if (!this.form?.get('cbxTimeToRead')?.value) return null;

      let minutesStr: string = timeGroup?.get('minutes')?.value;
      let hoursStr: string = timeGroup?.get('hours')?.value;
      
      let minutes = isNaN(parseInt(minutesStr)) ? 0 : parseInt(minutesStr);
      let hours = isNaN(parseInt(hoursStr)) ? 0 : parseInt(hoursStr);

      let totalSeconds = hours * 60 + minutes;

      if (totalSeconds === 0) {
        return { zeroTime: true };
      } else {
        return null;
      }
    };
  }
}
