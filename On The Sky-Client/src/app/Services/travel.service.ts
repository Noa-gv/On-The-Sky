import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import Travel from '../Models/travel.Model';

@Injectable({
  providedIn: 'root'
})
export class TravelService {

  private apiUrl = 'https://localhost:7169/api/Travels';

  constructor(private http: HttpClient) { }

  getAll(): Observable<Travel[]> {
    return this.http.get<Travel[]>(this.apiUrl);
  }

  getById(id: number): Observable<Travel> {
    return this.http.get<Travel>(`${this.apiUrl}/${id}`);
  }

  getMyTravels(): Observable<Travel[]> {
    return this.http.get<Travel[]>(`${this.apiUrl}/my`);
  }

  add(travel: Travel): Observable<any> {
    return this.http.post(this.apiUrl, travel);
  }

  update(id: number, travel: Travel): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, {
      flightId: travel.flightId,
      amountTickets: travel.amountTickets
    });
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
