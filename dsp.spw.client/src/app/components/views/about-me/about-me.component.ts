import { Component, OnInit } from '@angular/core';
import { ResearchersService } from 'src/app/services/api/researchers.service';
import {
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

  constructor(
    private researchersService: ResearchersService,
    private microsoftGraphService: MicrosoftGraphService
  ) {}

  ngOnInit(): void {
    /* TODO: Create account
    this.microsoftGraphService.getMe().subscribe((graphProfile) => {
      let model: PublisherProfileIdentityModel = {
        firstName: graphProfile.givenName,
        lastName: graphProfile.surname,
        email: graphProfile.mail!,
      };
      this.researchersService
        .getOrCreateInfo(model)
        .subscribe((moduleProfile) => {
          this.profile = moduleProfile;
        });
    }); */
    this.researchersService.getInfo().subscribe((moduleProfile) => {
      this.researcherInformationModel = moduleProfile;
    });
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
}
