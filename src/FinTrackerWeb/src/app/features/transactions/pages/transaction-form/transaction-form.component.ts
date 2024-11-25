import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BehaviorSubject, forkJoin } from 'rxjs';
import { TransactionService } from '../../services/transaction.service';
import { CategoryService } from '../../../categories/services/category.service';
import { BankAccountService } from '../../../bank-accounts/services/bank-account.service';
import { TransactionCategoryDto, TransactionType } from '../../../../core/interfaces/category.interface';

@Component({
  selector: 'app-transaction-form',
  templateUrl: './transaction-form.component.html',
  styleUrls: ['./transaction-form.component.css']
})
export class TransactionFormComponent implements OnInit {
  transactionForm: FormGroup;
  isEditMode = false;
  transactionId: string | null = null;
  categories: any[] = [];
  categories$ = new BehaviorSubject<TransactionCategoryDto[]>([]);
  accounts: any[] = [];
  TransactionType = TransactionType;

  constructor(
    private fb: FormBuilder,
    private transactionService: TransactionService,
    private categoryService: CategoryService,
    private accountService: BankAccountService,
    private router: Router,
    private route: ActivatedRoute,
    private snackBar: MatSnackBar
  ) {
    this.transactionForm = this.fb.group({
      transactionDate: [new Date(), Validators.required],
      amount: [0, [Validators.required, Validators.min(0.01)]],
      description: ['', Validators.required],
      transactionType: [TransactionType.Expense, Validators.required],
      categoryId: ['', Validators.required],
      bankAccountId: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.onTypeChange(TransactionType.Expense);
    this.loadFormData();
    this.transactionId = this.route.snapshot.paramMap.get('id');
    if (this.transactionId) {
      this.isEditMode = true;
      this.loadTransaction(this.transactionId);
    }
  }

  loadFormData(): void {
    forkJoin({
      categories: this.categoryService.getCategoriesByType(this.transactionForm.get('transactionType')?.value || TransactionType.Expense),
      accounts: this.accountService.getAccounts()
    }).subscribe({
      next: (data) => {
        this.categories = data.categories;
        this.accounts = data.accounts;
      },
      error: () => {
        this.snackBar.open('Error loading form data', 'Close', { duration: 3000 });
      }
    });
  }

  loadTransaction(id: string): void {
    this.transactionService.getTransaction(id).subscribe({
      next: (transaction) => {
        this.transactionForm.patchValue({
          transactionDate: new Date(transaction.transactionDate),
          amount: transaction.amount,
          description: transaction.description,
          transactionType: transaction.transactionType,
          categoryId: transaction.categoryName,
          bankAccountId: transaction.accountName
        });
      },
      error: () => {
        this.snackBar.open('Error loading transaction', 'Close', { duration: 3000 });
        this.router.navigate(['/transactions']);
      }
    });
  }

  onSubmit(): void {
    if (this.transactionForm.valid) {
      if (this.isEditMode && this.transactionId) {
        this.transactionService.updateTransaction(this.transactionId, {
          id: this.transactionId,
          ...this.transactionForm.value
        }).subscribe({
          next: () => {
            this.snackBar.open('Transaction updated successfully', 'Close', { duration: 3000 });
            this.router.navigate(['/transactions']);
          },
          error: () => {
            this.snackBar.open('Error updating transaction', 'Close', { duration: 3000 });
          }
        });
      } else {
        this.transactionService.createTransaction(this.transactionForm.value).subscribe({
          next: () => {
            this.snackBar.open('Transaction created successfully', 'Close', {
              duration: 3000,
              horizontalPosition: 'end',
              verticalPosition: 'top',
              panelClass: ['success-snackbar']
            });
            this.router.navigate(['/transactions']);
          },
          error: () => {
            this.snackBar.open('Error creating transaction', 'Close', { duration: 3000 });
          }
        });
      }
    }
  }  

  onTypeChange(type: TransactionType): void {
    this.categoryService.getCategoriesByType(type).subscribe({
      next: (categories) => {
        this.categories = categories;
        // Clear the current category selection since categories changed
        this.transactionForm.patchValue({
          categoryId: ''
        });
      },
      error: () => {
        this.snackBar.open('Error loading categories', 'Close', { duration: 3000 });
      }
    });
  }
}