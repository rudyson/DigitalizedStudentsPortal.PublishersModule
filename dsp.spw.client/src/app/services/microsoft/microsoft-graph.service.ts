import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

const GRAPH_ENDPOINT = 'https://graph.microsoft.com/v1.0';

export interface MicrosoftGraphMeResponse {
  '@odata.context'?: string;
  businessPhones: string[];
  displayName: string;
  givenName: string;
  jobTitle: string | null;
  mail: string | null;
  mobilePhone: string | null;
  officeLocation: string | null;
  preferredLanguage: string | null;
  surname: string;
  userPrincipalName: string;
  id: string;
}

@Injectable({
  providedIn: 'root',
})
export class MicrosoftGraphService {
  constructor(private http: HttpClient) {}

  getMe() {
    return this.http.get<MicrosoftGraphMeResponse>(`${GRAPH_ENDPOINT}/me`);
  }

  getMePhoto() {
    return this.http.get(`${GRAPH_ENDPOINT}/me/photo/$value`, {
      responseType: 'blob',
    });
  }
}
