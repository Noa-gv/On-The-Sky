import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import Flight from '../Models/flight.Model';

@Injectable({
  providedIn: 'root'
})
export class FlightService {
  private apiUrl = 'https://localhost:7169/api/flight'; 

  constructor(private http: HttpClient) {}

  // קבלת כל הטיסות
  getFlights(): Observable<Flight[]> {
    return this.http.get<Flight[]>(this.apiUrl);
  }

  // קבלת טיסה לפי ID
  getFlightById(id: number): Observable<Flight> {
    return this.http.get<Flight>(`${this.apiUrl}/${id}`);
  }

  // הוספת טיסה חדשה
  add(flight: Partial<Flight>): Observable<any> {
    return this.http.post(this.apiUrl, flight);
  }

  // עדכון טיסה קיימת
  update(id: number, flight: Partial<Flight>): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, flight);
  }

  // מחיקת טיסה
  delete(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
