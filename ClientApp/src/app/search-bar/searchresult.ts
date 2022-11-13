export type SearchResult = BookResult;

export interface BookResult {
  title: string,
  id: string,
  shortDescription: string,
  imgUrl?: string
}
