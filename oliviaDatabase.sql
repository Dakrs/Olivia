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
  [Active] INT NOT NULL DEFAULT 0,
  PRIMARY KEY ([Id_Recipe]),
  CONSTRAINT [FK_Id_Creator]
    FOREIGN KEY ([Creator])
    REFERENCES [User] ([Id_User])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);
GO

CREATE INDEX [FK_Creator_Recipe_Idx] ON [Recipe] ([Creator] ASC);
GO

CREATE TABLE [Recipe_Image] (
  [Id_Recipe] INT NOT NULL,
  [Image] VARBINARY(MAX) NOT NULL,
  PRIMARY KEY ([Id_Recipe]),
  CONSTRAINT [FK_Id_Recipe_Image]
    FOREIGN KEY ([Id_Recipe])
    REFERENCES [Recipe] ([Id_Recipe])
    ON DELETE CASCADE
    ON UPDATE NO ACTION);
;
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
  [Date] DATETIME NULL,
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
  [Position] INT NOT NULL,
  PRIMARY KEY ([Id_Recipe], [Id_Menu],[Position]),
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

CREATE INDEX [FK_Menu_Recipe_Recipe_Idx] ON [Menu_Recipe] ([Id_Recipe] ASC);
GO
CREATE INDEX [FK_Menu_Recipe_Menu_Idx] ON [Menu_Recipe] ([Id_Menu] ASC);
GO
CREATE INDEX [FK_Menu_Recipe_Position_Idx] ON [Menu_Recipe] ([Position] ASC);
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

-- -----------------------------------------------------
-- Table `Olivia`.`Warning`
-- -----------------------------------------------------
CREATE TABLE [Warning] (
  [Id_Recipe] INT NOT NULL,
  [Id_Warning] INT NOT NULL,
  [Warning] TEXT NOT NULL,
  PRIMARY KEY ([Id_Recipe], [Id_Warning]),
  CONSTRAINT [FK_IdRecipe]
    FOREIGN KEY ([Id_Recipe])
    REFERENCES [Recipe] ([Id_Recipe])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);
GO



/* SET SQL_MODE=@OLD_SQL_MODE; */
/* SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS; */
/* SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS; */


INSERT into [User] VALUES
('admin','21232f297a57a5a743894a0e4a801fc3','admin@olivia.pt',1,1,'admin',1);

SET IDENTITY_INSERT Recipe on;

insert into Recipe (Id_Recipe, Name, Description, Creator, Type,Calories, Fat, Carbs, Protein, Fiber, Sodium, Duration, Active)
    values (  1,
                'Bolo de Requeijão com Chocolate',
                'Sobremesa deliciosa e de rápida confeção. Ideal para os apreciadores de chocolate e queijo fresco. Os frutos vermelhos enriquecem ainda mais o seu saber.',
                1,
                2,
                305,
                13,
                35,
                8,
                5,
                0,
                45,
				1
        ),
        (    2,
                'Arroz de cabidela',
                'Um clássico da cozinha portuguesa, cozinhado com uma seleção de ingredientes característicos da nossa região mediterrânica',
                1,
                1,
                526,
                22,
                62,
                25,
                4,
                0,
                80,
				1
        ),
        (    3,
                'Gomos de marmelo com pistácios e lima',
                'Sobremesa deliciosa com um irresistível sabor da simplicidade.',
                1,
                2,
                116,
                4,
                18,
                2,
                4,
                0,
                45,
				1
        ),
        (    4,
                'Sopa de belgroegas com queijo de cabra',
                'Um caldo genuíno e cheio de sabor. Adequada a diabéticos e também pode se substituir o queijo de cabra por tofu para se tornar uma receita vegan',
                1,
                3,
                232,
                10,
                24,
                11,
                6,
                0,
                45,
				1
        ),
        ( 6,
           'Gelado merengado',
           'Um saborosa sobremesa especialmente dedicada às quentes tardes de verão. Além do seu sabor é ainda uma fonte rica de vitamina C.',
           1,
           2,
           162,
           3,
           30,
           3,
           5,
           0,
           13,
		   1
         ),
         ( 5,
            'Caril de peru com coco e maçã',
            'Uma combinação exótica de sabores rica em potássio, fósforo, ferro e zinco. Uma fonte excelente de vitamina B12 de rápida preparação e confecção',
            1,
            1,
            468,
            8,
            50,
            25,
            4,
            0,
            60,
			1
		),
        ( 7,
           'Camembert com Avelãs e Mel',
           'Uma entrada, três paladares. De extrema rápida confeção, ideal para um estilo de vida sofisticado e acelerado.',
           1,
           3,
           245,
           16,
           13,
           13,
           2,
           0,
           8,
		   1
        ),
        ( 8,
           'Coderniz no Forno',
           'O sabor da caça... Um delicioso e suculento prato de carne, de confeção relativamente rápida. Um verdadeiro pitéu.',
           1,
           1,
           330,
           15,
           32,
           18,
           5,
           0,
           50,
		   1
        );


SET IDENTITY_INSERT Recipe off;
SET IDENTITY_INSERT Ingredient on;

insert into Ingredient (Id_Ingredient, Name, Category) values
    (1, 'Ovos', 'Proteína'),
    (2, 'Requeijão', 'Laticínio'),
    (3, 'Açúcar', 'Tempero'),
    (4, 'Chocolate culinário', 'Laticínio'),
    (5, 'Framboesas', 'Fruta'),
    (6, 'Farinha', 'Massa'),
    (7, 'Fermento em pó', 'Massa'),
    (8, 'Leite', 'Laticínio'),
    (9, 'Peito de peru','Carne'),
    (10, 'Sal','Tempero'),
    (11, 'Caril em pó','Tempero'),
    (12, 'Massa de allho','Tempero'),
    (13, 'Limão','Fruta'),
    (14, 'Louro','Tempero'),
    (15, 'Malagueta vermelha','Tempero'),
    (16, 'Azeite','Tempero'),
    (17, 'Vinagre','Tempero'),
    (18, 'Cebola picada','Vegetal'),
    (19, 'Pau de Canela','Tempero'),
    (20, 'Leite de coco','Tempero'),
    (21, 'Maçã','Fruta'),
    (22, 'Arroz basmati','Acompanhamento'),
    (23, 'Folhas de coentros','Tempero'),
    (24, 'Salada de rúcula','Acompanhamente'),
    (25, 'Vinagre balsâmico','Tempero'),
    (26, 'Clara','Proteína'),
    (27, 'Gelado de iogurte','Acompanhamento'),
    (28, 'Frango do campo com sangue e miúdos', 'Proteína'),
    (29, 'Aipo', 'Vegetal'),
    (30, 'Cenoura', 'Vegetal'),
    (31, 'Cominhos', 'Tempero'),
    (32, 'Limão', 'Fruta'),
    (33, 'Salsa', 'Tempero'),
    (34, 'Arroz', 'Acompanhamento'),
    (35, 'Salada', 'Vegetal'),
    (36, 'Tomate cherry', 'Vegetal'),
    (37, 'Oregãos', 'Tempero'),
    (38, 'Marmelos médios','Fruta'),
    (39, 'Água','Água'),
    (40, 'Iogurte Grego Natural','Laticínio'),
    (41, 'Pistácio','Fruto seco'),
    (42, 'Lima','Fruta'),
    (43, 'Queijo de cabra','Laticínio'),
    (44, 'Cabeça de Alho','Tempero'),
    (45, 'Cebola','Vegetal'),
    (46, 'Beldroegas','Vegetal'),
    (47, 'Croutons de pão torrado', 'Massa'),
    (48,'Miolo de avelã','Fruto seco'),
    (49,'Queijo Camembert','Laticínio'),
    (50,'Azeite','Tempero'),
    (51,'Orégãos','Tempero'),
    (52,'Mel de rosmaninho','Adoçante Natural'),
    (53,'Mistura de Alfaces','Vegetal'),
    (54,'Tostas finas integrais','Cereal'),
    (55,'Codernizes (660g)','Carne'),
    (56,'Limão pequeno','Fruta'),
    (57,'Sal','Tempero'),
    (58,'Pimenta','Tempero'),
    (59,'Fatias muito finas de bacon','Derivados de Carne'),
    (60,'Cebolas grandes','Vegetal'),
    (61,'Cogumelos Portobello pequenos','Fungo'),
    (62,'Abóbora limpa','Fruit'),
    (63,'Figos secos macios','Figo'),
    (64,'Tomilho','Tempero'),
    (65,'Azeite','Tempero'),
    (66,'Batatas pequenas','Vegetal');


SET IDENTITY_INSERT Ingredient off;


------------------------------------------receita 1------------------------------------------------------------------
insert into Recipe_Ingredient (Id_Recipe, Id_Ingredient, Quantity, Unit) values
    (1, 1, 4, 'unidades'),
    (1, 2, 260, 'g'),
    (1, 3, 150, 'g'),
    (1, 4, 1, 'tablete'),
    (1, 5, 300, 'g'),
    (1, 6, 130, 'g'),
    (1, 7, 3, 'c. de sobremesa'),
    (1, 8, 4, 'c. de sopa');

insert into Instruction (Designation, Position, Id_Recipe) values
    ('Ligue o forno a 180ºC', 1, 1),
    ('Parta os ovos separando as claras das gemas. Bata as claras em castelo, junte-lhes 50g de açúcar até ficarem bem espessas e reserve.', 2, 1),
    ('Junte o requeijão às gemas, adicione o restante açúcar e bata com uma batedeira até ficar em creme', 3, 1),
    ('Adicione a farinha e o fermento em pó e misture bem. Pique finalmente metade do chocolate e junte-o à massa.', 4, 1),
    ('Envolva delicadamente as claras em castelo na massa e eite tudo numa forma de mola previamente untada.', 5, 1),
    ('Reserve metadedas framboesas para decoração e distribua as restantes pela massa, enterrando-as levemente.', 6, 1),
    ('Leve ao forno por cerca de 30 minutos.', 7, 1),
    ('Depois de pronto, desenforme o bolo para o prato de servir e deixe arrefecer.', 8, 1),
    ('Parta o restante chocolate, junte leite leve ao microondas durante cerca de 50 segundos. Mexa com uma vara de arames e espalhe sobre o bolo.', 9, 1),
    ('Decore com as framboesas reservadas e sirva. Bom apetite!', 10, 1);

------------------------------------------receita 2------------------------------------------------------------------
insert into Recipe_Ingredient (Id_Recipe, Id_Ingredient, Quantity, Unit) values
    (2, 28, 750, 'g'),
    (2, 10, 1, 'c. sopa'),
    (2, 29, 1, 'talo'),
    (2, 30, 120, 'g'),
    (2, 45, 150, 'g'),
    (2, 16, 3, 'c. sopa'),
    (2, 14, 1, 'folha'),
    (2, 31, 1, 'c. de sobremesa'),
    (2, 34, 2.5, 'chávenas'),
    (2, 13, 300, 'g'),
    (2, 33, 1, 'c. de sopa'),
    (2, 35, 160, 'g'),
    (2, 36, 350, 'g'),
    (2, 25, 2, 'c. de sopa'),
    (2, 37, 1, 'c. de sopa');

insert into Instruction (Designation, Position, Id_Recipe) values
    ('Reserve os miúdos e o sangue. Limpe o frango de canículas e penugem e coloque numa panela', 1, 2),
    ('Cubra com água, junte o sal, o talo de aipo e a cenoura lavada e cortada em pedaços.', 2, 2),
    ('Leve ao lume e deixe ferver até o frango estar tenro.', 3, 2),
    ('Refogue a cebola picaa num tacho com azeite até estar bem dourada. Adicione a folha de louro e os miúdos cortados em pedaços.', 4, 2),
    ('Polvilhe com cominhos e deixe alourar. Meça 1 litro de caldo da cozedura do frango e deite no tacho.', 5, 2),
    ('Quando retomar a fervura introduza o arroz, tape e deixe ferver durante 10 minutos. Entretanto limpe o frango de peles e ossos e desfie-o em pedaços.', 6, 2),
    ('Junteao arroz, adicione o sangue, mexa e deixe ferver mais 10 minutos', 7, 2),
    ('Antes de servir regue com sumo de limão e salpique com salsa picada', 8, 2),
    ('Para finalizar, sirva acompanhado da salada e do tomate cherry cortado em metades, temperado com azeite, vinagre balsâmico e oregãos. Bom Apetite!!', 9, 2);

------------------------------------------receita 3------------------------------------------------------------------
insert into Recipe_Ingredient (Id_Recipe, Id_Ingredient, Quantity, Unit) values
    (3, 38, 3, '500 gramas'),
    (3, 3, 3, 'c. de sopa'),
    (3, 19, 3, 'unidade'),
    (3, 39, 3, 'c. de sopa'),
    (3, 40, 4, 'c. de sopa'),
    (3, 41, 45, 'gramas'),
    (3, 42, 1, 'unidade(60g)');

insert into Instruction (Designation, Position, Id_Recipe) values
     ('Destaque os pistácios e reserve',1,3),
     ('Lave os marmelos e descasque-os',2,3),
     ('Elimine os caroços e corte-os em fatias para dentro de um tacho',3,3),
     ('Junte o açucar,o pau da canela, a água e leve a ferver destapado e sobre lume brando, até os marmelos estarem tenros',4,3),
     ('Retire os marmelos do lume e deixe arrefecer',5,3),
     ('Se a calda estiver muito líquida,ferve-a durante mais uns minutos',6,3),
     ('Distribua os marmelos por taças individuais e coloque por cima uma colher de sopa de iogurte grego',7,3),
     ('Salpique com miolo de pistácio grosseiramente picado e a raspa de casca da lima',8,3);

------------------------------------------receita 4------------------------------------------------------------------
insert into Recipe_Ingredient (Id_Recipe, Id_Ingredient, Quantity, Unit) values
    (4, 43, 150, 'gramas'),
    (4, 44, 1, 'unidade'),
    (4, 45, 2, 'gramas'),
    (4, 46, 1, 'molho(900g)'),
    (4, 16, 2, 'c. de sopa'),
    (4, 39, 1, 'litro'),
    (4, 33, 1, 'c. de sopa'),
    (4, 47, 150, 'gramas');


  insert into Instruction (Designation, Position, Id_Recipe) values
     ('Corte o queijo em cubos e reserve',1,4),
     ('Descasque os dentes de alho e pique-os finalmente',2,4),
     ('Deite-os numa panela, junte o aceite e leve ao lume',3,4),
     ('Entretanto, descasque e pique as cebolas e junte-as ao alho picado',4,4),
     ('Tape e deixe cozinhar sobre lume brando',5,4),
     ('Lave muito bem as beldroegas, separe e reserve as folhas',6,4),
     ('Pique finalmente os talos e adicieone-os à cebola',7,4),
     ('Deixe cozinhar tapado durante certa de 5 minutos',8,4),
     ('Junte as folhas, mexa e passados outros 5 minutos regue com a água a ferver',9,4),
     ('Tempere com sal e pimenta moída na altura e deixe ferver por mais 5 minutos',10,4),
     ('Distribua os croutons de pão pelos pratos ep or cima deite a sopa',11,4),
     ('Sirva acompanhada dos dubos de queijo de cabra',12,4);

---------------------------------------------receita 5---------------------------------------------------------------
INSERT into Recipe_Ingredient VALUES
    (5,9,400,'gramas'),
    (5,10,1,'c. chá'),
    (5,11,2,'c. sopa'),
    (5,12,1,'c. sobremesa'),
    (5,13,0.5,'unidade'),
    (5,14,1,'unidade'),
    (5,15,1,'unidade'),
    (5,16,4,'c. sopa'),
    (5,17,1,'c. sopa'),
    (5,18,100,'gramas'),
    (5,19,1,'unidade'),
    (5,20,100,'ml'),
    (5,21,1,'unidade'),
    (5,22,200,'gramas'),
    (5,23,1,'unidade'),
    (5,24,200,'gramas');

INSERT into Instruction VALUES
    ('Misture 2 colheres de sopa de caril em pó, com uma colher de sopa em massa de alho',0,5),
    ('Corte o peito de peru em tiras. Junte-lhe a pasta de caril e misture bem',1,5),
    ('Adicione a folha de louro, partida em pedaços. Limpe a malagueta de sementes, corte-a em pedacinhos e junte à carne',2,5),
    ('Deixe marinar durante 30 minutos. Aqueça 3 colheres de sopa de azeite num tacho, junte os pedaços de carne e aloure-os de todos os lados mexendo.',3,5),
    ('Adicione a cebola picada e o pau de canela, reduza o lume, tape e deixe cozinhar suavemente durante cerca de 10 minutos.',4,5),
    ('Mexa de vez em quando e se achar necessário borrife com um pouco de água quente.',5,5),
    ('Junte leite de coco e a maçã, previamente lavada e cortada em fatias finas.',6,5),
    ('Deixe cozinhar durante mais 10 minutos ou até a carne estar tenra.',7,5),
    ('Em simultâneo leve um tacho ao lume com àgua temperada com sal e quando ferver deite o arroz e deixe cozer cerca de 15 minutos.',8,5),
    ('Regue a carne com umas gotas de sumo de limão e salpique com folhas de coentros.',9,5),
    ('Acompanhe com arroz e salada de rúcula temperada com 1 colher de sopa de azeite e vinagre.',10,5);

---------------------------------------------receita 6---------------------------------------------------------------

INSERT into Recipe_Ingredient VALUES
    (6,5,250,'gramas'),
    (6,3,35,'gramas'),
    (6,25,1,'c. sobremesa'),
    (6,26,1,'unidade'),
    (6,27,4,'bolas');

INSERT into Instruction VALUES
    ('Lave as framboesas e junte-lhes uma colher de sopa de açúcar e o vinagre balsâmico',0,6),
    ('Misture bem e distribua por quatro taças individuais que possam ir ao forno.',1,6),
    ('Bata a clara com a batedeira até começar a fazer espuma.',2,6),
    ('Junte o açúcar e continue a bater até estar brilhante e bem firme. Entretanto ligue o grelhado do forno.',3,6),
    ('Coloque uma bola de gelado em cada taça, por cima distribua o merengue.',4,6),
    ('Leva ao forno durante cerca de três minutos para alourar. Sirva sem demora',5,6);

---------------------------------------------receita 7---------------------------------------------------------------
insert into Recipe_Ingredient (Id_Recipe, Id_Ingredient, Quantity, Unit) values
    (7,48,40,'g'),
    (7,49,250,'g'),
    (7,50,1,'c. de sobremesa'),
    (7,51,1,'c. de chá'),
    (7,52,1,'c. de sobremesa'),
    (7,25,1,'c. de sobremesa'),
    (7,53,150,'g'),
    (7,54,24,'unidades');

insert into Instruction (Designation, Position, Id_Recipe) values
    ('Leve as avelãs ao lume numa frigideira antiaderente e deixe tostar levemente',1,7),
    ('Esfregue as avelãs num pano para as pelar',2,7),
    ('Desembrulhe o queijo e retire cuidadosamente a casca branca da sua superfície',3,7),
    ('Salpique com orégãos e regue com azeite',4,7),
    ('Leve o queijo ao microondas dentro da própria embalagem entre 1 a 2 minutos numa potencia média (600W)',5,7),
    ('Mexa cuidadosamente a pasta do queijo, que deve estar derretida, regue com mel de rosmaninho e salpique as avelãs',6,7),
    ('FINALIZAÇÃO: Sirva, sem demora, com tostas acompanhadas de mistura de alfaces salpicadas com vinagre bolsâmico',7,7);


---------------------------------------------receita 8---------------------------------------------------------------

insert into Recipe_Ingredient (Id_Recipe, Id_Ingredient, Quantity, Unit) values
    (8,55,6,'unidades'),
    (8,56,1,'unidade'),
    (8,57,1,'c. de sopa'),
    (8,58,0,'q.b'),
    (8,59,6,'unidades'),
    (8,60,800,'g'),
    (8,61,300,'g'),
    (8,62,200,'g'),
    (8,63,6,'unidades'),
    (8,64,0,'q.b'),
    (8,65,3,'c. de sopa'),
    (8,25,1,'c. de sopa'),
    (8,66,750,'g');

insert into Instruction (Designation, Position, Id_Recipe) values
    ('Ligue o forno e regule-o para os 180ºC',1,8),
    ('Lave muito bem o limão e corte-o em 6 gomos',2,8),
    ('Enfie cada gomo de limão numa codorniz, tempere-a levemente com sal e pimenta',3,8),
    ('Enrole-a numa fatia fina de bacon e coloque num tabuleiro de forno',4,8),
    ('Descasque as cebolas e corte-as em gomos finos, limpe os cogumelos e corte a abóroa em cubos',5,8),
    ('Misture todos os legumes e junte-lhes os figos cortados ao meio',6,8),
    ('Disponha a mistura à volta das codornizes',7,8),
    ('Junte 2 a 3 hastes de tomilho, regue com o azite e borrife com vinagre balsâmico',8,8),
    ('Tape o tabuleiro com folha de alumínio e leve ao forno durante 25 minutos',9,8),
    ('Ao mesmo tempo coza as batitinhas em água temperada com sal',10,8),
    ('Passadas os 25 minutos, retire a folha de alumínio e aumente o forno para os 200ºC',11,8),
    ('FINALIZAÇÃO: Escorra as batatas, disponha-as à volta das codernizes e leve o tabuleiro de novo ao forno durante mais 10 minutos',12,8);

insert into Warning (Id_Recipe, Id_Warning, Warning) values
    (1,1,'TESOURO NUTRICIONAL: Fonte de fósforo, cálcio, vitamina B2 e ácido fólico. Rico em B12.'),
    (2,1,'TESOURO NUTRICIONAL: Rico em potássio e vitaminas B1, B12, K e ácido fólico. Excelente fonte de vitamina B6, vitamina C, betacaroteno e niacina.'),
    (2,2,'CONSERVAÇÃO: Congele o frango dentro de um saco de plástico transparente, com uma etiqueta, por um período máximo de 15 dias. Na etiqueta deve constar a data de congelação e a data limite de consumo. Para descongelar coloque o frango na última prateleeira do frigorífico.'),
    (2,3,'NOTA: Se comprar o frango inteiro, reserve a parte não utilizada e congele para utilizar numa próxima receita.'),
    (4,1,'TESOURO NUTRICIONAL: Rico em magnésio e vitaminca C.'),
    (4,2,'SUGESTÃO SAUDÁVEL: Acabe a refeição com fruta da época. Por exemplo, um punhado de amoras.'),
    (4,3,'SUGESTÃO VEGAN: Substitua o queijo de cabra por tofu. Junte-lhe 2 colheres de sopa de molha de soja, a raspa da casca e o sumo de limão e um pouco de pimentão doce. Misture bem e deixe marinar. Junte ao preparado na altura de servir.'),
    (6,1,'TESOURO NUTRICIONAL: Fonte de vitamina C.'),
    (6,2,'SUGESTÃO SAUDÁVEL: Pode substituir o gelado de iogurte com frutos silvestres, por gelado light de morango, reduzindo desta forma a quantidade de gordura e de açúcar.'),
    (6,3,'SUGESTÃO: Pode substituir as framboesas por morangos picados'),
    (5,1,'TESOURO NUTRICIONAL: Rico em potássio, fósfero, ferro, zinco e ácido fólico. Excelente fonte de vitamnia B12.'),
    (5,2,'GLOSSÁRIO GASTRONÓMICO: Marinar - mergulhar alimentos crus, normalmente carne ou peixe, num líquido composto por vinho, azeite, alho, ervas ou especiarias durante algumas horas. Não só dá sabor aos alimentos como os torna mais tenros.'),
    (7,1,'TESOURO NUTRICIONAL: Fonte de ácido fólico, magnésio, fósforo e vitaminas B2, B12, E, retinol, biotina e ácido fólico. Rico em vitamina K.'),
    (7,2,'GLOSSÁRIO GASTRONÓMICO: Pelar - tirar a pele a frutos ou vegetais.'),
    (8,1,'TESOURO NUTRICIONAL: Excelente fonte de potássio, vitamina B6, B1 e niacina.'),
    (8,2,'GLOSSÁRIO GASTRONÓMICO: Salter - cozinhar rapidamente em gordura bem quente.');


---------------------------


INSERT INTO Recipe_image (Id_Recipe, Image) 
	SELECT 1, BulkColumn FROM Openrowset( Bulk 'C:\Users\51-Megamind\Desktop\LI4\recipe_images\1.png', Single_Blob) as image

INSERT INTO Recipe_image (Id_Recipe, Image) 
	SELECT 2, BulkColumn FROM Openrowset( Bulk 'C:\Users\51-Megamind\Desktop\LI4\recipe_images\2.png', Single_Blob) as image

INSERT INTO Recipe_image (Id_Recipe, Image) 
	SELECT 3, BulkColumn FROM Openrowset( Bulk 'C:\Users\51-Megamind\Desktop\LI4\recipe_images\3.png', Single_Blob) as image

INSERT INTO Recipe_image (Id_Recipe, Image) 
	SELECT 4, BulkColumn FROM Openrowset( Bulk 'C:\Users\51-Megamind\Desktop\LI4\recipe_images\4.png', Single_Blob) as image

INSERT INTO Recipe_image (Id_Recipe, Image) 
	SELECT 5, BulkColumn FROM Openrowset( Bulk 'C:\Users\51-Megamind\Desktop\LI4\recipe_images\5.png', Single_Blob) as image

INSERT INTO Recipe_image (Id_Recipe, Image) 
	SELECT 6, BulkColumn FROM Openrowset( Bulk 'C:\Users\51-Megamind\Desktop\LI4\recipe_images\6.png', Single_Blob) as image

INSERT INTO Recipe_image (Id_Recipe, Image) 
	SELECT 7, BulkColumn FROM Openrowset( Bulk 'C:\Users\51-Megamind\Desktop\LI4\recipe_images\7.png', Single_Blob) as image

INSERT INTO Recipe_image (Id_Recipe, Image) 
	SELECT 8, BulkColumn FROM Openrowset( Bulk 'C:\Users\51-Megamind\Desktop\LI4\recipe_images\8.png', Single_Blob) as image
