export interface PublisherProfileIdentityModel {
  lastName: string;
  firstName: string;
  middleName?: string;
  email: string;
}

export enum AcademicDegrees {
  None = 0,
  Student,
  Assistant, // Асистент
  AssociateProfessor, // Доцент
  SeniorLecturer, // Старший викладач
  Lecturer, // Викладач
  Professor, // Професор
}

export interface ResearcherGetInformationModel {
  id: number;
  lastName: string;
  middleName?: string;
  firstName: string;
  email: string;
  phoneNumber?: string;
  orcidUrl?: string;
  posada?: string;
  zvannya?: string;
  academicDegree: AcademicDegrees;
  stepin?: string;

  chair?: ChairGetInformationModel;

  pseudonyms: ResearcherPseudonymModel[];
  profiles: ResearcherProfileModel[];
}

export interface ResearcherPseudonymModel {
  id: number;
  shortName: string;
  lastName: string;
  middleName?: string;
  firstName: string;
}

export enum ScienceDatabaseTypes {
  Faculty, // Фахове
  Scopus, // https://www.scopus.com/
  WebOfScience, // https://clarivate.com/cis/solutions/web-of-science/
  GoogleAcademy, // https://scholar.google.com.ua/
}

export interface ResearcherProfileModel {
  id: number;

  type: ScienceDatabaseTypes;

  internalId?: string;
  url?: string;
}

export interface ResearcherPseudonymSearchModel {
  id: number;
  shortName: string;
}

export interface ResearcherSearchModel {
  id: number;
  shortName: string;
}

export interface ChairGetInformationModel {
  chairName: string;
  chairAbbreviation: string;
  facultyTitle: string;
  chairId: number;
  facultyId: number;
}

export interface PaginationWrapper<TModel> {
  data?: TModel;
  skip: number;
  take: number;
  total: number;
}
