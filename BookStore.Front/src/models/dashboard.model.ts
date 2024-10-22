export class Dashboard {
    qtdLivros?: number;
    qtdAssuntos?: number;
    qtdAutores?: number;

    constructor(qtdLivros: number, qtdAssuntos: number, qtdAutores: number) {
        this.qtdLivros = qtdLivros;
        this.qtdAssuntos = qtdAssuntos;
        this.qtdAutores = qtdAutores;
    }
}