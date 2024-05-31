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
}

@Injectable({
  providedIn: 'root',
})
export class ResearchersService {
  constructor(private http: HttpClient) {}

  getInfo(model: PublisherProfileIdentityModel) {
    return this.http.post<ResearcherGetInformationModel>(
      `${API_ENDPOINT}/researchers/get`,
      model
    );
  }
}
