import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TabMenuModule } from 'primeng/tabmenu';
import { MenuModule } from 'primeng/menu';
import { CardModule } from 'primeng/card';
import { BadgeModule } from 'primeng/badge';
import { RippleModule } from 'primeng/ripple';
import { AvatarModule } from 'primeng/avatar';
import { ButtonModule } from 'primeng/button';
import { TooltipModule } from 'primeng/tooltip';
import { ChipModule } from 'primeng/chip';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { ChipsModule } from 'primeng/chips';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { InputTextModule } from 'primeng/inputtext';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { DropdownModule } from 'primeng/dropdown';
import { CheckboxModule } from 'primeng/checkbox';
import { FloatLabelModule } from 'primeng/floatlabel';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FieldsetModule } from 'primeng/fieldset';
import { PanelModule } from 'primeng/panel';
import { CalendarModule } from 'primeng/calendar';
import { TableModule } from 'primeng/table';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { InputNumberModule } from 'primeng/inputnumber';
import { PaginatorModule } from 'primeng/paginator';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';

import { HomeComponent } from './components/home/home.component';
import { ProfileComponent } from './components/profile/profile.component';

import {
  HTTP_INTERCEPTORS,
  HttpClientModule,
  provideHttpClient,
  withInterceptors,
  withInterceptorsFromDi,
} from '@angular/common/http';
import {
  IPublicClientApplication,
  PublicClientApplication,
  BrowserCacheLocation,
  LogLevel,
  InteractionType,
} from '@azure/msal-browser';
import {
  MSAL_INSTANCE,
  MSAL_INTERCEPTOR_CONFIG,
  MsalInterceptorConfiguration,
  MSAL_GUARD_CONFIG,
  MsalGuardConfiguration,
  MsalBroadcastService,
  MsalService,
  MsalGuard,
  MsalRedirectComponent,
  MsalModule,
  MsalInterceptor,
} from '@azure/msal-angular';
import { TestApiCallComponent } from './components/test-api-call/test-api-call.component';
import { HeaderComponent } from './components/layout/header/header.component';
import { AsideComponent } from './components/layout/aside/aside.component';
import { AboutMeComponent } from './components/views/about-me/about-me.component';
import { LoginPageComponent } from './components/layout/login-page/login-page.component';
import { TranslocoRootModule } from './transloco-root.module';
import { PublicationsFormComponent } from './components/views/publications-form/publications-form.component';
import { LoadingPageComponent } from './components/layout/loading-page/loading-page.component';
import { PublicationsListComponent } from './components/views/publications/publications-list/publications-list.component';
import { ResearchersListComponent } from './components/views/researchers/researchers-list/researchers-list.component';
import { ReportsPageComponent } from './components/views/reports/reports-page/reports-page.component';
import { environment } from 'src/environments/environment';
import { errorInterceptor } from './interceptors/error.interceptor';

const isIE =
  window.navigator.userAgent.indexOf('MSIE ') > -1 ||
  window.navigator.userAgent.indexOf('Trident/') > -1;

export function loggerCallback(logLevel: LogLevel, message: string) {
  console.log(message);
}

export function MSALInstanceFactory(): IPublicClientApplication {
  return new PublicClientApplication({
    auth: {
      clientId: environment.microsoft.entraId.clientId,
      authority: `https://login.microsoftonline.com/${environment.microsoft.entraId.tenantId}`,
      redirectUri: environment.microsoft.entraId.redirectUri,
    },
    cache: {
      cacheLocation: BrowserCacheLocation.LocalStorage,
      storeAuthStateInCookie: isIE, // set to true for IE 11
    },
    system: {
      loggerOptions: {
        loggerCallback,
        logLevel: LogLevel.Info,
        piiLoggingEnabled: false,
      },
    },
  });
}

export function MSALInterceptorConfigFactory(): MsalInterceptorConfiguration {
  const protectedResourceMap = new Map<string, Array<string>>();
  protectedResourceMap.set(environment.microsoft.graph, ['user.read']);
  protectedResourceMap.set(
    'localhost',
    environment.microsoft.entraId.exposedApis
  );

  return {
    interactionType: InteractionType.Redirect,
    protectedResourceMap,
  };
}

export function MSALGuardConfigFactory(): MsalGuardConfiguration {
  return {
    interactionType: InteractionType.Redirect,
    authRequest: {
      scopes: environment.microsoft.entraId.scopes,
    },
  };
}
@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    HeaderComponent,
    AsideComponent,
    ProfileComponent,
    TestApiCallComponent,
    AboutMeComponent,
    LoginPageComponent,
    PublicationsFormComponent,
    LoadingPageComponent,
    PublicationsListComponent,
    ResearchersListComponent,
    ReportsPageComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,

    TabMenuModule,
    MenuModule,
    BadgeModule,
    RippleModule,
    AvatarModule,
    ButtonModule,
    TooltipModule,
    CardModule,
    ChipModule,
    IconFieldModule,
    InputIconModule,
    ChipsModule,
    ProgressSpinnerModule,
    InputTextModule,
    InputTextareaModule,
    DropdownModule,
    CheckboxModule,
    FloatLabelModule,
    FieldsetModule,
    PanelModule,
    CalendarModule,
    TableModule,
    AutoCompleteModule,
    InputNumberModule,
    PaginatorModule,
    ConfirmDialogModule,
    ToastModule,

    MsalModule,
    TranslocoRootModule,
  ],
  providers: [
    ConfirmationService,
    MessageService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: MsalInterceptor,
      multi: true,
    },
    provideHttpClient(
      withInterceptorsFromDi(),
      withInterceptors([errorInterceptor])
    ),
    {
      provide: MSAL_INSTANCE,
      useFactory: MSALInstanceFactory,
    },
    {
      provide: MSAL_GUARD_CONFIG,
      useFactory: MSALGuardConfigFactory,
    },
    {
      provide: MSAL_INTERCEPTOR_CONFIG,
      useFactory: MSALInterceptorConfigFactory,
    },
    MsalService,
    MsalGuard,
    MsalBroadcastService,
  ],
  bootstrap: [AppComponent, MsalRedirectComponent],
})
export class AppModule {}
