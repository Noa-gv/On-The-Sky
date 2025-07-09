import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import Place from '../Models/place.Model';

@Injectable({ providedIn: 'root' })
export class PlaceService {
  private apiUrl = 'https://localhost:7169/api/Places';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Place[]> {
    return this.http.get<Place[]>(this.apiUrl);
  }

  getById(id: number): Observable<Place> {
    return this.http.get<Place>(`${this.apiUrl}/${id}`);
  }

  add(place: { country: string }): Observable<any> {
    return this.http.post(this.apiUrl, place);
  }

  update(id: number, place: { country: string }): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, place);
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}