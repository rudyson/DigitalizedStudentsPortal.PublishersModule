import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ValidationErrors,
  Validators,
} from '@angular/forms';
import {
  PublicationCategory,
  PublicationTypes,
} from 'src/app/services/api/publications.models';

@Component({
  selector: 'app-publications-form',
  templateUrl: './publications-form.component.html',
  styleUrl: './publications-form.component.scss',
})
export class PublicationsFormComponent implements OnInit {
  translatedPublicationCategories: {
    label: string;
    value: PublicationCategory;
  }[] = [];
  translatedPublicationTypes: { label: string; value: PublicationTypes }[] = [];

  publicationTypes = PublicationTypes;
  publicationCategories = PublicationCategory;

  ngOnInit(): void {
    console.log(Object.entries(PublicationTypes));
    this.translatedPublicationTypes = Object.values(this.publicationTypes)
      .filter((key) => !isNaN(+key))
      .map((type) => ({
        label: PublicationTypes[type as number] as string,
        value: type as PublicationTypes,
      }));
    this.translatedPublicationCategories = Object.values(
      this.publicationCategories
    )
      .filter((key) => !isNaN(+key))
      .map((type) => ({
        label: PublicationCategory[type as number] as string,
        value: type as PublicationCategory,
      }));
  }

  selectedPublicationType: PublicationTypes = PublicationTypes.Article;
  selectedPublicationCategory: PublicationCategory = PublicationCategory.C;
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
      type: [PublicationTypes.Article, Validators.required],
      category: [PublicationCategory.C, Validators.required],
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

      internalAuthors: [[]],
      externalAuthors: [[]],
    });
  }

  onSubmit(): void {
    this.publicationsForm.markAllAsTouched();
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
