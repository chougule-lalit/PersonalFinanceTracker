import { Component, Input } from '@angular/core';
import { BankAccountDto } from '../../../../core/interfaces/bank-account.interface';

@Component({
  selector: 'app-account-balance',
  templateUrl: './account-balance.component.html',
  styleUrl: './account-balance.component.css'
})
export class AccountBalanceComponent {
  @Input() accounts: BankAccountDto[] = [];
}
