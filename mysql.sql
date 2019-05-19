-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema Olivia
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema Olivia
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `Olivia` DEFAULT CHARACTER SET utf8 ;
USE `Olivia` ;

-- -----------------------------------------------------
-- Table `Olivia`.`Utilizador`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Olivia`.`Utilizador` (
  `id_utilizador` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `username` VARCHAR(100) NULL,
  `password` VARCHAR(45) NULL,
  `email` VARCHAR(45) NULL,
  `type` TINYINT NULL,
  `preferencia` INT NULL,
  `nome` VARCHAR(100) NULL,
  PRIMARY KEY (`id_utilizador`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Olivia`.`Receita`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Olivia`.`Receita` (
  `id_receita` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `nome` VARCHAR(45) NULL,
  `descricao` VARCHAR(100) NULL,
  `autor` INT UNSIGNED NOT NULL,
  `tipo` INT NULL,
  `calorias` FLOAT NULL,
  `gordura` FLOAT NULL,
  `carbohidratos` FLOAT NULL,
  `proteina` FLOAT NULL,
  `fibra` FLOAT NULL,
  `sodio` FLOAT NULL,
  PRIMARY KEY (`id_receita`),
  INDEX `id_autor_idx` (`autor` ASC) VISIBLE,
  CONSTRAINT `id_autor`
    FOREIGN KEY (`autor`)
    REFERENCES `Olivia`.`Utilizador` (`id_utilizador`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Olivia`.`Historico`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Olivia`.`Historico` (
  `id_utilizador` INT UNSIGNED NOT NULL,
  `id_receita` INT UNSIGNED NOT NULL,
  `data` DATETIME NULL,
  PRIMARY KEY (`id_receita`, `id_utilizador`),
  INDEX (`id_receita` ASC) VISIBLE,
  INDEX `id_utilizador_idx` (`id_utilizador` ASC) VISIBLE,
  CONSTRAINT `id_utilizador`
    FOREIGN KEY (`id_utilizador`)
    REFERENCES `Olivia`.`Utilizador` (`id_utilizador`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `id_receita`
    FOREIGN KEY (`id_receita`)
    REFERENCES `Olivia`.`Receita` (`id_receita`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Olivia`.`Avaliacao`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Olivia`.`Avaliacao` (
  `id_utilizador` INT UNSIGNED NOT NULL,
  `id_receita` INT UNSIGNED NOT NULL,
  `avaliacao` INT NULL,
  PRIMARY KEY (`id_receita`, `id_utilizador`),
  INDEX `id_utilizador1_idx` (`id_utilizador` ASC) INVISIBLE,
  INDEX `id_receita1_idx` (`id_receita` ASC) VISIBLE,
  CONSTRAINT `id_utilizador1`
    FOREIGN KEY (`id_utilizador`)
    REFERENCES `Olivia`.`Utilizador` (`id_utilizador`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `id_receita1`
    FOREIGN KEY (`id_receita`)
    REFERENCES `Olivia`.`Receita` (`id_receita`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Olivia`.`Ementa`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Olivia`.`Ementa` (
  `id_ementa` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `id_user` INT UNSIGNED NOT NULL,
  `date` DATE NULL,
  PRIMARY KEY (`id_ementa`),
  INDEX `id_user6_idx` (`id_user` ASC) VISIBLE,
  CONSTRAINT `id_user6`
    FOREIGN KEY (`id_user`)
    REFERENCES `Olivia`.`Utilizador` (`id_utilizador`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Olivia`.`Ementa_Receita`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Olivia`.`Ementa_Receita` (
  `id_receita` INT UNSIGNED NOT NULL,
  `id_ementa` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`id_receita`, `id_ementa`),
  INDEX `id_ementa_idx` (`id_ementa` ASC) VISIBLE,
  CONSTRAINT `id_receita2`
    FOREIGN KEY (`id_receita`)
    REFERENCES `Olivia`.`Receita` (`id_receita`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `id_ementa2`
    FOREIGN KEY (`id_ementa`)
    REFERENCES `Olivia`.`Ementa` (`id_ementa`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Olivia`.`Ingredientes`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Olivia`.`Ingredientes` (
  `id_ingrediente` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `nome` VARCHAR(45) NULL,
  `categoria` VARCHAR(45) NULL,
  PRIMARY KEY (`id_ingrediente`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Olivia`.`Receita_Ingrediente`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Olivia`.`Receita_Ingrediente` (
  `id_receita` INT UNSIGNED NOT NULL,
  `id_ingrediente` INT UNSIGNED NOT NULL,
  `quantidade` FLOAT NULL,
  `unidade` VARCHAR(45) NULL,
  PRIMARY KEY (`id_receita`, `id_ingrediente`),
  INDEX `id_ingrediente_idx` (`id_ingrediente` ASC) VISIBLE,
  INDEX `id_receita_idx` (`id_receita` ASC) INVISIBLE,
  CONSTRAINT `id_receita4`
    FOREIGN KEY (`id_receita`)
    REFERENCES `Olivia`.`Receita` (`id_receita`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `id_ingrediente4`
    FOREIGN KEY (`id_ingrediente`)
    REFERENCES `Olivia`.`Ingredientes` (`id_ingrediente`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Olivia`.`Instrucao`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Olivia`.`Instrucao` (
  `designacao` VARCHAR(100) NULL,
  `duracao` INT NULL,
  `posicao` INT UNSIGNED NOT NULL,
  `id_receita` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`id_receita`, `posicao`),
  INDEX `id_receita_idx` (`id_receita` ASC) VISIBLE,
  CONSTRAINT `id_receita5`
    FOREIGN KEY (`id_receita`)
    REFERENCES `Olivia`.`Receita` (`id_receita`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Olivia`.`Favoritos`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Olivia`.`Favoritos` (
  `id_utilizador` INT UNSIGNED NOT NULL,
  `id_receita` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`id_receita`, `id_utilizador`),
  INDEX `id_receita0_idx` (`id_receita` ASC) INVISIBLE,
  INDEX `id_utilizador0_idx` (`id_utilizador` ASC) VISIBLE,
  CONSTRAINT `id_utilizador0`
    FOREIGN KEY (`id_utilizador`)
    REFERENCES `Olivia`.`Utilizador` (`id_utilizador`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `id_receita0`
    FOREIGN KEY (`id_receita`)
    REFERENCES `Olivia`.`Receita` (`id_receita`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
