import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookPageComponent } from './book-page.component';

describe('BookComponent', () => {
  let component: BookPageComponent;
  let fixture: ComponentFixture<BookPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ component ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
