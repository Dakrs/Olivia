-- -----------------------------------------------------
-- Schema Olivia
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema Olivia
-- -----------------------------------------------------
--DROP DATABASE [Olivia];
--GO

CREATE DATABASE [Olivia];
GO

USE [Olivia];
GO

-- -----------------------------------------------------
-- Table `Olivia`.`Utilizador`
-- -----------------------------------------------------
CREATE TABLE [Utilizador] (
  [id_utilizador] INT IDENTITY(1,1) NOT NULL,
  [username] VARCHAR(100) NOT NULL,
  [password] VARCHAR(45) NOT NULL,
  [email] VARCHAR(45) NOT NULL,
  [type] SMALLINT NOT NULL,
  [preferencia] INT NOT NULL,
  [nome] VARCHAR(100) NOT NULL,
  PRIMARY KEY ([id_utilizador]))
;
GO


-- -----------------------------------------------------
-- Table `Olivia`.`Receita`
-- -----------------------------------------------------
CREATE TABLE [Receita] (
  [id_receita] INT IDENTITY(1,1) NOT NULL,
  [nome] VARCHAR(45) NOT NULL,
  [descricao] VARCHAR(100) NOT NULL,
  [autor] INT NULL,
  [tipo] INT NOT NULL,
  [calorias] FLOAT NOT NULL,
  [gordura] FLOAT NOT NULL,
  [carbohidratos] FLOAT NOT NULL,
  [proteina] FLOAT NOT NULL,
  [fibra] FLOAT NOT NULL,
  [sodio] FLOAT NOT NULL,
  PRIMARY KEY ([id_receita]),
  CONSTRAINT [fk_id_autor]
    FOREIGN KEY ([autor])
    REFERENCES [Utilizador] ([id_utilizador])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);
GO

CREATE INDEX [fk_autor_receita_idx] ON [Receita] ([autor] ASC);
GO

-- -----------------------------------------------------
-- Table `Olivia`.`Historico`
-- -----------------------------------------------------
CREATE TABLE [Historico] (
  [id_utilizador] INT NOT NULL,
  [id_receita] INT NOT NULL,
  [data] DATETIME NOT NULL,
  PRIMARY KEY ([id_receita], [id_utilizador]),
  CONSTRAINT [fk_id_utilizador]
    FOREIGN KEY ([id_utilizador])
    REFERENCES [Utilizador] ([id_utilizador])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT [fk_id_receita]
    FOREIGN KEY ([id_receita])
    REFERENCES [Receita] ([id_receita])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

GO

CREATE INDEX [fk_id_utilizador_idx] ON [Historico] ([id_utilizador]);
GO

CREATE INDEX [fk_id_receita_idx] ON [Historico] ([id_receita]);
GO

-- -----------------------------------------------------
-- Table `Olivia`.`Avaliacao`
-- -----------------------------------------------------
CREATE TABLE [Avaliacao] (
  [id_utilizador] INT NOT NULL,
  [id_receita] INT NOT NULL,
  [avaliacao] INT NOT NULL,
  PRIMARY KEY ([id_receita], [id_utilizador]),
  CONSTRAINT [fk_id_utilizador_avaliacao]
    FOREIGN KEY ([id_utilizador])
    REFERENCES [Utilizador] ([id_utilizador])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT [fk_id_receita_avaliacao]
    FOREIGN KEY ([id_receita])
    REFERENCES [Receita] ([id_receita])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
;
GO

CREATE INDEX [fk_id_utilizador_avaliacao_idx] ON [Avaliacao] ([id_utilizador] ASC);
GO

CREATE INDEX [fk_id_receita_avaliacao_idx] ON [Avaliacao] ([id_receita] ASC);
GO


-- -----------------------------------------------------
-- Table `Olivia`.`Ementa`
-- -----------------------------------------------------
CREATE TABLE [Ementa] (
  [id_ementa] INT IDENTITY(1,1) NOT NULL,
  [id_user] INT NOT NULL,
  [data] DATE NULL,
  PRIMARY KEY ([id_ementa]),
  CONSTRAINT [fk_utilizador_ementa]
    FOREIGN KEY ([id_user])
    REFERENCES [Utilizador] ([id_utilizador])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);
GO

CREATE INDEX [fk_utilizador_ementa_idx] ON [Ementa] ([id_user] ASC);
GO

-- -----------------------------------------------------
-- Table `Olivia`.`Ementa_Receita`
-- -----------------------------------------------------
CREATE TABLE [Ementa_Receita] (
  [id_receita] INT NOT NULL,
  [id_ementa] INT NOT NULL,
  PRIMARY KEY ([id_receita], [id_ementa]),
  CONSTRAINT [fk_Ementa_Receita_Receita]
    FOREIGN KEY ([id_receita])
    REFERENCES [Receita] ([id_receita])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT [fk_Ementa_Receita_Ementa]
    FOREIGN KEY ([id_ementa])
    REFERENCES [Ementa] ([id_ementa])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

GO

CREATE INDEX [fk_Ementa_Receita_Receita_idx] ON [Ementa_Receita] ([id_ementa] ASC);
GO

-- -----------------------------------------------------
-- Table `Olivia`.`Ingrediente`
-- -----------------------------------------------------
CREATE TABLE [Ingrediente] (
  [id_ingrediente] INT IDENTITY(1,1) NOT NULL,
  [nome] VARCHAR(45) NOT NULL,
  [categoria] VARCHAR(45) NULL,
  PRIMARY KEY ([id_ingrediente]))
;

GO


-- -----------------------------------------------------
-- Table `Olivia`.`Receita_Ingrediente`
-- -----------------------------------------------------
CREATE TABLE [Receita_Ingrediente] (
  [id_receita] INT NOT NULL,
  [id_ingrediente] INT NOT NULL,
  [quantidade] FLOAT NOT NULL,
  [unidade] VARCHAR(45) NOT NULL,
  PRIMARY KEY ([id_receita], [id_ingrediente]),
  CONSTRAINT [fk_receita_ingrediente_receita]
    FOREIGN KEY ([id_receita])
    REFERENCES [Receita] ([id_receita])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT [fk_receita_ingrediente_ingrediente]
    FOREIGN KEY ([id_ingrediente])
    REFERENCES [Ingrediente] ([id_ingrediente])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

GO


CREATE INDEX [fk_receita_ingrediente_receita_idx] ON [Receita_Ingrediente] ([id_receita] ASC);
GO

CREATE INDEX [fk_receita_ingrediente_ingrediente_idx] ON [Receita_Ingrediente] ([id_ingrediente] ASC);
GO


-- -----------------------------------------------------
-- Table `Olivia`.`Instrucao`
-- -----------------------------------------------------
CREATE TABLE [Instrucao] (
  [designacao] VARCHAR(100) NOT NULL,
  [duracao] INT NOT NULL,
  [posicao] INT NOT NULL,
  [id_receita] INT NOT NULL,
  PRIMARY KEY ([id_receita], [posicao]),
  CONSTRAINT [fk_instrucao_receita]
    FOREIGN KEY ([id_receita])
    REFERENCES [Receita] ([id_receita])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);
GO

CREATE INDEX [fk_instrucao_receita_idx] ON [Instrucao] ([id_receita] ASC);
GO


-- -----------------------------------------------------
-- Table `Olivia`.`Favorito`
-- -----------------------------------------------------
CREATE TABLE [Favorito] (
  [id_utilizador] INT NOT NULL,
  [id_receita] INT NOT NULL,
  PRIMARY KEY ([id_receita], [id_utilizador]),
  CONSTRAINT [fk_favorito_utilizador1]
    FOREIGN KEY ([id_utilizador])
    REFERENCES [Utilizador] ([id_utilizador])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT [fk_favorito_utilizador2]
    FOREIGN KEY ([id_receita])
    REFERENCES [Receita] ([id_receita])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);
GO

CREATE INDEX [fk_favorito_utilizador1_idx] ON [Favorito] ([id_utilizador] ASC);
GO

CREATE INDEX [fk_favorito_utilizador2_idx] ON [Favorito] ([id_receita] ASC);
GO


/* SET SQL_MODE=@OLD_SQL_MODE; */
/* SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS; */
/* SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS; */
