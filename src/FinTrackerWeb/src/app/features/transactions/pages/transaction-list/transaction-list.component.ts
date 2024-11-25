import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TransactionService } from '../../services/transaction.service';
import { TransactionDto } from '../../../../core/interfaces/transaction.interface';
import { TransactionType } from '../../../../core/interfaces/category.interface';

@Component({
  selector: 'app-transaction-list',
  templateUrl: './transaction-list.component.html',
  styleUrls: ['./transaction-list.component.css']
})
export class TransactionListComponent implements OnInit {
  dataSource: MatTableDataSource<TransactionDto>;
  displayedColumns: string[] = ['date', 'description', 'category', 'amount', 'account', 'actions'];
  TransactionType = TransactionType;

  dateRange = new FormGroup({
    start: new FormControl<Date | null>(null),
    end: new FormControl<Date | null>(null),
  });

  typeFilter = new FormControl<TransactionType | null>(null);

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private transactionService: TransactionService,
    private router: Router,
    private snackBar: MatSnackBar
  ) {
    this.dataSource = new MatTableDataSource<TransactionDto>([]);
  }

  ngOnInit(): void {
    this.loadTransactions();
    this.setupFilters();
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  setupFilters(): void {
    this.dateRange.valueChanges.subscribe(() => this.applyFilters());
    this.typeFilter.valueChanges.subscribe(() => this.applyFilters());
  }

  applyFilters(): void {
    this.dataSource.filterPredicate = (data: TransactionDto) => {
      let matchesType = true;
      let matchesDate = true;

      if (this.typeFilter.value !== null) {
        matchesType = data.transactionType === this.typeFilter.value;
      }

      const start = this.dateRange.get('start')?.value;
      const end = this.dateRange.get('end')?.value;

      if (start && end) {
        const transactionDate = new Date(data.transactionDate);
        matchesDate = transactionDate >= start && transactionDate <= end;
      }

      return matchesType && matchesDate;
    };

    this.dataSource.filter = 'trigger';
  }

  loadTransactions(): void {
    this.transactionService.getTransactions().subscribe({
      next: (transactions) => {
        this.dataSource.data = transactions;
      },
      error: () => {
        this.snackBar.open('Error loading transactions', 'Close', { duration: 3000 });
      }
    });
  }

  onAdd(): void {
    this.router.navigate(['/transactions/new']);
  }

  onEdit(id: string): void {
    this.router.navigate(['/transactions/edit', id]);
  }

  onDelete(id: string): void {
    if (confirm('Are you sure you want to delete this transaction?')) {
      this.transactionService.deleteTransaction(id).subscribe({
        next: () => {
          this.snackBar.open('Transaction deleted successfully', 'Close', { duration: 3000 });
          this.loadTransactions();
        },
        error: () => {
          this.snackBar.open('Error deleting transaction', 'Close', { duration: 3000 });
        }
      });
    }
  }
}