export interface Book {
  id: string,
  title: string,
  author: string,
  createdDate: Date,
  releasedDate: Date,
  description: string,
  imageUrl: string,
  genres: string[],
  majorGenre: string
};
