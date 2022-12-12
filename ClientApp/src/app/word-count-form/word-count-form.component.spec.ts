import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WordCountFormComponent } from './word-count-form.component';

describe('WordCountFormComponent', () => {
  let component: WordCountFormComponent;
  let fixture: ComponentFixture<WordCountFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WordCountFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WordCountFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
