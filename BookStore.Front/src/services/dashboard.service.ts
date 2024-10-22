import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Autor } from '../models/autor.model';
import { Dashboard } from 'src/models/dashboard.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  private apiUrl = environment.apiUrl + "Dashboard"; // Altere para o URL da sua API

  constructor(private http: HttpClient) { }

  getDashboard(): Observable<Dashboard> {
    var url = "/GetDashboard";
    return this.http.get<Dashboard>(this.apiUrl + url);
  }
}
