import { Component, Input, OnChanges } from '@angular/core';
import { Chart, ChartConfiguration } from 'chart.js';

@Component({
  selector: 'app-expense-chart',
  templateUrl: './expense-chart.component.html',
  styleUrl: './expense-chart.component.css'
})

export class ExpenseChartComponent implements OnChanges {
  @Input() expenseData: Record<string, number> = {};

  chartData: any = {
    labels: [],
    datasets: [{
      data: [],
      backgroundColor: [
        '#FF6384', '#36A2EB', '#FFCE56', '#4BC0C0', '#9966FF',
        '#FF9F40', '#4CAF50', '#9C27B0', '#607D8B', '#FF5722'
      ]
    }]
  };

  chartOptions: ChartConfiguration['options'] = {
    responsive: true,
    plugins: {
      legend: {
        position: 'right'
      }
    }
  };

  ngOnChanges(): void {
    if (this.expenseData) {
      this.chartData = {
        labels: Object.keys(this.expenseData),
        datasets: [{
          data: Object.values(this.expenseData),
          backgroundColor: this.chartData.datasets[0].backgroundColor
        }]
      };
    }
  }
}