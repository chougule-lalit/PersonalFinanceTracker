import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { TransactionDto, CreateTransactionDto, UpdateTransactionDto } from '../../../core/interfaces/transaction.interface';

@Injectable({
    providedIn: 'root'
})
export class TransactionService {
    private apiUrl = `${environment.apiUrl}/api/Transaction`;

    constructor(private http: HttpClient) {}

    getTransactions(): Observable<TransactionDto[]> {
        return this.http.get<TransactionDto[]>(this.apiUrl);
    }

    getTransaction(id: string): Observable<TransactionDto> {
        return this.http.get<TransactionDto>(`${this.apiUrl}/${id}`);
    }

    createTransaction(transaction: CreateTransactionDto): Observable<TransactionDto> {
        return this.http.post<TransactionDto>(this.apiUrl, transaction);
    }

    updateTransaction(id: string, transaction: UpdateTransactionDto): Observable<void> {
        return this.http.put<void>(`${this.apiUrl}/${id}`, transaction);
    }

    deleteTransaction(id: string): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/${id}`);
    }
}