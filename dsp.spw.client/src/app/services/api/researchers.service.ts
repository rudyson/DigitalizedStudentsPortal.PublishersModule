import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  PublisherProfileIdentityModel,
  ResearcherGetInformationModel,
  ResearcherPseudonymSearchModel,
  ResearcherSearchModel,
} from './researchers.service.models';
import { Observable } from 'rxjs';

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

  searchResearchers(query: string): Observable<ResearcherSearchModel[]> {
    return this.http.get<ResearcherSearchModel[]>(
      `${API_ENDPOINT}/researchers/search`,
      {
        params: { query },
      }
    );
  }

  getPseudonyms(
    researcherId: number
  ): Observable<ResearcherPseudonymSearchModel[]> {
    return this.http.get<ResearcherPseudonymSearchModel[]>(
      `${API_ENDPOINT}/researchers/pseudonyms/${researcherId}`
    );
  }
}
