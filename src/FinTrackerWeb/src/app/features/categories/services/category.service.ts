// src/app/features/categories/services/category.service.ts (Replace entire file)
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { 
    TransactionCategoryDto, 
    CreateTransactionCategoryDto, 
    UpdateTransactionCategoryDto,
    TransactionType 
} from '../../../core/interfaces/category.interface';

@Injectable({
    providedIn: 'root'
})
export class CategoryService {
    private apiUrl = `${environment.apiUrl}/api/TransactionCategory`;

    constructor(private http: HttpClient) {}

    getCategories(): Observable<TransactionCategoryDto[]> {
        return this.http.get<TransactionCategoryDto[]>(this.apiUrl);
    }

    getCategoriesByType(type: TransactionType): Observable<TransactionCategoryDto[]> {
        return this.http.get<TransactionCategoryDto[]>(`${this.apiUrl}/type/${type}`);
      }

    getCategory(id: string): Observable<TransactionCategoryDto> {
        return this.http.get<TransactionCategoryDto>(`${this.apiUrl}/${id}`);
    }

    createCategory(category: CreateTransactionCategoryDto): Observable<TransactionCategoryDto> {
        return this.http.post<TransactionCategoryDto>(this.apiUrl, category);
    }

    updateCategory(id: string, category: UpdateTransactionCategoryDto): Observable<void> {
        return this.http.put<void>(`${this.apiUrl}/${id}`, category);
    }

    deleteCategory(id: string): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/${id}`);
    }
}