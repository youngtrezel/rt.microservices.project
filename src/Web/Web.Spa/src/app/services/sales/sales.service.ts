import { Injectable} from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SalesService {

  constructor(private http: HttpClient) { }

  getPlates(pageNumber: number, pageSize: number) : Observable<any> {
    const headers = new HttpHeaders().append('Content-Type', 'application/json');
    const params = new HttpParams().append('pageNumber', pageNumber).append('pageSize', pageSize)
    return this.http.get('http://localhost:15683/sales/getplates', { headers, params }).pipe();
  }

}
