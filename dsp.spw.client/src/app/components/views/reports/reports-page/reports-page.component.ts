import { Component } from '@angular/core';
import { ReportsService } from 'src/app/services/api/reports.service';

@Component({
  selector: 'app-reports-page',
  templateUrl: './reports-page.component.html',
  styleUrl: './reports-page.component.scss',
})
export class ReportsPageComponent {
  report?: string;
  constructor(private reportsService: ReportsService) {}

  generateSimpleReport() {
    this.reportsService.getSimpleReport().then((response) => {
      if (response.data) {
        this.report = response.data;
      }
    });
  }
  getReportUrl(): string | undefined {
    if (this.report) {
      return this.reportsService.getReportFile(this.report);
    }
    return undefined;
  }
}
