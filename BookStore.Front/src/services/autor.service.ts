import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { Autor } from '../models/autor.model';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
})
export class AutorService {
    private apiUrl = environment.apiUrl + "Autor";

    constructor(private http: HttpClient) { }

    getAll(): Observable<Autor[]> {
        return this.http.get<Autor[]>(this.apiUrl);
    }

    create(author: Autor): Observable<Autor> {
        return this.http.post<Autor>(this.apiUrl, author);
    }

    update(author: Autor): Observable<Autor> {
        return this.http.put<Autor>(`${this.apiUrl}/${author.codAutor}`, author);
    }

    delete(id: number): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
            catchError((error: HttpErrorResponse) => {
                let errorMessage = 'Ocorreu um erro ao tentar excluir o autor.';

                if (error.status === 400) {
                    errorMessage = error.error || errorMessage;
                }

                return throwError(errorMessage);
            })
        );;
    }
}
