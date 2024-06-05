import { Component, OnDestroy, OnInit } from '@angular/core';

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
  conferenceDates?: Date[];
  conferenceCity?: string;
  conferenceName?: string;
  journalIssue?: string;
  journalNumber?: string;
  journalName?: string;
  totalPages?: string;
  authorPages?: string;
  printingInfo?: string;
  isbn?: string;
  issn?: string;
  doi?: string;
  url?: string;

  isConferenceInformation: boolean = false;

  constructor() {}
  ngOnInit(): void {
    console.log('Method not implemented.');
  }
}
