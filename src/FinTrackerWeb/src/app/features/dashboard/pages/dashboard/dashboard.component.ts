import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { DashboardService } from '../../services/dashboard.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})

export class DashboardComponent implements OnInit {
  dashboardData$: Observable<any>;

  constructor(private dashboardService: DashboardService) {
    this.dashboardData$ = this.dashboardService.getDashboardData();
  }

  ngOnInit(): void { }
}