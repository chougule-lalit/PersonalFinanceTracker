// src/app/features/bank-accounts/services/bank-account.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { BankAccountDto, CreateBankAccountDto, UpdateBankAccountDto } from '../../../core/interfaces/bank-account.interface';

@Injectable({
    providedIn: 'root'
})
export class BankAccountService {
    private apiUrl = `${environment.apiUrl}/api/BankAccount`;

    constructor(private http: HttpClient) { }

    getAccounts(): Observable<BankAccountDto[]> {
        return this.http.get<BankAccountDto[]>(this.apiUrl);
    }

    getAccount(id: string): Observable<BankAccountDto> {
        return this.http.get<BankAccountDto>(`${this.apiUrl}/${id}`);
    }

    createAccount(account: CreateBankAccountDto): Observable<BankAccountDto> {
        return this.http.post<BankAccountDto>(this.apiUrl, account);
    }

    updateAccount(id: string, account: UpdateBankAccountDto): Observable<void> {
        return this.http.put<void>(`${this.apiUrl}/${id}`, account);
    }

    deleteAccount(id: string): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/${id}`);
    }
}