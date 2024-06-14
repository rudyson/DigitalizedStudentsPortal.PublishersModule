import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  PaginationWrapper,
  PublisherProfileIdentityModel,
  ResearcherGetInformationModel,
  ResearcherPseudonymSearchModel,
  ResearcherSearchModel,
} from './researchers.service.models';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ResearchersService {
  api: string = `${environment.api}/api`;
  constructor(private http: HttpClient) {}

  getOrCreateInfo(model: PublisherProfileIdentityModel) {
    return this.http.post<ResearcherGetInformationModel>(
      `${this.api}/researchers/create`,
      model
    );
  }

  getInfoById(id: number) {
    return this.http.get<ResearcherGetInformationModel>(
      `${this.api}/researchers/${id}`
    );
  }

  getMyInfo() {
    return this.http.get<ResearcherGetInformationModel>(
      `${this.api}/researchers/me`
    );
  }

  getAllPaginated(skip: number = 0, take: number = 10) {
    return this.http.get<PaginationWrapper<ResearcherGetInformationModel[]>>(
      `${this.api}/researchers`,
      {
        params: { skip, take },
      }
    );
  }

  searchResearchers(query: string): Observable<ResearcherSearchModel[]> {
    return this.http.get<ResearcherSearchModel[]>(
      `${this.api}/researchers/search`,
      {
        params: { query },
      }
    );
  }

  getPseudonyms(
    researcherId: number
  ): Observable<ResearcherPseudonymSearchModel[]> {
    return this.http.get<ResearcherPseudonymSearchModel[]>(
      `${this.api}/researchers/pseudonyms/${researcherId}`
    );
  }
}
