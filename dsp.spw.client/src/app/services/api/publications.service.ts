import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Publication } from './publications.models';

const API_ENDPOINT = 'https://localhost:7239/api';

@Injectable({
  providedIn: 'root',
})
export class PublicationsService {
  constructor(private http: HttpClient) {}

  createPublication(model: Publication): Observable<Publication> {
    return this.http.post<Publication>(
      `${API_ENDPOINT}/publication/create`,
      model
    );
  }
}
