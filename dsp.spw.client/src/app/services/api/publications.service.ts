import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

const API_ENDPOINT = 'https://localhost:7239/api';

@Injectable({
  providedIn: 'root',
})
export class PublicationsService {
  constructor(private http: HttpClient) {}

  createPublication(publication: any): Observable<any> {
    return this.http.post(`${API_ENDPOINT}/publication/create`, publication);
  }
}
