import { Component, OnInit } from '@angular/core';
import {
  PublisherProfileIdentityModel,
  ResearcherGetInformationModel,
  ResearchersService,
} from 'src/app/services/api/researchers.service';
import { MicrosoftGraphService } from 'src/app/services/microsoft/microsoft-graph.service';

@Component({
  selector: 'app-about-me',
  templateUrl: './about-me.component.html',
  styleUrl: './about-me.component.scss',
})
export class AboutMeComponent implements OnInit {
  fullName: string = 'John Doe';
  avatarUrl: string = 'https://via.placeholder.com/150'; // Replace with your avatar URL
  pseudonyms: string[] = ['A', 'B', 'C'];
  userId: string = '123456';
  profile?: ResearcherGetInformationModel;

  constructor(
    private researchersService: ResearchersService,
    private microsoftGraphService: MicrosoftGraphService
  ) {}

  ngOnInit(): void {
    this.microsoftGraphService.getMe().subscribe((graphProfile) => {
      let model: PublisherProfileIdentityModel = {
        firstName: graphProfile.givenName,
        lastName: graphProfile.surname,
        email: graphProfile.mail!,
      };
      this.researchersService.getInfo(model).subscribe((moduleProfile) => {
        this.profile = moduleProfile;
      });
    });
  }
}
