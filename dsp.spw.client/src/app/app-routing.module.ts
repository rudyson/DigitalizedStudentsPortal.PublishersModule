import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MsalGuard } from '@azure/msal-angular';
import { BrowserUtils } from '@azure/msal-browser';
import { HomeComponent } from './components/home/home.component';
import { ProfileComponent } from './components/profile/profile.component';
import { AboutMeComponent } from './components/views/about-me/about-me.component';
import { PublicationsFormComponent } from './components/views/publications-form/publications-form.component';
import { PublicationsListComponent } from './components/views/publications/publications-list/publications-list.component';
import { ResearchersListComponent } from './components/views/researchers/researchers-list/researchers-list.component';
import { LoginPageComponent } from './components/layout/login-page/login-page.component';

const routes: Routes = [
  {
    path: 'profile',
    component: ProfileComponent,
    canActivate: [MsalGuard],
  },
  {
    path: 'home',
    component: HomeComponent,
    canActivate: [MsalGuard],
  },
  {
    path: 'login',
    component: LoginPageComponent,
  },
  {
    // Needed for Error routing
    path: 'error',
    component: HomeComponent,
  },
  {
    path: 'researchers',
    children: [
      { path: 'all', component: ResearchersListComponent },
      { path: 'me', component: AboutMeComponent },
      { path: '**', redirectTo: 'all' },
    ],
    canActivate: [MsalGuard],
  },
  {
    path: 'publications',
    children: [
      { path: 'all', component: PublicationsListComponent },
      { path: 'new', component: PublicationsFormComponent },
      { path: '**', redirectTo: 'all' },
    ],
    canActivate: [MsalGuard],
  },
  { path: '**', redirectTo: 'researchers/me' },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, {
      // Don't perform initial navigation in iframes or popups
      initialNavigation:
        !BrowserUtils.isInIframe() && !BrowserUtils.isInPopup()
          ? 'enabledNonBlocking'
          : 'disabled', // Set to enabledBlocking to use Angular Universal
    }),
  ],
  exports: [RouterModule],
})
export class AppRoutingModule {}
