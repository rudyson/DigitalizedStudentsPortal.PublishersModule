import {
  HttpErrorResponse,
  HttpInterceptorFn,
  HttpStatusCode,
} from '@angular/common/http';
import { inject } from '@angular/core';
import { TranslocoService } from '@jsverse/transloco';
import { Message, MessageService } from 'primeng/api';
import { catchError, of } from 'rxjs';
import { ResponseWrapper } from '../services/api/common.models';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const messageService = inject(MessageService);
  const translocoService = inject(TranslocoService);
  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      if (error) {
        const message = messageFromHttpErrorResponse(error, translocoService);
        messageService.add(message);
        return of();
      }
      console.log(error);
      throw error;
    })
  );
};

function messageFromHttpErrorResponse(
  error: HttpErrorResponse,
  translocoService: TranslocoService
): Message {
  switch (error.status) {
    case 0:
      return {
        severity: 'error',
        summary: translocoService.translate('messages.errors.network'),
      };
    case HttpStatusCode.UnprocessableEntity:
    case HttpStatusCode.BadRequest: {
      const validationErrors = (error.error as ResponseWrapper<object>)?.errors;

      return {
        icon: 'pi-minus-circle',
        severity: 'warn',
        summary: translocoService.translate('messages.errors.validation'),
        detail: validationErrors
          ? Object.values(validationErrors).join('\n')
          : undefined,
      };
    }
    default:
      return {
        severity: 'error',
        summary: translocoService.translate('messages.errors.unhandled'),
      };
  }
}
