import { JsonRequestResult } from './../models/JsonRequestResult';
import { Post } from './../models/Post';
import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import { catchError, map } from 'rxjs/operators'
import { of, throwError, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class VkReportServiceService {

constructor(private http: HttpClient) { }

public GetVKPosts(userId: string): Observable<Post[]> {
  const params = new HttpParams().append("id", userId);
  return this.FetchData<Post[]>('/api/GetVKWallPosts', params);
}

public FetchData<T>(url: string, params: HttpParams):Observable<T> {
  return this.http.get<JsonRequestResult>(url, { params: params }).pipe(
    catchError(this.handleError),
    map(x=> {
      if (x.StatusCode == 200) 
        return x.Payload as T;
      else {
        //alert("Error occured during request. Error message: " + x.payload);
        throw Error(x.Payload);
      }
    })
  );
}

handleError(error) {
  console.log(error);
  return throwError(error);
}

}
