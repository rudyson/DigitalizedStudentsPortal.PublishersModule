import {
  ResearcherPseudonymSearchModel,
  ResearcherSearchModel,
} from './researchers.service.models';

export interface PublicationGetInformationModel {
  id: number;
  title: string;
  reference: string;
  type: PublicationTypes;
  category: PublicationCategory;
  year: Date;
  url: string | null;
  contributors: PublicationContributorModel[];
}

export interface PublicationContributorModel {
  researcherId: number | null;
  pseudonymId: number;
  shortName: string;
}

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
  type: PublicationTypes;
  category: PublicationCategory;
  year: Date;
  pages: number | null;
  pagesAuthor: string | null;
  publishingName: string | null;
  isWithStudent: boolean | null;
  isInternational: boolean | null;
  conferenceName: string | null;
  conferenceDates: Date[] | null;
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
