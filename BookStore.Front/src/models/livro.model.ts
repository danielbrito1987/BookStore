export interface Livro {
    codLivro: number;
    titulo: string;
    editora: string;
    edicao: number;
    anoPublicacao: string;
    autoresIds: number[];
    assuntosIds: number[];
}