import { Pipe, PipeTransform } from '@angular/core';
import { AcademicDegrees } from '../services/api/researchers.service.models';
//import { TranslocoService } from '@ngneat/transloco';

@Pipe({
  name: 'academicDegreeTransloco',
})
export class AcademicDegreeTranslocoPipe implements PipeTransform {
  //constructor(private translocoService: TranslocoService) {}

  transform(value: AcademicDegrees): string {
    return `AD:${value}`;
    //return this.translocoService.translate(`status.${value}`);
  }
}
