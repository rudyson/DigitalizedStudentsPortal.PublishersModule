import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {
  Publication,
  PublicationGetInformationModel,
} from './publications.models';
import { PaginationWrapper } from './researchers.service.models';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class PublicationsService {
  api: string = `${environment.api}/api`;
  constructor(private http: HttpClient) {}

  createPublication(model: Publication): Observable<Publication> {
    return this.http.post<Publication>(
      `${this.api}/publications/create`,
      model
    );
  }

  getAllPaginated(skip: number = 0, take: number = 10) {
    return this.http.get<PaginationWrapper<PublicationGetInformationModel[]>>(
      `${this.api}/publications`,
      {
        params: { skip, take },
      }
    );
  }
}
