import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import {
  Publication,
  PublicationGetInformationModel,
} from './publications.models';
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
        `${this.api}/publications`,
        model
      )
    );
  }

  search(
    query: string
  ): Promise<ResponseWrapper<PublicationGetInformationModel[]>> {
    return lastValueFrom(
      this.http.get<ResponseWrapper<PublicationGetInformationModel[]>>(
        `${this.api}/publications/search`,
        {
          params: { query },
        }
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
