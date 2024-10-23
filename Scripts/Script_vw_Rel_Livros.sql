CREATE OR REPLACE VIEW vw_Rel_Livros AS
SELECT 
    l.Codl As CodLivro,
    l.Titulo,
    a.CodAu AS CodAutor,
    a.nome AS NomeAutor,
    GROUP_CONCAT(DISTINCT s.descricao ORDER BY s.descricao SEPARATOR ', ') AS Assuntos,
    l.Edicao,
    l.AnoPublicacao,
    pl.Valor
FROM Livro l
INNER JOIN Livro_Autor la ON l.Codl = la.Livro_Codl
INNER JOIN Autor a ON la.Autor_CodAu = a.CodAu
INNER JOIN Livro_Assunto las ON l.Codl = las.Livro_Codl
INNER JOIN Assunto s ON las.Assunto_CodAs = s.CodAs
LEFT JOIN Preco_Livro pl ON l.Codl = pl.Livro_Codl
GROUP BY a.CodAu, l.Codl;