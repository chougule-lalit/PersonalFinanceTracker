import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-summary-cards',
  templateUrl: './summary-cards.component.html',
  styleUrl: './summary-cards.component.css'
})

export class SummaryCardsComponent {
  @Input() totalBalance: number = 0;
  @Input() monthlyIncome: number = 0;
  @Input() monthlyExpense: number = 0;
}
