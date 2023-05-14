CREATE TABLE Usuarios(
	email_login varchar(100) NOT NULL,
	senha varchar(max) NOT NULL,
	telefone varchar(12) NOT NULL,
	nome varchar(100) NOT NULL,
	cargo integer NOT NULL,
	email_confirmado bit DEFAULT 0,
	CONSTRAINT Pessoa_PK PRIMARY KEY(email_login),
);
CREATE TABLE Enderecos(
	login_usuario varchar(100) NOT NULL,
	logradouro varchar(12) NOT NULL,
	numero integer NOT NULL,
	complemento varchar(50) DEFAULT 'Rua Sem Nome',
	cep varchar(9),
	cidade varchar(30) NOT NULL,
	uf varchar(2) NOT NULL,
	CONSTRAINT Cliente_FK FOREIGN KEY(login_usuario) REFERENCES Usuarios(email_login)
);
INSERT INTO Usuarios(email_login, senha, telefone, nome, cargo) VALUES ('teste@emailteste.com', 'abcd123', '61 912345678', 'nome Teste', 1);
INSERT INTO Enderecos(login_usuario, logradouro, numero, complemento, cep, cidade, uf) VALUES ('teste@emailteste.com', 'quadra 2', 4, 'Rua dos Pinheiros', '12345-67', 'Cidade dos Deuses', 'OL');

INSERT INTO Usuarios(email_login, senha, telefone, nome, cargo) VALUES ('teste2@emailteste.com', 'abcd123', '61 912345678', 'nome TESTE2', 1);
INSERT INTO Enderecos(login_usuario, logradouro, numero, complemento, cep, cidade, uf) VALUES ('teste2@emailteste.com', 'quadra 2', 4, 'Rua dos Pinheiros', '12345-67', 'Cidade dos Deuses', 'OL');

INSERT INTO Usuarios(email_login, senha, telefone, nome, cargo) VALUES ('teste3@emailteste.com', 'abcd123', '61 912345678', 'nome Teste3', 1);
INSERT INTO Enderecos(login_usuario, logradouro, numero, complemento, cep, cidade, uf) VALUES ('teste3@emailteste.com', 'quadra 2', 4, 'Rua dos Pinheiros', '12345-67', 'Cidade dos Deuses', 'OL');

SELECT * FROM Usuarios, Enderecos WHERE Usuarios.email_login LIKE Enderecos.login_usuario;
SELECT * FROM Usuarios, Enderecos WHERE Usuarios.email_login LIKE Enderecos.login_usuario AND Usuarios.email_login LIKE 'aderlane@email.com';
SELECT * FROM Usuarios;
SELECT * FROM Enderecos;
DELETE Enderecos;
DELETE Usuarios;

