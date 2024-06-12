import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {
  Publication,
  PublicationGetInformationModel,
} from './publications.models';
import { PaginationWrapper } from './researchers.service.models';

const API_ENDPOINT = 'https://localhost:7239/api';

@Injectable({
  providedIn: 'root',
})
export class PublicationsService {
  constructor(private http: HttpClient) {}

  createPublication(model: Publication): Observable<Publication> {
    return this.http.post<Publication>(
      `${API_ENDPOINT}/publications/create`,
      model
    );
  }

  getAllPaginated(skip: number = 0, take: number = 10) {
    return this.http.get<PaginationWrapper<PublicationGetInformationModel[]>>(
      `${API_ENDPOINT}/publications`,
      {
        params: { skip, take },
      }
    );
  }
}
