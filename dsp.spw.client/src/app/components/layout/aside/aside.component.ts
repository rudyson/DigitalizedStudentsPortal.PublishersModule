import { Component } from '@angular/core';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-aside',
  templateUrl: './aside.component.html',
  styleUrl: './aside.component.scss',
})
export class AsideComponent {
  sideMenuItems: MenuItem[] = [
    {
      label: 'Home',
      icon: 'pi pi-home',
      routerLink: '',
    },
    {
      label: 'About me',
      icon: 'pi pi-user',
      routerLink: 'researchers/me',
    },
    {
      label: 'Researchers',
      icon: 'pi pi-users',
      routerLink: 'researchers',
    },
    {
      label: 'Publications',
      icon: 'pi pi-file-check',
      routerLink: 'publications',
    },
    {
      label: 'Reports',
      icon: 'pi pi-receipt',
    },
    { label: 'Conferences', icon: 'pi pi-briefcase' },
    { label: 'Science groups', icon: 'pi pi-users' },
  ];
}
