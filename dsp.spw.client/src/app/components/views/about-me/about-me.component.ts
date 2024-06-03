import { Component, OnInit } from '@angular/core';
import { ResearchersService } from 'src/app/services/api/researchers.service';
import { ResearcherGetInformationModel } from 'src/app/services/api/researchers.service.models';
import { MicrosoftGraphService } from 'src/app/services/microsoft/microsoft-graph.service';

@Component({
  selector: 'app-about-me',
  templateUrl: './about-me.component.html',
  styleUrl: './about-me.component.scss',
})
export class AboutMeComponent implements OnInit {
  fullName: string = 'John Doe';
  avatarUrl: string = 'https://via.placeholder.com/150'; // Replace with your avatar URL
  pseudonyms: string[] = ['Дядюшкін Р.С.', 'Ruslan Diadiushkin'];
  userId: string = '123456';
  researcherInformationModel?: ResearcherGetInformationModel;

  constructor(
    private researchersService: ResearchersService,
    private microsoftGraphService: MicrosoftGraphService
  ) {}

  ngOnInit(): void {
    /*
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
}
