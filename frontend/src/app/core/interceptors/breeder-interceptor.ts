import { HttpInterceptorFn } from '@angular/common/http';
import { BreederContextService } from '../services/breeder-context';
import { inject } from '@angular/core';

export const breederIdInterceptor: HttpInterceptorFn = (req, next) => {
  const breederContext = inject(BreederContextService);

  const cloned = req.clone({
    setHeaders: { 'X-Breeder-Id': breederContext.breederId },
  });

  return next(cloned);
};
