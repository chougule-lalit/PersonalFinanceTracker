import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { BankAccountService } from '../../services/bank-account.service';
import { BankAccountDto } from '../../../../core/interfaces/bank-account.interface';

@Component({
  selector: 'app-account-list',
  templateUrl: './account-list.component.html',
  styleUrls: ['./account-list.component.css']
})
export class AccountListComponent implements OnInit {
  accounts$: Observable<BankAccountDto[]>;

  constructor(
    private bankAccountService: BankAccountService,
    private router: Router
  ) {
    this.accounts$ = this.bankAccountService.getAccounts();
  }

  ngOnInit(): void { }

  onAddAccount(): void {
    this.router.navigate(['/bank-accounts/new']);
  }

  onEditAccount(id: string): void {
    this.router.navigate(['/bank-accounts/edit', id]);
  }

  onDeleteAccount(id: string): void {
    if (confirm('Are you sure you want to delete this account?')) {
      this.bankAccountService.deleteAccount(id).subscribe(() => {
        this.accounts$ = this.bankAccountService.getAccounts();
      });
    }
  }
}