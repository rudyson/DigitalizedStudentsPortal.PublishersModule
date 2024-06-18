import { Component, OnInit, SecurityContext } from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { MicrosoftAuthorizationControllerService } from 'src/app/services/microsoft/microsoft-authorization-controller.service';
import {
  MicrosoftGraphMeResponse,
  MicrosoftGraphService,
} from 'src/app/services/microsoft/microsoft-graph.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent implements OnInit {
  profile!: MicrosoftGraphMeResponse;
  image?: SafeUrl;

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
    this.microsoftGraphService.getMePhoto().subscribe((blob: Blob) => {
      let objectURL = URL.createObjectURL(blob);
      this.image = this.sanitizer.bypassSecurityTrustUrl(objectURL);
    });
  }

  logout(): void {
    this.microsoftAuthorizationControllerService.logout();
  }
}
