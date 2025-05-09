import { Component, OnInit } from '@angular/core';
import { PaginatorState } from 'primeng/paginator';
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
  searchQuery: string = '';

  constructor(private publicationsService: PublicationsService) {}
  ngOnInit(): void {
    this.loadItems();
  }

  onPublicationSearch() {
    this.loading = true;
    if (this.searchQuery) {
      this.publicationsService
        .search(this.searchQuery)
        .then((response) => {
          this.publications = response.data ?? [];
        })
        .finally(() => {
          this.loading = false;
        });
    } else {
      this.searchQuery = '';
      this.loadItems();
    }
  }

  loadItems() {
    this.loading = true;
    this.publicationsService
      .getAllPaginated(this.first, this.rows)
      .then((response) => {
        if (response.data) {
          this.publications = response.data;
          this.totalRecords = response.count ?? this.rows;
        }
      })
      .finally(() => {
        this.loading = false;
      });
  }

  onPageChanged($event: TablePageEvent) {
    this.first = $event.first;
    this.rows = $event.rows;
    this.loadItems();
  }

  onPaginatorPageChanged($event: PaginatorState) {
    this.first = $event.first ?? this.first;
    this.rows = $event.rows ?? this.rows;
    this.loadItems();
  }

  getPublicationAuthors(publication: PublicationGetInformationModel): string[] {
    const internal = publication.contributors.map((x) => x.shortName);
    const external = publication.externalContributors.map((x) => x.shortName);
    return internal.concat(external);
  }
}
