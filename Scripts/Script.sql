CREATE TABLE Livro
(
	Codl INT NOT NULL AUTO_INCREMENT,
	Titulo VARCHAR(40),
	Editora VARCHAR(40),
	Edicao INTEGER,
	AnoPublicacao VARCHAR(4),
	
	PRIMARY KEY (Codl)
)

CREATE TABLE Autor
(
	CodAu INT NOT NULL AUTO_INCREMENT,
	Nome VARCHAR(40),
	
	PRIMARY KEY (CodAu)
)

CREATE TABLE Assunto
(
	CodAs INT NOT NULL AUTO_INCREMENT,
	Descricao VARCHAR(20),
	
	PRIMARY KEY (CodAs)
)

CREATE TABLE Livro_Autor
(
	Livro_Codl INT NOT NULL,
	Autor_CodAu INT NOT NULL,
	
	FOREIGN KEY (Livro_Codl) REFERENCES Livro(Codl),
	FOREIGN KEY (Autor_CodAu) REFERENCES Autor(CodAu)
)

CREATE TABLE Livro_Assunto
(
	Livro_Codl INT NOT NULL,
	Assunto_CodAs INT NOT NULL,
	
	FOREIGN KEY (Livro_Codl) REFERENCES Livro(Codl),
	FOREIGN KEY (Assunto_CodAs) REFERENCES Assunto(CodAs)
)



