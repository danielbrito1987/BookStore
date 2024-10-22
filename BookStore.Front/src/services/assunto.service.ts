import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Assunto } from '../models/assunto.model';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
})
export class AssuntoService {
    private apiUrl = environment.apiUrl + "Assunto";

    constructor(private http: HttpClient) { }

    getAll(): Observable<Assunto[]> {
        return this.http.get<Assunto[]>(this.apiUrl);
    }

    create(assunto: Assunto): Observable<Assunto> {
        return this.http.post<Assunto>(this.apiUrl, assunto);
    }

    update(assunto: Assunto): Observable<Assunto> {
        return this.http.put<Assunto>(`${this.apiUrl}/${assunto.codAssunto}`, assunto);
    }

    delete(id: number): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/${id}`);
    }
}
