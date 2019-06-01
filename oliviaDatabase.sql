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
-- Table `Olivia`.`User`
-- -----------------------------------------------------
CREATE TABLE [User] (
  [Id_User] INT IdENTITY(1,1) NOT NULL,
  [Username] VARCHAR(100) NOT NULL,
  [Password] VARCHAR(100) NOT NULL,
  [Email] VARCHAR(45) NOT NULL,
  [Type] SMALLINT NOT NULL,
  [Preference] INT NOT NULL,
  [Name] VARCHAR(100) NOT NULL,
  [Active] BIT NOT NULL DEFAULT 1,
  PRIMARY KEY ([Id_User]))
;
GO


-- -----------------------------------------------------
-- Table `Olivia`.`Recipe`
-- -----------------------------------------------------
CREATE TABLE [Recipe] (
  [Id_Recipe] INT IdENTITY(1,1) NOT NULL,
  [Name] VARCHAR(100) NOT NULL,
  [Description] TEXT NOT NULL,
  [Creator] INT NOT NULL,
  [Type] INT NOT NULL,
  [Duration] INT NOT NULL,
  [Calories] FLOAT NOT NULL,
  [Fat] FLOAT NOT NULL,
  [Carbs] FLOAT NOT NULL,
  [Protein] FLOAT NOT NULL,
  [Fiber] FLOAT NOT NULL,
  [Sodium] FLOAT NOT NULL,
  [Active] BIT NOT NULL DEFAULT 1,
  PRIMARY KEY ([Id_Recipe]),
  CONSTRAINT [FK_Id_Creator]
    FOREIGN KEY ([Creator])
    REFERENCES [User] ([Id_User])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);
GO

CREATE INDEX [FK_Creator_Recipe_Idx] ON [Recipe] ([Creator] ASC);
GO

-- -----------------------------------------------------
-- Table `Olivia`.`History`
-- -----------------------------------------------------
CREATE TABLE [History] (
  [Id_User] INT NOT NULL,
  [Id_Recipe] INT NOT NULL,
  [Date] DATETIME NOT NULL,
  PRIMARY KEY ([Id_Recipe], [Id_User],[Date]),
  CONSTRAINT [FK_Id_User]
    FOREIGN KEY ([Id_User])
    REFERENCES [User] ([Id_User])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT [FK_Id_Recipe]
    FOREIGN KEY ([Id_Recipe])
    REFERENCES [Recipe] ([Id_Recipe])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

GO

CREATE INDEX [FK_Id_User_Idx] ON [History] ([Id_User]);
GO

CREATE INDEX [FK_Id_Recipe_Idx] ON [History] ([Id_Recipe]);
GO

-- -----------------------------------------------------
-- Table `Olivia`.`Rating`
-- -----------------------------------------------------
CREATE TABLE [Rating] (
  [Id_User] INT NOT NULL,
  [Id_Recipe] INT NOT NULL,
  [Rating] INT NOT NULL,
  PRIMARY KEY ([Id_Recipe], [Id_User]),
  CONSTRAINT [FK_Id_User_Rating]
    FOREIGN KEY ([Id_User])
    REFERENCES [User] ([Id_User])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT [FK_Id_Recipe_Rating]
    FOREIGN KEY ([Id_Recipe])
    REFERENCES [Recipe] ([Id_Recipe])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
;
GO

CREATE INDEX [FK_Id_User_Rating_Idx] ON [Rating] ([Id_User] ASC);
GO

CREATE INDEX [FK_Id_Recipe_Rating_Idx] ON [Rating] ([Id_Recipe] ASC);
GO


-- -----------------------------------------------------
-- Table `Olivia`.`Menu`
-- -----------------------------------------------------
CREATE TABLE [Menu] (
  [Id_Menu] INT IDENTITY(1,1) NOT NULL,
  [Id_User] INT NOT NULL,
  [Date] DATE NULL,
  PRIMARY KEY ([Id_Menu]),
  CONSTRAINT [FK_User_Menu]
    FOREIGN KEY ([Id_User])
    REFERENCES [User] ([Id_User])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);
GO

CREATE INDEX [FK_User_Menu_Idx] ON [Menu] ([Id_User] ASC);
GO

-- -----------------------------------------------------
-- Table `Olivia`.`Menu_Recipe`
-- -----------------------------------------------------
CREATE TABLE [Menu_Recipe] (
  [Id_Recipe] INT NOT NULL,
  [Id_Menu] INT NOT NULL,
  PRIMARY KEY ([Id_Recipe], [Id_Menu]),
  CONSTRAINT [FK_Menu_Recipe_Recipe]
    FOREIGN KEY ([Id_Recipe])
    REFERENCES [Recipe] ([Id_Recipe])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT [FK_Menu_Recipe_Menu]
    FOREIGN KEY ([Id_Menu])
    REFERENCES [Menu] ([Id_Menu])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

GO

CREATE INDEX [FK_Menu_Recipe_Recipe_Idx] ON [Menu_Recipe] ([Id_Menu] ASC);
GO

-- -----------------------------------------------------
-- Table `Olivia`.`Ingredient`
-- -----------------------------------------------------
CREATE TABLE [Ingredient] (
  [Id_Ingredient] INT IdENTITY(1,1) NOT NULL,
  [Name] VARCHAR(45) NOT NULL,
  [Category] VARCHAR(45) NULL,
  [Active] BIT NOT NULL DEFAULT 1,
  PRIMARY KEY ([Id_Ingredient]))
;

GO


-- -----------------------------------------------------
-- Table `Olivia`.`Recipe_Ingredient`
-- -----------------------------------------------------
CREATE TABLE [Recipe_Ingredient] (
  [Id_Recipe] INT NOT NULL,
  [Id_Ingredient] INT NOT NULL,
  [Quantity] FLOAT NOT NULL,
  [Unit] VARCHAR(45) NOT NULL,
  PRIMARY KEY ([Id_Recipe], [Id_Ingredient]),
  CONSTRAINT [FK_Recipe_Ingredient_Recipe]
    FOREIGN KEY ([Id_Recipe])
    REFERENCES [Recipe] ([Id_Recipe])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT [FK_Recipe_Ingredient_Ingredient]
    FOREIGN KEY ([Id_Ingredient])
    REFERENCES [Ingredient] ([Id_Ingredient])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

GO


CREATE INDEX [FK_Recipe_Ingredient_Recipe_Idx] ON [Recipe_Ingredient] ([Id_Recipe] ASC);
GO

CREATE INDEX [FK_Recipe_Ingredient_Ingredient_Idx] ON [Recipe_Ingredient] ([Id_Ingredient] ASC);
GO


-- -----------------------------------------------------
-- Table `Olivia`.`Instruction`
-- -----------------------------------------------------
CREATE TABLE [Instruction] (
  [Designation] TEXT NOT NULL,
  [Position] INT NOT NULL,
  [Id_Recipe] INT NOT NULL,
  PRIMARY KEY ([Id_Recipe], [Position]),
  CONSTRAINT [FK_Instruction_Recipe]
    FOREIGN KEY ([Id_Recipe])
    REFERENCES [Recipe] ([Id_Recipe])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);
GO

CREATE INDEX [FK_Instruction_Recipe_Idx] ON [Instruction] ([Id_Recipe] ASC);
GO


-- -----------------------------------------------------
-- Table `Olivia`.`Favorite`
-- -----------------------------------------------------
CREATE TABLE [Favorite] (
  [User_key] INT NOT NULL,
  [Recipe_key] INT NOT NULL,
  PRIMARY KEY ([User_key], [Recipe_key]),
  CONSTRAINT [FK_Favorite_User1]
    FOREIGN KEY ([User_key])
    REFERENCES [User] ([Id_User])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT [FK_Favorite_Rec2]
    FOREIGN KEY ([Recipe_key])
    REFERENCES [Recipe] ([Id_Recipe])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);
GO

CREATE INDEX [FK_Favorite_User1_Idx] ON [Favorite] ([User_key] ASC);
GO

CREATE INDEX [FK_Favorite_Rec2_Idx] ON [Favorite] ([Recipe_key] ASC);
GO


/* SET SQL_MODE=@OLD_SQL_MODE; */
/* SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS; */
/* SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS; */
