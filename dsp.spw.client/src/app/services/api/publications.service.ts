import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, lastValueFrom } from 'rxjs';
import {
  Publication,
  PublicationGetInformationModel,
} from './publications.models';
import { PaginationWrapper } from './researchers.service.models';
import { environment } from 'src/environments/environment';
import { ResponseWrapper } from './common.models';

@Injectable({
  providedIn: 'root',
})
export class PublicationsService {
  api: string = `${environment.api}/api`;
  constructor(private http: HttpClient) {}

  createPublication(model: Publication): Promise<ResponseWrapper<Publication>> {
    return lastValueFrom(
      this.http.post<ResponseWrapper<Publication>>(
        `${this.api}/publications/create`,
        model
      )
    );
  }

  getAllPaginated(
    skip: number = 0,
    take: number = 10
  ): Promise<ResponseWrapper<PublicationGetInformationModel[]>> {
    return lastValueFrom(
      this.http.get<ResponseWrapper<PublicationGetInformationModel[]>>(
        `${this.api}/publications`,
        {
          params: { skip, take },
        }
      )
    );
  }
}
