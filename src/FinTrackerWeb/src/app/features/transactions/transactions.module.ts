import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { TransactionsRoutingModule } from './transactions-routing.module';
import { MaterialModule } from '../../shared/material.module';

import { TransactionListComponent } from './pages/transaction-list/transaction-list.component';
import { TransactionFormComponent } from './pages/transaction-form/transaction-form.component';

@NgModule({
  declarations: [
    TransactionListComponent,
    TransactionFormComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    TransactionsRoutingModule,
    MaterialModule
  ]
})
export class TransactionsModule { }