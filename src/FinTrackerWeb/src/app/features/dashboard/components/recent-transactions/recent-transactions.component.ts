import { Component, Input } from '@angular/core';
import { TransactionDto } from '../../../../core/interfaces/transaction.interface';
import { TransactionType } from '../../../../core/interfaces/category.interface';

@Component({
  selector: 'app-recent-transactions',
  templateUrl: './recent-transactions.component.html',
  styleUrl: './recent-transactions.component.css'
})

export class RecentTransactionsComponent {
  @Input() transactions: TransactionDto[] = [];
  TransactionType = TransactionType;
}
