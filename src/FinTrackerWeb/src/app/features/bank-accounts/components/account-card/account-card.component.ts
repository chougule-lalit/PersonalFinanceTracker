// src/app/features/bank-accounts/components/account-card/account-card.component.ts
import { Component, Input, Output, EventEmitter } from '@angular/core';
import { BankAccountDto, AccountType  } from '../../../../core/interfaces/bank-account.interface';

@Component({
  selector: 'app-account-card',
  templateUrl: './account-card.component.html',
  styleUrls: ['./account-card.component.css']
})
export class AccountCardComponent {
  @Input() account!: BankAccountDto;
  @Output() edit = new EventEmitter<string>();
  @Output() delete = new EventEmitter<string>();

  AccountType = AccountType;

  onEdit(): void {
    this.edit.emit(this.account.id);
  }

  onDelete(): void {
    this.delete.emit(this.account.id);
  }
}