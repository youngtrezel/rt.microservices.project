import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Plate } from '../../../models/plate';

@Injectable({
  providedIn: 'root'
})
export class CommercialService {

  constructor(private http: HttpClient) { }


  getPlate(Registration: string) : Observable<any> {
    const headers = new HttpHeaders().append('Content-Type', 'application/json');
    const params = new HttpParams().append('Registration', Registration);
    return this.http.get('http://localhost:15683/commercial/getplate', { headers, params }).pipe();
  }

  getPlates(pageNumber: number, pageSize: number) : Observable<any> {
    const headers = new HttpHeaders().append('Content-Type', 'application/json');
    const params = new HttpParams().append('pageNumber', pageNumber).append('pageSize', pageSize)
    return this.http.get('http://localhost:15683/commercial/getplates', { headers, params }).pipe();
  }

  getUnreservedPlates(pageNumber: number, pageSize: number) : Observable<any> {
    const headers = new HttpHeaders().append('Content-Type', 'application/json');
    const params = new HttpParams().append('pageNumber', pageNumber).append('pageSize', pageSize)
    return this.http.get('http://localhost:15683/commercial/getunreservedplates', { headers, params }).pipe();
  }

  getUnsoldPlates(pageNumber: number, pageSize: number) : Observable<any> {
    const headers = new HttpHeaders().append('Content-Type', 'application/json');
    const params = new HttpParams().append('pageNumber', pageNumber).append('pageSize', pageSize)
    return this.http.get('http://localhost:15683/commercial/getunsoldplates', { headers, params }).pipe();
  }

  addPlate(plate:Plate) : Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    }
    return this.http.post('http://localhost:15683/commercial/addplate', plate, httpOptions).pipe();
  }

  reservePlate(Registration: string) : Observable<any> {
    const headers = new HttpHeaders().append('Content-Type', 'application/json');
    const params = new HttpParams().append('Registration', Registration)
    return this.http.get('http://localhost:15683/commercial/reserveplate', { headers, params}).pipe();
  }

  unreservePlate(Registration: string) : Observable<any> {
    const headers = new HttpHeaders().append('Content-Type', 'application/json');
    const params = new HttpParams().append('Registration', Registration)
    return this.http.get('http://localhost:15683/commercial/unreserveplate', { headers, params }).pipe();
  }
  
  plateCount(filter: string) {
    const headers = new HttpHeaders().append('Content-Type', 'application/json');
    const params = new HttpParams().append('filter', filter);
    return this.http.get('http://localhost:15683/commercial/platecount', { headers, params}).pipe();
  }

  getFilteredUnsoldPlates(letters: string, pageNumber: number, pageSize: number ) : Observable<any> {
    const headers = new HttpHeaders().append('Content-Type', 'application/json');
    const params = new HttpParams().append('letters', letters).append('pageNumber', pageNumber).append('pageSize', pageSize)
    return this.http.get('http://localhost:15683/commercial/filteredunsold', { headers, params }).pipe();
  }


}


