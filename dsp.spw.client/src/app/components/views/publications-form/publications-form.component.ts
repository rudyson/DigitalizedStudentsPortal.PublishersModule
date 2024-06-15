import { Component, OnDestroy, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  FormArray,
  Validators,
  ValidationErrors,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslocoService } from '@jsverse/transloco';
import { ConfirmationService, MessageService } from 'primeng/api';
import { AutoCompleteCompleteEvent } from 'primeng/autocomplete';
import { Observable, Subject } from 'rxjs';
import { CanComponentDeactivate } from 'src/app/guards/deactivate/if-not-unsaved-changes.guard';
import {
  Publication,
  PublicationCategory,
  PublicationTypes,
} from 'src/app/services/api/publications.models';
import { PublicationsService } from 'src/app/services/api/publications.service';
import { ResearchersService } from 'src/app/services/api/researchers.service';
import {
  ResearcherPseudonymSearchModel,
  ResearcherSearchModel,
} from 'src/app/services/api/researchers.service.models';

type PublicationForm = FormGroup<{
  title: FormControl<string | null>;
  reference: FormControl<string | null>;
  type: FormControl<PublicationTypes>;
  category: FormControl<PublicationCategory>;
  year: FormControl<Date | null>;
  pages: FormControl<number[] | null>;
  pagesAuthor: FormControl<number | null>;
  publishingName: FormControl<string | null>;

  isWithStudent: FormControl<boolean | null>;
  isInternational: FormControl<boolean | null>;
  conferenceName: FormControl<string | null>;
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
export class PublicationsFormComponent
  implements OnInit, OnDestroy, CanComponentDeactivate
{
  destroy$ = new Subject<void>();

  translatedPublicationCategories: {
    label: string;
    value: PublicationCategory;
  }[] = [];
  translatedPublicationTypes: { label: string; value: PublicationTypes }[] = [];

  publicationTypes = PublicationTypes;
  publicationCategories = PublicationCategory;

  foundResearchers: ResearcherSearchModel[] = [];
  foundResearcherPseudonyms: ResearcherPseudonymSearchModel[] = [];

  currentDate = new Date();

  publicationsForm: PublicationForm = this.formBuilder.group({
    title: this.formBuilder.control<string | null>('', Validators.required),
    reference: this.formBuilder.control<string | null>('', Validators.required),
    type: this.formBuilder.control<PublicationTypes>(
      PublicationTypes.Article,
      Validators.required
    ),
    category: this.formBuilder.control<PublicationCategory>(
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
    private researchersService: ResearchersService,
    private publicationsService: PublicationsService,
    private confirmationService: ConfirmationService,
    private translocoService: TranslocoService,
    private messageService: MessageService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {}

  onSubmit(): void {
    this.publicationsForm.markAllAsTouched();
    if (this.publicationsForm.valid) {
      this.publicationsService
        .createPublication(this.publicationsForm.getRawValue() as Publication)
        .then((response) => {
          if (response.data) {
            this.router.navigate(['/publications/all']);
            this.messageService.add({
              summary: this.translocoService.translate(
                'views.publications.new.messages.successSubmit'
              ),
              severity: 'success',
              detail: response.data.reference,
            });
          }
        });
    } else {
      this.logValidationErrors(this.publicationsForm);
    }
  }

  public canDeactivate():
    | boolean
    | Observable<boolean>
    | Promise<boolean>
    | Subject<boolean> {
    const isChangesSaved = !this.publicationsForm.touched;
    if (isChangesSaved) {
      return isChangesSaved;
    }
    const deactivateSubject = new Subject<boolean>();
    this.confirmationService.confirm({
      header: this.translocoService.translate(
        'messages.warn.unsavedChanges.title'
      ),
      message: this.translocoService.translate(
        'messages.warn.unsavedChanges.description'
      ),
      accept: () => {
        deactivateSubject.next(true);
      },
      reject: () => {
        deactivateSubject.next(false);
      },
    });
    return deactivateSubject;
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

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.unsubscribe();
  }

  totalAuthorsCount(): number {
    const formModel = this.publicationsForm.getRawValue();
    return (
      formModel.externalAuthors?.length ?? 0 + formModel.internalAuthors.length
    );
  }

  isAuthorControlInvalid(): boolean {
    return this.totalAuthorsCount() === 0;
  }

  isControlInvalid(formControlName: string): boolean {
    const control = this.publicationsForm.get(formControlName);
    return control?.invalid ?? true;
  }

  isControlDirty(formControlName: string): boolean {
    const control = this.publicationsForm.get(formControlName);
    return (control?.dirty || control?.touched) ?? true;
  }

  onAuthorSearch($event: AutoCompleteCompleteEvent) {
    if ($event.query) {
      this.researchersService
        .searchResearchers($event.query)
        .then((response) => {
          this.foundResearchers = response.data ?? [];
        });
    } else {
      this.foundResearchers = [];
    }
  }

  onAuthorPseudonymSearch(researcherId: number) {
    this.researchersService.getPseudonyms(researcherId).then((response) => {
      this.foundResearcherPseudonyms = response.data ?? [];
    });
  }

  generateInternalAuthorFormControl(): InternalAuthorFormControl {
    return this.formBuilder.group({
      author: this.formBuilder.control<ResearcherSearchModel | null>(null),
      pseudonym:
        this.formBuilder.control<ResearcherPseudonymSearchModel | null>({
          value: null,
          disabled: false,
        }),
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
