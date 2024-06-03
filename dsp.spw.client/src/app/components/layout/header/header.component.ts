import { Component, OnInit, SecurityContext } from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { MicrosoftAuthorizationControllerService } from 'src/app/services/microsoft/microsoft-authorization-controller.service';
import {
  MicrosoftGraphMeResponse,
  MicrosoftGraphService,
} from 'src/app/services/microsoft/microsoft-graph.service';

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
  profile!: MicrosoftGraphMeResponse;
  image?: SafeUrl;
  imageUrl: string = '';

  constructor(
    private sanitizer: DomSanitizer,
    private microsoftAuthorizationControllerService: MicrosoftAuthorizationControllerService,
    private microsoftGraphService: MicrosoftGraphService
  ) {}

  ngOnInit() {
    this.getProfile();
    this.getImageUrl();
  }

  getProfile() {
    this.microsoftGraphService.getMe().subscribe((profile) => {
      this.profile = profile;
    });
  }

  getImageUrl(): void {
    this.microsoftGraphService.getMePhoto().subscribe((blob: any) => {
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
