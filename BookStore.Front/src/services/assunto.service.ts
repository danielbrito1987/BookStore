import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
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
        return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
            catchError((error: HttpErrorResponse) => {
                let errorMessage = 'Ocorreu um erro ao tentar excluir o assunto.';

                if (error.status === 400) {
                    errorMessage = error.error || errorMessage;
                }

                return throwError(errorMessage);
            })
        );;
    }
}
