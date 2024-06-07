import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
const API_ENDPOINT = 'https://localhost:7239/api';

@Injectable({
  providedIn: 'root',
})
export class ReportsService {
  constructor(private http: HttpClient) {}
}
