import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'word-count-form',
  templateUrl: './word-count-form.component.html',
  styleUrls: ['./word-count-form.component.css']
})
export class WordCountFormComponent implements OnInit {

  public form: FormGroup = new FormGroup({
    cbxWordCount: new FormControl(true),
    cbxPageCount: new FormControl(false),
    cbxTimeToRead: new FormControl(false),
    cbxDifficulty: new FormControl(false),
    edition: new FormControl(''),
    wordCount: new FormControl('', [ Validators.pattern("^[0-9]*$")]), // Ensure number 
    pageCount: new FormControl('', [ Validators.pattern("^[0-9]*$")]), // Ensure number
    timeToReadH: new FormControl(0, [ Validators.pattern("^[0-9]*$")]), // A lot of sites don't have this already
    timeToReadM: new FormControl(0, [ Validators.pattern("^[0-9]*$")]),
    difficulty: new FormControl(0, [Validators.pattern("^[0-9]*$")]),
    comment: new FormControl('', Validators.minLength(10)),
  });
  
  constructor() {
    console.log('in WordCountFormComponent constructor');
  }
  
  ngOnInit(): void {
    console.log('in WordCountFormComponent ngOnInit');
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
      str = 'Medium';
    } else if (difficulty < 9) {
      str = 'Hard';
    } else {
      str = 'Very Hard';
    }

    return str;
  }

}
