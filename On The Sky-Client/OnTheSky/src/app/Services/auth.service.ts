import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, BehaviorSubject } from 'rxjs';

export interface LoginModel {
  userName: string;
  password: string;
}

export interface RegisterModel {
  userName: string;
  password: string;
}

export interface LoggedUser {
  userName: string;
  role: string;
  id?: number;
}

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private baseUrl = 'https://localhost:7169/api/Auth';

  private currentUserSubject = new BehaviorSubject<LoggedUser | null>(null);
  currentUser$ = this.currentUserSubject.asObservable();

  private isLoggedInSubject = new BehaviorSubject<boolean>(this.isLoggedIn());
  isLoggedIn$ = this.isLoggedInSubject.asObservable();

  constructor(private http: HttpClient, private router: Router) {}

  login(loginData: LoginModel): Observable<any> {
    return this.http.post<{ token: string }>(`${this.baseUrl}`, loginData);
  }

  // שינוי כאן: מוסיפים responseType: 'text' כדי לקבל תגובה טקסטואלית
  register(registerData: RegisterModel): Observable<string> {
    return this.http.post(`${this.baseUrl}/register`, registerData, { responseType: 'text' });
  }

  saveToken(token: string) {
    localStorage.setItem('jwt', token);
    this.isLoggedInSubject.next(true);
  }

  getToken(): string | null {
    return localStorage.getItem('jwt');
  }

  getCurrentUser(): Observable<LoggedUser> {
    const token = this.getToken();
    if (!token) throw new Error('No token found');

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });

    return this.http.get<LoggedUser>(`${this.baseUrl}/me`, { headers });
  }

  setCurrentUser(user: LoggedUser) {
    this.currentUserSubject.next(user);
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  logout() {
    localStorage.removeItem('jwt');
    this.currentUserSubject.next(null);
    this.isLoggedInSubject.next(false);
    this.router.navigate(['/login']);
  }
}
