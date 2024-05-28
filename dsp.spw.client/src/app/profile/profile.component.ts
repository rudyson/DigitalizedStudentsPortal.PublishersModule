import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

const GRAPH_ENDPOINT = 'https://graph.microsoft.com/v1.0/me';
const ASPNET_ENDPOINT = 'https://localhost:7239';

type ProfileType = {
  givenName?: string;
  surname?: string;
  userPrincipalName?: string;
  id?: string;
  displayName?: string;
  jobTitle?: string;
  mail?: string;
};

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent implements OnInit {
  profile?: ProfileType;
  profilePicture?: SafeResourceUrl;

  constructor(private http: HttpClient, private domSanitizer: DomSanitizer) {}

  ngOnInit() {
    this.getProfile();
    this.getTest();
  }

  getProfile() {
    this.http.get(GRAPH_ENDPOINT).subscribe((profile) => {
      this.profile = profile;
    });
  }

  getProfilePicture() {
    this.http
      .get(`${GRAPH_ENDPOINT}/photo/$value`, { responseType: 'blob' })
      .subscribe((response) => {
        var urlCreator = window.URL || window.webkitURL;
        this.profilePicture = this.domSanitizer.bypassSecurityTrustResourceUrl(
          urlCreator.createObjectURL(response)
        );
      });
  }

  getTest() {
    return this.http
      .get(`${ASPNET_ENDPOINT}/test/gettest`, { responseType: 'text' })
      .subscribe((result) => {
        console.log(result);
      });
  }
}
