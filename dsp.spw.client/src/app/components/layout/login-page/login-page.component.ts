import { Component } from '@angular/core';
import { MicrosoftAuthorizationControllerService } from 'src/app/services/microsoft/microsoft-authorization-controller.service';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrl: './login-page.component.scss',
})
export class LoginPageComponent {
  constructor(
    private microsoftAuthorizationControllerService: MicrosoftAuthorizationControllerService
  ) {}
  loginWithMicrosoft() {
    this.microsoftAuthorizationControllerService.login();
  }
}
