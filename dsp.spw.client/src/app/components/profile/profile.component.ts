import { HttpClient } from '@angular/common/http';
import { Component, OnInit, SecurityContext } from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import {
  MicrosoftGraphMeResponse,
  MicrosoftGraphService,
} from 'src/app/services/microsoft/microsoft-graph.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss',
})
export class ProfileComponent implements OnInit {
  profile!: MicrosoftGraphMeResponse;
  image?: SafeUrl;
  imageUrl: string = '';

  constructor(
    private http: HttpClient,
    private sanitizer: DomSanitizer,
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
}
