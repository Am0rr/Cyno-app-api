import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GetLittersRequest, PagedLitterResponse, PublishResponse } from '../models/litter.model';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class LitterService {
  private readonly apiUrl = `${environment.apiUrl}/litters`;

  constructor(private http: HttpClient) {}

  publish(litterId: string): Observable<PublishResponse> {
    return this.http.post<PublishResponse>(`${this.apiUrl}/${litterId}/publish`, {});
  }

  getLitters(request: GetLittersRequest): Observable<PagedLitterResponse> {
    let params = new HttpParams()
      .set('pageNumber', request.pageNumber)
      .set('pageSize', request.pageSize);

    if (request.status) {
      params = params.set('status', request.status);
    }

    return this.http.get<PagedLitterResponse>(this.apiUrl, { params });
  }
}
