import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'http://localhost:5173/api/Auth';


  constructor(private http: HttpClient) { }

  login(email: string, password: string): Observable<boolean> {
    return this.http.post<any>(`${this.apiUrl}/login`, { email, password })
      .pipe(
        map(response => {
          if (response && response.token) {
            localStorage.setItem('token', response.token);
            return true;
          }
          return false;
        }),
        catchError(error => {
          return throwError(() => new Error('Invalid credentials'));
        })
      );
  }

  register(name:string, lastname:string, email: string, password: string): Observable<boolean> {
    return this.http.post<any>(`${this.apiUrl}/add-user`, { name, lastname, email, password })
    .pipe(
      map(response => {
        return true;
      }),
      catchError(error => {
        return throwError(() => new Error('Error registering user'));
      })
    );
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  isLoggedIn(): boolean {
    return this.getToken() !== null;
  }

  logout(): void {
    localStorage.removeItem('token');
  }

  checkAuthStatus(): boolean {
    const token = this.getToken();
    // Here you can add additional logic to validate the token if needed
    return !!token;
  }

}