import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MarketingService {

  constructor(private http: HttpClient) { }

  sellPlate(registration: string) : Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    }
    return this.http.put('http://localhost:15683/marketing/sellplate', registration, httpOptions).pipe();
  }

  getPlates(pageNumber: number, pageSize: number) : Observable<any> {
    const headers = new HttpHeaders().append('Content-Type', 'application/json');
    const params = new HttpParams().append('pageNumber', pageNumber).append('pageSize', pageSize)
    return this.http.get('http://localhost:15683/marketing/getplates', { headers, params }).pipe();
  }

  getFilteredPlates(letters: string, pageNumber: number, pageSize: number) : Observable<any> {
    const headers = new HttpHeaders().append('Content-Type', 'application/json');
    const params = new HttpParams().append('letters', letters).append('pageNumber', pageNumber).append('pageSize', pageSize)
    return this.http.get('http://localhost:15683/marketing/getfilteredplates', { headers, params }).pipe();
  }

  plateCount(filter: string) {
    const headers = new HttpHeaders().append('Content-Type', 'application/json');
    const params = new HttpParams().append('filter', filter);
    return this.http.get('http://localhost:15683/commercial/platecount', { headers, params}).pipe();
  }

}
