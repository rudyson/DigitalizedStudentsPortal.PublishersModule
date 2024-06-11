import { Component, OnInit } from '@angular/core';
import { TablePageEvent } from 'primeng/table';
import { PublicationGetInformationModel } from 'src/app/services/api/publications.models';
import { PublicationsService } from 'src/app/services/api/publications.service';

@Component({
  selector: 'app-publications-list',
  templateUrl: './publications-list.component.html',
  styleUrl: './publications-list.component.scss',
})
export class PublicationsListComponent implements OnInit {
  rows: number = 10;
  publications: PublicationGetInformationModel[] = [];
  first: number = 0;
  totalRecords: number = this.rows;
  loading: boolean = false;

  constructor(private publicationsService: PublicationsService) {}
  ngOnInit(): void {
    this.loadItems();
  }

  loadItems() {
    this.loading = true;
    this.publicationsService
      .getAllPaginated(this.first, this.rows)
      .subscribe((response) => {
        if (response.data) {
          this.publications = response.data;
          this.totalRecords = response.total;
          this.loading = false;
        }
      });
  }

  onPageChanged($event: TablePageEvent) {
    this.first = $event.first;
    this.rows = $event.rows;
    this.loadItems();
  }
}
