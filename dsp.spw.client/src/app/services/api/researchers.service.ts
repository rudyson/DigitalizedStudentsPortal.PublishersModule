import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  PublisherProfileIdentityModel,
  ResearcherGetInformationModel,
  ResearcherPseudonymModel,
  ResearcherPseudonymSearchModel,
  ResearcherSearchModel,
} from './researchers.service.models';
import { lastValueFrom } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ResponseWrapper } from './common.models';
import { ResearcherCreatePseudonymModel } from './publications.models';

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
        `${this.api}/researchers/${researcherId}/pseudonyms/search`
      )
    );
  }

  createPseudonym(
    researcherId: number,
    model: ResearcherCreatePseudonymModel
  ): Promise<ResponseWrapper<ResearcherPseudonymModel>> {
    return lastValueFrom(
      this.http.post<ResponseWrapper<ResearcherPseudonymModel>>(
        `${this.api}/researchers/${researcherId}/pseudonyms`,
        model
      )
    );
  }

  deletePseudonym(
    researcherId: number,
    pseudonymId: number,
    model: ResearcherCreatePseudonymModel
  ): Promise<ResponseWrapper<ResearcherPseudonymModel>> {
    return lastValueFrom(
      this.http.post<ResponseWrapper<ResearcherPseudonymModel>>(
        `${this.api}/researchers/${researcherId}/pseudonyms`,
        model,
        {
          params: {
            pseudonymId,
          },
        }
      )
    );
  }
}
