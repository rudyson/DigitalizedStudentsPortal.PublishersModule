import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
const BACKEND = {
  HOST: 'https://localhost:7239/',
  API: 'https://localhost:7239/api',
  REPORTS: 'https://localhost:7239/files/reports',
};
const API_ENDPOINT = 'https://localhost:7239/api';

export interface ResponseWrapper<T> {
  data?: T;
}

@Injectable({
  providedIn: 'root',
})
export class ReportsService {
  constructor(private http: HttpClient) {}

  getSimpleReport() {
    return this.http.get<ResponseWrapper<string>>(
      `${API_ENDPOINT}/report/simple`
    );
  }

  getReportFile(fileName: string): string {
    return `${BACKEND.REPORTS}/${fileName}`;
  }
}
