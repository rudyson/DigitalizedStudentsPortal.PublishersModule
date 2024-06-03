import { HttpClient } from '@angular/common/http';
import { Injectable, SecurityContext } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';

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
  constructor(private http: HttpClient, private sanitizer: DomSanitizer) {}

  getMe() {
    return this.http.get<MicrosoftGraphMeResponse>(`${GRAPH_ENDPOINT}/me`);
  }

  getMePhoto() {
    return this.http.get(`${GRAPH_ENDPOINT}/me/photo/$value`, {
      responseType: 'blob',
    });
  }

  getProfileImageUnsafeUrl(): string {
    this.getMePhoto().subscribe((blob: any) => {
      let objectURL = URL.createObjectURL(blob);
      let imageSafeUrl = this.sanitizer.bypassSecurityTrustUrl(objectURL);
      return (
        this.sanitizer.sanitize(SecurityContext.RESOURCE_URL, imageSafeUrl) ??
        ''
      );
    });
    return '';
  }
}
