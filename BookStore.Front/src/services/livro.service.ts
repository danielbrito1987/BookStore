import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { Assunto } from '../models/assunto.model';
import { environment } from 'src/environments/environment';
import { Livro } from 'src/models/livro.model';

@Injectable({
    providedIn: 'root'
})
export class LivroService {
    private apiUrl = environment.apiUrl + "Livro";

    constructor(private http: HttpClient) { }

    getAll(): Observable<Livro[]> {
        return this.http.get<Livro[]>(this.apiUrl);
    }

    create(livro: Livro): Observable<Livro> {
        return this.http.post<Livro>(this.apiUrl, livro);
    }

    update(livro: Livro): Observable<Livro> {
        return this.http.put<Livro>(`${this.apiUrl}/${livro.codLivro}`, livro);
    }

    delete(id: number): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
            catchError((error: HttpErrorResponse) => {
                let errorMessage = 'Ocorreu um erro ao tentar excluir o livro.';

                if(error.status === 400) {
                    errorMessage = error.error || errorMessage;
                }

                return throwError(errorMessage);
            })
        );
    }
}
