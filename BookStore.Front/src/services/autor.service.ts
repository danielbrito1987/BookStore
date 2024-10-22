import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Autor } from '../models/autor.model';

@Injectable({
  providedIn: 'root'
})
export class AutorService {
  private apiUrl = 'http://localhost:3000/autores'; // Altere para o URL da sua API

  constructor(private http: HttpClient) { }

  getAll(): Observable<Autor[]> {
    return this.http.get<Autor[]>(this.apiUrl);
  }

  create(author: Autor): Observable<Autor> {
    return this.http.post<Autor>(this.apiUrl, author);
  }

  update(author: Autor): Observable<Autor> {
    return this.http.put<Autor>(`${this.apiUrl}/${author.id}`, author);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
