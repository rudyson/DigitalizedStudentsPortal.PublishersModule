import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-test-api-call',
  templateUrl: './test-api-call.component.html',
  styleUrl: './test-api-call.component.scss',
})
export class TestApiCallComponent {
  protected test: string | undefined;
  constructor(private http: HttpClient) {
    this.http
      .get('https://localhost:7239/test/gettestwithrole', {
        responseType: 'text',
      })
      .subscribe((x) => (this.test = x));
  }
}
