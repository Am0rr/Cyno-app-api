import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { ApplicationConfig, provideBrowserGlobalErrorListeners } from '@angular/core';
import { breederIdInterceptor } from './core/interceptors/breeder-interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideHttpClient(withInterceptors([breederIdInterceptor])),
    provideBrowserGlobalErrorListeners(),
  ],
};
