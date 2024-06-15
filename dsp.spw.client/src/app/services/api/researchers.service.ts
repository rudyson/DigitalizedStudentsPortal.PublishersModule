import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  PaginationWrapper,
  PublisherProfileIdentityModel,
  ResearcherGetInformationModel,
  ResearcherPseudonymSearchModel,
  ResearcherSearchModel,
} from './researchers.service.models';
import { Observable, last, lastValueFrom } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ResponseWrapper } from './common.models';

@Injectable({
  providedIn: 'root',
})
export class ResearchersService {
  api: string = `${environment.api}/api`;
  constructor(private http: HttpClient) {}

  getOrCreateInfo(
    model: PublisherProfileIdentityModel
  ): Promise<ResponseWrapper<ResearcherGetInformationModel>> {
    return lastValueFrom(
      this.http.post<ResponseWrapper<ResearcherGetInformationModel>>(
        `${this.api}/researchers/create`,
        model
      )
    );
  }

  getInfoById(
    id: number
  ): Promise<ResponseWrapper<ResearcherGetInformationModel>> {
    return lastValueFrom(
      this.http.get<ResponseWrapper<ResearcherGetInformationModel>>(
        `${this.api}/researchers/${id}`
      )
    );
  }

  getMyInfo(): Promise<ResponseWrapper<ResearcherGetInformationModel>> {
    return lastValueFrom(
      this.http.get<ResponseWrapper<ResearcherGetInformationModel>>(
        `${this.api}/researchers/me`
      )
    );
  }

  getAllPaginated(
    skip: number = 0,
    take: number = 10
  ): Promise<ResponseWrapper<ResearcherGetInformationModel[]>> {
    return lastValueFrom(
      this.http.get<ResponseWrapper<ResearcherGetInformationModel[]>>(
        `${this.api}/researchers`,
        {
          params: { skip, take },
        }
      )
    );
  }

  searchResearchers(
    query: string
  ): Promise<ResponseWrapper<ResearcherSearchModel[]>> {
    return lastValueFrom(
      this.http.get<ResponseWrapper<ResearcherSearchModel[]>>(
        `${this.api}/researchers/search`,
        {
          params: { query },
        }
      )
    );
  }

  getPseudonyms(
    researcherId: number
  ): Promise<ResponseWrapper<ResearcherPseudonymSearchModel[]>> {
    return lastValueFrom(
      this.http.get<ResponseWrapper<ResearcherPseudonymSearchModel[]>>(
        `${this.api}/researchers/pseudonyms/${researcherId}`
      )
    );
  }
}
