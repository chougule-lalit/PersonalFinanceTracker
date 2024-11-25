import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { MaterialModule } from '../../shared/material.module';
import { provideCharts, withDefaultRegisterables } from 'ng2-charts';

import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { SummaryCardsComponent } from './components/summary-cards/summary-cards.component';
import { ExpenseChartComponent } from './components/expense-chart/expense-chart.component';
import { AccountBalanceComponent } from './components/account-balance/account-balance.component';
import { RecentTransactionsComponent } from './components/recent-transactions/recent-transactions.component';

@NgModule({
  providers: [provideCharts(withDefaultRegisterables())],
  declarations: [
    DashboardComponent,
    SummaryCardsComponent,
    ExpenseChartComponent,
    AccountBalanceComponent,
    RecentTransactionsComponent
  ],
  imports: [
    CommonModule,
    DashboardRoutingModule,
    MaterialModule
  ]
})
export class DashboardModule { }