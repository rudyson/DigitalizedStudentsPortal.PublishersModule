import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  FormArray,
  Validators,
  ValidationErrors,
} from '@angular/forms';
import { AutoCompleteCompleteEvent } from 'primeng/autocomplete';
import {
  PublicationCategory,
  PublicationTypes,
} from 'src/app/services/api/publications.models';
import { ResearchersService } from 'src/app/services/api/researchers.service';
import {
  ResearcherPseudonymSearchModel,
  ResearcherSearchModel,
} from 'src/app/services/api/researchers.service.models';

type PublicationForm = FormGroup<{
  title: FormControl<string | null>;
  type: FormControl<PublicationTypes | null>;
  category: FormControl<PublicationCategory | null>;
  year: FormControl<Date | null>;
  pages: FormControl<number[] | null>;
  pagesAuthor: FormControl<number | null>;
  publishingName: FormControl<string | null>;

  isWithStudent: FormControl<boolean | null>;
  isInternational: FormControl<boolean | null>;
  conferenceName: FormControl<string | null>;
  conferenceStartDate: FormControl<Date | null>;
  conferenceEndDate: FormControl<Date | null>;
  conferenceDates: FormControl<Date[] | null>;
  conferenceCountry: FormControl<string | null>;
  conferenceCity: FormControl<string | null>;

  magazineName: FormControl<string | null>;
  magazineIssue: FormControl<string | null>;
  magazineNumber: FormControl<number | null>;
  pageFirst: FormControl<number | null>;
  pageLast: FormControl<number | null>;

  doi: FormControl<string | null>;
  isbn: FormControl<string | null>;
  issn: FormControl<string | null>;
  url: FormControl<string | null>;

  internalAuthors: FormArray<InternalAuthorFormControl>;
  externalAuthors: FormControl<string[] | null>;
}>;

type InternalAuthorFormControl = FormGroup<{
  author: FormControl<ResearcherSearchModel | null>;
  pseudonym: FormControl<string | null>;
}>;

@Component({
  selector: 'app-publications-form',
  templateUrl: './publications-form.component.html',
  styleUrls: ['./publications-form.component.scss'],
})
export class PublicationsFormComponent implements OnInit {
  translatedPublicationCategories: {
    label: string;
    value: PublicationCategory;
  }[] = [];
  translatedPublicationTypes: { label: string; value: PublicationTypes }[] = [];

  publicationTypes = PublicationTypes;
  publicationCategories = PublicationCategory;

  foundResearchers: ResearcherSearchModel[] = [];
  foundResearcherPseudonyms: ResearcherPseudonymSearchModel[] = [];

  onAuthorSearch($event: AutoCompleteCompleteEvent) {
    if ($event.query) {
      this.researchersService
        .searchResearchers($event.query)
        .subscribe((response) => {
          this.foundResearchers = response;
        });
    } else {
      this.foundResearchers = [];
    }
  }

  onAuthorPseudonymSearch(researcherId: number) {
    this.researchersService
      .getPseudonyms(researcherId)
      .subscribe((response) => {
        this.foundResearcherPseudonyms = response;
      });
  }

  ngOnInit(): void {
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
  conferenceDates?: Date[];

  isConferenceInformation: boolean = false;

  publicationsForm: PublicationForm = this.formBuilder.group({
    title: this.formBuilder.control<string | null>('', Validators.required),
    type: this.formBuilder.control<PublicationTypes | null>(
      PublicationTypes.Article,
      Validators.required
    ),
    category: this.formBuilder.control<PublicationCategory | null>(
      PublicationCategory.C,
      Validators.required
    ),
    year: this.formBuilder.control<Date | null>(
      new Date(),
      Validators.required
    ),
    pages: this.formBuilder.control<number[] | null>(null),
    pagesAuthor: this.formBuilder.control<number | null>(null),
    publishingName: this.formBuilder.control<string | null>(null),

    isWithStudent: this.formBuilder.control<boolean | null>(false),
    isInternational: this.formBuilder.control<boolean | null>(false),
    conferenceName: this.formBuilder.control<string | null>(null),
    conferenceStartDate: this.formBuilder.control<Date | null>(null),
    conferenceEndDate: this.formBuilder.control<Date | null>(null),
    conferenceDates: this.formBuilder.control<Date[] | null>(null),
    conferenceCountry: this.formBuilder.control<string | null>(null),
    conferenceCity: this.formBuilder.control<string | null>(null),

    magazineName: this.formBuilder.control<string | null>(null),
    magazineIssue: this.formBuilder.control<string | null>(null),
    magazineNumber: this.formBuilder.control<number | null>(null),
    pageFirst: this.formBuilder.control<number | null>(null),
    pageLast: this.formBuilder.control<number | null>(null),

    doi: this.formBuilder.control<string | null>(null),
    isbn: this.formBuilder.control<string | null>(null),
    issn: this.formBuilder.control<string | null>(null),
    url: this.formBuilder.control<string | null>(null),

    internalAuthors: this.formBuilder.array<InternalAuthorFormControl>([
      this.generateInternalAuthorFormControl(),
    ]),
    externalAuthors: this.formBuilder.control<string[] | null>(null),
  }) as PublicationForm;

  constructor(
    private formBuilder: FormBuilder,
    private researchersService: ResearchersService
  ) {}

  generateInternalAuthorFormControl(): InternalAuthorFormControl {
    return this.formBuilder.group({
      author: this.formBuilder.control<ResearcherSearchModel | null>(null),
      pseudonym:
        this.formBuilder.control<ResearcherPseudonymSearchModel | null>(null),
    }) as InternalAuthorFormControl;
  }

  addNewInternalAuthorFormControl(): void {
    this.publicationsForm.controls.internalAuthors.push(
      this.generateInternalAuthorFormControl()
    );
  }

  deleteInternalAuthorFormControl(index: number): void {
    this.publicationsForm.controls.internalAuthors.removeAt(index);
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
