export class RelatorioFilter {
    titulo?: string;
    codAutor?: number;

    constructor(titulo: string, codAutor: number) {
        this.titulo = titulo;
        this.codAutor = codAutor;
    }
}