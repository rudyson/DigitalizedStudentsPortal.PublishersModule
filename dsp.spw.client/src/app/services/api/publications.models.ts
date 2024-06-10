import {
  ResearcherPseudonymSearchModel,
  ResearcherSearchModel,
} from './researchers.service.models';

export enum PublicationTypes {
  Article = 1, // стаття
  Theses = 2, // тези
  MethodicalManual = 3, // Методичний посібник
  StudyMethodicalManual = 4, // Навчально-методичний посібник
  Patent = 5, // Патенти
  Note = 6, // Замітка
}

export enum HighEducationLevels {
  Bachelor = 0,
  Master = 1,
  DoctorOfPhilosophy = 2,
}

export enum PublicationCategory {
  A = 1,
  B = 2,
  C = 3,
}

export enum ScienceDatabaseTypes {
  Faculty = 0, // Фахове
  Scopus = 1, // https://www.scopus.com/
  WebOfScience = 2, // https://clarivate.com/cis/solutions/web-of-science/
  GoogleAcademy = 3, // https://scholar.google.com.ua/
}

export interface InternalAuthor {
  author: ResearcherSearchModel | null;
  pseudonym: ResearcherPseudonymSearchModel | null;
}

export interface Publication {
  title: string;
  reference: string;
  type: PublicationTypes; // Use specific enum type if available, e.g., PublicationTypes
  category: PublicationCategory; // Use specific enum type if available, e.g., PublicationCategory
  year: Date; // Use Date if the backend handles Date serialization/deserialization
  pages: number | null;
  pagesAuthor: string | null;
  publishingName: string | null;
  isWithStudent: boolean | null;
  isInternational: boolean | null;
  conferenceName: string | null;
  conferenceDates: Date[] | null; // Use Date[] if the backend handles Date serialization/deserialization
  conferenceCountry: string | null;
  conferenceCity: string | null;
  magazineName: string | null;
  magazineIssue: string | null;
  magazineNumber: number | null;
  pageFirst: number | null;
  pageLast: number | null;
  doi: string | null;
  isbn: string | null;
  issn: string | null;
  url: string | null;
  internalAuthors: InternalAuthor[] | null;
  externalAuthors: string[] | null;
}
