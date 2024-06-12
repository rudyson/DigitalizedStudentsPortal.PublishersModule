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
      label: 'aboutme',
      icon: 'pi pi-user',
      routerLink: 'researchers/me',
    },
    {
      label: 'researchers',
      icon: 'pi pi-users',
      routerLink: 'researchers/all',
    },
    {
      label: 'publications',
      icon: 'pi pi-file-check',
      routerLink: 'publications/all',
    },
    {
      label: 'newpublication',
      icon: 'pi pi-plus',
      routerLink: 'publications/new',
    },
    {
      label: 'reports',
      icon: 'pi pi-receipt',
      routerLink: 'reports',
    },
    { label: 'disciplines', icon: 'pi pi-briefcase' },
  ];
}
