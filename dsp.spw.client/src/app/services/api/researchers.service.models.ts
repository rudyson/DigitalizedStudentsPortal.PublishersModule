export interface PublisherProfileIdentityModel {
  lastName: string;
  firstName: string;
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
