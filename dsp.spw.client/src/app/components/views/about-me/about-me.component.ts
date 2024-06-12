import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ResearchersService } from 'src/app/services/api/researchers.service';
import {
  PublisherProfileIdentityModel,
  ResearcherGetInformationModel,
  ScienceDatabaseTypes,
} from 'src/app/services/api/researchers.service.models';
import { MicrosoftGraphService } from 'src/app/services/microsoft/microsoft-graph.service';

@Component({
  selector: 'app-about-me',
  templateUrl: './about-me.component.html',
  styleUrl: './about-me.component.scss',
})
export class AboutMeComponent implements OnInit {
  researcherInformationModel?: ResearcherGetInformationModel;
  profileAvatar: string = '';
  id: string | null = null;
  loading: boolean = true;
  needsRegistration: boolean = false;
  registrationModel?: PublisherProfileIdentityModel;

  constructor(
    private researchersService: ResearchersService,
    private microsoftGraphService: MicrosoftGraphService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');

    if (this.id) {
      this.researchersService.getInfoById(Number(this.id)).subscribe(
        (response) => {
          this.researcherInformationModel = response;
          this.loading = false;
        },
        (error: HttpErrorResponse) => {
          this.loading = false;
        }
      );
    } else {
      this.researchersService.getMyInfo().subscribe(
        (response) => {
          this.researcherInformationModel = response;
          this.loading = false;
        },
        (error: HttpErrorResponse) => {
          this.loading = false;
          if (error.status === 404) {
            this.needsRegistration = true;
            this.getGraphInfo();
          }
        }
      );
    }
  }

  getDatabaseIconName(type: ScienceDatabaseTypes): string {
    let fileName = '';
    switch (type) {
      case ScienceDatabaseTypes.Faculty:
        fileName = '';
        break;
      case ScienceDatabaseTypes.Scopus:
        fileName = 'scopus.ico';
        break;
      case ScienceDatabaseTypes.WebOfScience:
        fileName = 'clarivate_web_of_science.svg';
        break;
      case ScienceDatabaseTypes.GoogleAcademy:
        fileName = 'google_scholar.svg';
        break;
    }
    return fileName;
  }

  getGraphInfo() {
    this.microsoftGraphService.getMe().subscribe((graphProfile) => {
      this.registrationModel = {
        firstName: graphProfile.givenName,
        lastName: graphProfile.surname,
        email: graphProfile.mail!,
      };
    });
  }
  register() {
    if (this.registrationModel) {
      this.researchersService
        .getOrCreateInfo(this.registrationModel)
        .subscribe((moduleProfile) => {
          window.location.reload();
        });
    }
  }
}
