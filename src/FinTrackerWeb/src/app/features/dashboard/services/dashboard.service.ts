import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, forkJoin, map } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { BankAccountDto } from '../../../core/interfaces/bank-account.interface';
import { TransactionDto } from '../../../core/interfaces/transaction.interface';
import { TransactionType } from '../../../core/interfaces/category.interface';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getDashboardData() {
    return forkJoin({
      accounts: this.http.get<BankAccountDto[]>(`${this.apiUrl}/api/BankAccount`),
      transactions: this.http.get<TransactionDto[]>(`${this.apiUrl}/api/Transaction`)
    }).pipe(
      map(data => {
        const totalBalance = data.accounts.reduce((sum, acc) => sum + acc.currentBalance, 0);
        const monthlyTransactions = data.transactions.filter(t => {
          const transDate = new Date(t.transactionDate);
          const now = new Date();
          return transDate.getMonth() === now.getMonth() &&
            transDate.getFullYear() === now.getFullYear();
        });

        const monthlyIncome = monthlyTransactions
          .filter(t => t.transactionType === TransactionType.Income)
          .reduce((sum, t) => sum + t.amount, 0);

        const monthlyExpense = monthlyTransactions
          .filter(t => t.transactionType === TransactionType.Expense)
          .reduce((sum, t) => sum + t.amount, 0);

        const expenseByCategory = monthlyTransactions
          .filter(t => t.transactionType === TransactionType.Expense)
          .reduce((acc, t) => {
            acc[t.categoryName] = (acc[t.categoryName] || 0) + t.amount;
            return acc;
          }, {} as Record<string, number>);

        return {
          totalBalance,
          monthlyIncome,
          monthlyExpense,
          expenseByCategory,
          recentTransactions: monthlyTransactions.slice(0, 5),
          accounts: data.accounts
        };
      })
    );
  }
}