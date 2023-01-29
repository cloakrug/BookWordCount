import { Component, OnInit } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { first } from 'rxjs/operators';
import { Book } from '../models/book';
import { UserBookStatsModel } from '../models/userbookstats';
import { BookService } from '../services/book.service';

interface UserBookStatsTableModel {
  bookTitle: string;
  wordCount: string;
  pageCount: string;
  difficulty: string;
  durationInSeconds: string;
}

@Component({
  selector: 'user-page',
  templateUrl: './user-page.component.html',
  styleUrls: ['./user-page.component.css']
})
export class UserPageComponent implements OnInit {

  public displayedColumns: string[] = ['book', 'wordCount', 'pageCount', 'duration', 'difficulty'];

  public userBookStats$: BehaviorSubject<UserBookStatsTableModel[]> = new BehaviorSubject([] as UserBookStatsTableModel[]);
  public showGetStatsError: boolean = false;

  constructor(private bookService: BookService) { }

  ngOnInit(): void {
    this.bookService.getAllUserStats().pipe(first()).subscribe((res: UserBookStatsModel[]) => {
      const vm = this.convertResToViewModel(res);
      console.log('vm: ', vm);
      this.userBookStats$.next(vm);
    }, this.handleGetStatsError);
  }

  convertResToViewModel(res: UserBookStatsModel[]): UserBookStatsTableModel[] {
    return res.map((model: UserBookStatsModel) => {
      return {
        bookTitle: model.book?.title,
        wordCount: model.wordCount?.toString() ?? "-",
        pageCount: model.pageCount?.toString() ?? "-",
        difficulty: model.difficulty?.toString() ?? "-",
        durationInSeconds: model.durationInSeconds?.toString() ?? "-"
      } as UserBookStatsTableModel;
    });
  }

  // TODO
  handleGetStatsError(): void {
    this.showGetStatsError = true;
  }
}
