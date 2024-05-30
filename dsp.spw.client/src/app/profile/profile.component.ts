import { HttpClient } from '@angular/common/http';
import { Component, OnInit, SecurityContext } from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { Observable } from 'rxjs';

const GRAPH_ENDPOINT = 'https://graph.microsoft.com/v1.0/me';

type ProfileType = {
  givenName?: string;
  surname?: string;
  userPrincipalName?: string;
  id?: string;
};

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss',
})
export class ProfileComponent implements OnInit {
  profile!: ProfileType;
  image?: SafeUrl;
  imageUrl: string = '';

  constructor(private http: HttpClient, private sanitizer: DomSanitizer) {}

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
      console.log('loh');
      this.image = this.sanitizer.bypassSecurityTrustUrl(objectURL);
      this.imageUrl =
        this.sanitizer.sanitize(SecurityContext.RESOURCE_URL, this.image) ?? '';
    });
  }
}
