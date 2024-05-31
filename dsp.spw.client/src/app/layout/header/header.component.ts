import { HttpClient } from '@angular/common/http';
import { Component, OnInit, SecurityContext } from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { Observable } from 'rxjs';
import { MicrosoftAuthorizationControllerService } from 'src/app/services/microsoft-authorization-controller.service';

const GRAPH_ENDPOINT = 'https://graph.microsoft.com/v1.0/me';

type ProfileType = {
  givenName?: string;
  surname?: string;
  userPrincipalName?: string;
  id?: string;
  displayName?: string;
  jobTitle?: string;
  email?: string;
};

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent implements OnInit {
  profile!: ProfileType;
  image?: SafeUrl;
  imageUrl: string = '';

  constructor(
    private http: HttpClient,
    private sanitizer: DomSanitizer,
    private microsoftAuthorizationControllerService: MicrosoftAuthorizationControllerService
  ) {}

  ngOnInit() {
    this.getProfile();
    this.getImageUrl();
  }

  getProfile() {
    this.http.get(GRAPH_ENDPOINT).subscribe((profile) => {
      this.profile = profile;
    });
  }

  getImageBlob(): Observable<Blob> {
    return this.http.get(`${GRAPH_ENDPOINT}/photo/$value`, {
      responseType: 'blob',
    });
  }

  getImageUrl(): void {
    this.getImageBlob().subscribe((blob: any) => {
      let objectURL = URL.createObjectURL(blob);
      this.image = this.sanitizer.bypassSecurityTrustUrl(objectURL);
      this.imageUrl =
        this.sanitizer.sanitize(SecurityContext.RESOURCE_URL, this.image) ?? '';
    });
  }

  logout(): void {
    this.microsoftAuthorizationControllerService.logout();
  }
}
