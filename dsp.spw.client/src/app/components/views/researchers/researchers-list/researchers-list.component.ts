import { Component, OnInit } from '@angular/core';
import { TablePageEvent } from 'primeng/table';
import { ResearchersService } from 'src/app/services/api/researchers.service';
import { ResearcherGetInformationModel } from 'src/app/services/api/researchers.service.models';

@Component({
  selector: 'app-researchers-list',
  templateUrl: './researchers-list.component.html',
  styleUrl: './researchers-list.component.scss',
})
export class ResearchersListComponent implements OnInit {
  rows: number = 10;
  researchers: ResearcherGetInformationModel[] = [];
  first: number = 0;
  totalRecords: number = this.rows;
  loading: boolean = false;

  constructor(private researchersService: ResearchersService) {}
  ngOnInit(): void {
    this.loadItems();
  }

  loadItems() {
    this.loading = true;
    this.researchersService
      .getAllPaginated(this.first, this.rows)
      .then((response) => {
        if (response.data) {
          this.researchers = response.data;
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
}
