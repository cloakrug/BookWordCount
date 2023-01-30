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
  duration: string;
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
        duration: model.durationInSeconds === null
          ? "-"
          : this.convertSecondsToTimeStr(model.durationInSeconds)
      } as UserBookStatsTableModel;
    });
  }
  
  private convertSecondsToTimeStr(seconds: number): string {
    let hours = Math.floor(seconds / 3600);
    let minutes = Math.floor((seconds - (hours * 3600)) / 60);

    let hoursStr = hours.toString();
    let minutesStr = minutes.toString();

    if (minutes < 10) {
      minutesStr = "0" + minutes;
    }
    return hoursStr + ':' + minutesStr;
  }

  // TODO
  handleGetStatsError(): void {
    this.showGetStatsError = true;
  }
}
