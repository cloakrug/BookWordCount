import { Book } from "./book";

export interface UserBookStatsModel {
  book: Book;
  wordCount: number | null;
  pageCount: number | null;
  difficulty: number | null;
  durationInSeconds: number | null;
}
