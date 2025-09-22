import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

  login(username: string, password: string): Observable<boolean> {
    if (username === 'admin' && password === 'admin') {
      return of(true);
    } else {
      return throwError(() => new Error('Invalid credentials'));
    }
  }
}
