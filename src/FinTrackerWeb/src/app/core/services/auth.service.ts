import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { LoginRequest, LoginResponse, RegisterRequest } from '../interfaces/auth.interface';
import { environment } from '../../../environments/environment';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private readonly AUTH_KEY = 'auth_token';
    private httpOptions = {
        headers: new HttpHeaders({
            'Content-Type': 'application/json',
            'Accept': 'application/json'
        })
    };
    private currentUserSubject = new BehaviorSubject<LoginResponse | null>(null);
    public currentUser$ = this.currentUserSubject.asObservable();

    constructor(private http: HttpClient) {
        this.loadStoredAuth();
    }

    register(request: RegisterRequest): Observable<LoginResponse> {
        return this.http.post<LoginResponse>(
            `${environment.apiUrl}/api/Auth/register`,
            request,
            this.httpOptions
        ).pipe(
            tap(response => this.handleAuthentication(response))
        );
    }

    login(request: LoginRequest): Observable<LoginResponse> {
        return this.http.post<LoginResponse>(
            `${environment.apiUrl}/api/Auth/login`,
            request,
            this.httpOptions
        ).pipe(
            tap(response => this.handleAuthentication(response))
        );
    }

    logout(): void {
        localStorage.removeItem(this.AUTH_KEY);
        this.currentUserSubject.next(null);
    }

    private handleAuthentication(response: LoginResponse): void {
        localStorage.setItem(this.AUTH_KEY, JSON.stringify(response));
        this.currentUserSubject.next(response);
    }

    private loadStoredAuth(): void {
        const storedAuth = localStorage.getItem(this.AUTH_KEY);
        if (storedAuth) {
            this.currentUserSubject.next(JSON.parse(storedAuth));
        }
    }

    getAuthToken(): string | null {
        const currentUser = this.currentUserSubject.value;
        return currentUser?.token ?? null;
    }
}