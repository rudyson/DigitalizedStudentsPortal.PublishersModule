import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

const API_ENDPOINT = 'https://localhost:7239/api';

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

  Pseudonyms: ResearcherPseudonymModel[];
  Profiles: ResearcherProfileModel[];
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

@Injectable({
  providedIn: 'root',
})
export class ResearchersService {
  constructor(private http: HttpClient) {}

  getOrCreateInfo(model: PublisherProfileIdentityModel) {
    return this.http.post<ResearcherGetInformationModel>(
      `${API_ENDPOINT}/researchers/create`,
      model
    );
  }

  getInfo() {
    return this.http.get<ResearcherGetInformationModel>(
      `${API_ENDPOINT}/researchers/get`
    );
  }
}
