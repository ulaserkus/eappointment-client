import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ResultModel } from '../models/result.model';
import { Constants } from '../constants';
import { ErrorService } from './error.service';

@Injectable({
  providedIn: 'root',
})
export class HttpService {
  constructor(private http: HttpClient,private errorService:ErrorService) {}

  post<T>(
    apiUrl: string,
    body: any,
    callback: (res: ResultModel<T>) => void,
    errorCallback?: (err: HttpErrorResponse) => void
  ) {
    this.http
      .post<ResultModel<T>>(`${Constants.API_URL}/${apiUrl}`, body,{
        headers: {
          'Authorization': `Bearer ${localStorage.getItem('token')}`,
        },
      })
      .subscribe({
        next: (res) => {
          if (res.data !== undefined && res.data != null) callback(res);
        },
        error: (err: HttpErrorResponse) => {
          if (errorCallback) {
            errorCallback(err);
          }
          this.errorService.errorHandler(err);
        },
      });
  }
}
