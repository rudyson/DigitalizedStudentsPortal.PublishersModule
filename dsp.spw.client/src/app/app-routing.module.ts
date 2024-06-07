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

const routes: Routes = [
  {
    path: 'profile',
    component: ProfileComponent,
    canActivate: [MsalGuard],
  },
  {
    path: '',
    component: HomeComponent,
  },
  { path: 'about-me', component: AboutMeComponent },
  {
    // Needed for Error routing
    path: 'error',
    component: HomeComponent,
  },
  {
    path: 'researchers',
    children: [
      { path: '', component: ResearchersListComponent },
      { path: 'me', component: AboutMeComponent },
    ],
    canActivate: [MsalGuard],
  },
  {
    path: 'publications',
    children: [
      { path: '', component: PublicationsListComponent },
      { path: 'new', component: PublicationsFormComponent },
      { path: '**', redirectTo: 'new' },
    ],
    canActivate: [MsalGuard],
  },
  { path: '**', redirectTo: '' },
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
