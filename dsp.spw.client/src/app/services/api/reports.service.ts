import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ResponseWrapper } from './common.models';

@Injectable({
  providedIn: 'root',
})
export class ReportsService {
  api: string = `${environment.api}/api`;
  reports: string = `${environment.api}/files/reports`;
  constructor(private http: HttpClient) {}

  getSimpleReport() {
    return this.http.get<ResponseWrapper<string>>(`${this.api}/report/simple`);
  }

  getReportFile(fileName: string): string {
    return `${this.reports}/${fileName}`;
  }
}
