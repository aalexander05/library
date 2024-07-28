export interface Book {
    id: number;
    title: string;
    author: string;
    description: string;
    coverImage: string | null;
    publisher: string;
    publicationDate: string;
    category: string;
    isbn: string;
    pageCount: number;
    isCheckedOut: boolean;
    returnDueDate: string | null;
}