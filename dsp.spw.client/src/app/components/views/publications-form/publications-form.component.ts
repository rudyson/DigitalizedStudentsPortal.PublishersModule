import { Component, OnDestroy, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ValidationErrors,
  Validators,
} from '@angular/forms';

@Component({
  selector: 'app-publications-form',
  templateUrl: './publications-form.component.html',
  styleUrl: './publications-form.component.scss',
})
export class PublicationsFormComponent implements OnInit {
  publicationTypes = [
    { label: 'Стаття у журналі', value: 'article1' },
    { label: 'Стаття конференції', value: 'article2' },
    { label: 'Патент', value: 'article3' },
    { label: 'Підручник', value: 'article4' },
    { label: 'Навчально-методичний посібник', value: 'article5' },
    { label: 'Тези конференції', value: 'article6' },
    { label: 'Замітка', value: 'article7' },
  ];

  publicationCategories = [
    {
      label: 'Категорія А (Scopus, Web of Science)',
      value: 'category_a',
    },
    { label: 'Категорія Б (Фахове)', value: 'category_b' },
    { label: 'Категорія В (нефахове)', value: 'category_c' },
  ];

  selectedPublicationType?: string;
  selectedPublicationCategory?: string;
  isInternational: boolean = false;
  isWithStudent: boolean = false;
  publicationYear?: Date;
  publicationPages?: string;
  pageFirst?: number;
  pageLast?: number;
  conferenceDates?: Date[];
  conferenceCity?: string;
  conferenceName?: string;
  magazineIssue?: string;
  magazineNumber?: string;
  magazineName?: string;
  totalPages?: string;
  authorPages?: string;
  printingInfo?: string;
  isbn?: string;
  issn?: string;
  doi?: string;
  url?: string;

  isConferenceInformation: boolean = false;

  publicationsForm: FormGroup;

  constructor(private formBuilder: FormBuilder) {
    this.publicationsForm = this.formBuilder.group({
      title: ['', Validators.required],
      type: ['', Validators.required],
      category: ['', Validators.required],
      year: [''],
      pages: [''],
      pagesAuthor: [''],
      publishingName: [''],

      isWithStudent: [false],
      isInternational: [false],
      conferenceName: [''],
      conferenceStartDate: [''],
      conferenceEndDate: [''],
      conferenceDates: [''],
      conferenceCountry: [''],
      conferenceCity: [''],

      magazineName: [''],
      magazineIssue: [''],
      magazineNumber: [''],
      pageFirst: [''],
      pageLast: [''],

      doi: [''],
      isbn: [''],
      issn: [''],
      url: [''],
    });
  }
  ngOnInit(): void {
    console.log('Method not implemented.');
  }

  onSubmit(): void {
    if (this.publicationsForm.valid) {
      console.log(this.publicationsForm.value);
    } else {
      this.logValidationErrors(this.publicationsForm);
    }
  }

  logValidationErrors(formGroup: FormGroup): void {
    Object.keys(formGroup.controls).forEach((key) => {
      const control = formGroup.get(key);
      if (control && control.invalid) {
        this.logControlErrors(key, control.errors);
      }
    });
  }

  logControlErrors(controlName: string, errors: ValidationErrors | null): void {
    if (errors) {
      console.error(`Validation errors for ${controlName}:`, errors);
    }
  }
}
