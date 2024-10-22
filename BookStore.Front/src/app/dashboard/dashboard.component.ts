import { Component, OnInit } from '@angular/core';
import { Dashboard } from 'src/models/dashboard.model';
import { DashboardService } from 'src/services/dashboard.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  public dashboard: Dashboard;

  constructor(
    private dashboardService: DashboardService
  ) {
    this.dashboard = new Dashboard(0, 0, 0);
  }

  ngOnInit(): void {
    this.getDashboard();
  }

  getDashboard() {
    this.dashboardService.getDashboard().subscribe((data) => {
      this.dashboard = data;
    })
  }
}
