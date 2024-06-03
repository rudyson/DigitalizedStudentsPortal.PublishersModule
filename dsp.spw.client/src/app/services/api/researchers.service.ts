import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  PublisherProfileIdentityModel,
  ResearcherGetInformationModel,
} from './researchers.service.models';

const API_ENDPOINT = 'https://localhost:7239/api';

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
