import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BankAccountsRoutingModule } from './bank-accounts-routing.module';
import { MaterialModule } from '../../shared/material.module';
import { ReactiveFormsModule } from '@angular/forms';

import { AccountListComponent } from './pages/account-list/account-list.component';
import { AccountFormComponent } from './pages/account-form/account-form.component';
import { AccountCardComponent } from './components/account-card/account-card.component';



@NgModule({
  declarations: [
    AccountListComponent,
    AccountFormComponent,
    AccountCardComponent
  ],
  imports: [
    CommonModule,
    BankAccountsRoutingModule,
    MaterialModule,
    ReactiveFormsModule
  ]
})
export class BankAccountsModule { }
