import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { BankAccountService } from '../../services/bank-account.service';
import { AccountType } from '../../../../core/interfaces/bank-account.interface';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-account-form',
  templateUrl: './account-form.component.html',
  styleUrls: ['./account-form.component.css']
})
export class AccountFormComponent implements OnInit {
  accountForm: FormGroup;
  isEditMode = false;
  accountId: string | null = null;
  AccountType = AccountType; // Make enum available to template
  accountTypes = Object.keys(AccountType)
    .filter(key => !isNaN(Number(key)))
    .map(key => Number(key));

  constructor(
    private fb: FormBuilder,
    private bankAccountService: BankAccountService,
    private router: Router,
    private route: ActivatedRoute,
    private snackBar: MatSnackBar
  ) {
    this.accountForm = this.fb.group({
      accountName: ['', Validators.required],
      bankName: ['', Validators.required],
      accountType: [null, Validators.required],
      initialBalance: [0, [Validators.required, Validators.min(0)]],
      accountNumber: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.accountId = this.route.snapshot.paramMap.get('id');
    if (this.accountId) {
      this.isEditMode = true;
      this.loadAccount(this.accountId);
    }
  }

  loadAccount(id: string): void {
    this.bankAccountService.getAccount(id).subscribe({
      next: (account) => {
        this.accountForm.patchValue({
          accountName: account.accountName,
          bankName: account.bankName,
          accountType: account.accountType,
          initialBalance: account.currentBalance,
          accountNumber: account.accountNumber
        });
      },
      error: (error) => {
        this.snackBar.open('Error loading account', 'Close', { duration: 3000 });
        this.router.navigate(['/bank-accounts']);
      }
    });
  }

  onSubmit(): void {
    if (this.accountForm.valid) {
      if (this.isEditMode && this.accountId) {
        this.bankAccountService.updateAccount(this.accountId, {
          id: this.accountId,
          ...this.accountForm.value
        }).subscribe({
          next: () => {
            this.snackBar.open('Account updated successfully', 'Close', { duration: 3000 });
            this.router.navigate(['/bank-accounts']);
          },
          error: (error) => {
            this.snackBar.open('Error updating account', 'Close', { duration: 3000 });
          }
        });
      } else {
        this.bankAccountService.createAccount(this.accountForm.value).subscribe({
          next: () => {
            this.snackBar.open('Account created successfully', 'Close', { duration: 3000 });
            this.router.navigate(['/bank-accounts']);
          },
          error: (error) => {
            this.snackBar.open('Error creating account', 'Close', { duration: 3000 });
          }
        });
      }
    }
  }
}