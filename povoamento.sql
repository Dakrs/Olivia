USE [Olivia]
GO


INSERT INTO [dbo].[Ingrediente]
           ([nome]
           ,[categoria])
     VALUES
           ('Ovos','Ovos'),
           ('Enguia','Pescado'),
           ('Broculos','Horticolas'),
           ('Bacalhau','Pescado'),
           ('Cogumelos','Horticolas'),
           ('Espinafres','Horticolas'),
           ('Bife de Peru','Carne'),
           ('Salmao','Pescado'),
           ('Maca','Fruta'),
           ('Kiwi','Fruta'),
           ('Feijao Preto','Leguminosas'),
           ('Arroz','Acompanhamento'),
           ('Massa','Acompanhamento'),
		   ('Atum','Pescado'),
		   ('Salsichas','Carne'),
		   ('Agua','Agua')
GO

INSERT INTO [dbo].[Utilizador]
           ([username]
           ,[password]
           ,[email]
           ,[type]
           ,[preferencia]
           ,[nome])
     VALUES
           ('Megadeus','superseguro2000','62random@gmail.com',1,0,'varchar[megadeus]'),
           ('Randao','1','62random@hotmail.com',1,0,'Gajo aleatorio'),
           ('Amigo da Dora','2','62random@iol.pt',1,0,'Amor para a vida toda'),
           ('Nimbus2000','3','62random@live.com.pt',1,0,'Apetece-me algo')
GO
	


INSERT INTO [dbo].[Receita]
           ([nome]
           ,[descricao]
           ,[autor]
           ,[tipo]
           ,[calorias]
           ,[gordura]
           ,[carbohidratos]
           ,[proteina]
           ,[fibra]
           ,[sodio])
     VALUES
           ('Massa com Atum','Massa com Atum, exelente para quem precisa de uma refeicao rapida e saudavel',1,1,359,370,23,0.1,0.0001,5.1),
           ('Sopa de espinafres','Sopa extremamente saudavel',1,1,0,0,0,100000,0.0001,5.1),
           ('Punheta de Bacalhau','Nome auto explicativo',2,1,12,75,23,0.1,0.23,5.12)
GO

SET DATEFORMAT dmy;  
INSERT INTO [dbo].[Historico]
           ([id_utilizador]
           ,[id_receita]
           ,[data])
     VALUES
           (1,1,(convert(datetime,'13-11-18 ',5))),           
           (2,2,(convert(datetime,'14-10-18 ',5))),
           (1,2,(convert(datetime,'17-3-19 ',5))),
           (3,1,(convert(datetime,'5-4-19 ',5))),
           (4,1,(convert(datetime,'16-5-19 ',5))),
           (4,2,(convert(datetime,'26-4-19 ',5)))
GO

INSERT INTO [dbo].[Avaliacao]
           ([id_utilizador]
           ,[id_receita]
           ,[avaliacao])
     VALUES
           (1,1,3),           
           (2,2,1),
           (1,2,2),
           (4,1,5),
           (4,2,3)
GO

INSERT INTO [dbo].[Favorito]
           ([id_utilizador]
           ,[id_receita])
     VALUES
           (1,1),
           (1,2),
           (1,3),
           (2,2),
           (3,2)
GO


INSERT INTO [dbo].[Receita_Ingrediente]
           ([id_receita]
           ,[id_ingrediente]
           ,[quantidade]
           ,[unidade])
     VALUES
		   (1,13,500,'Gramas'),
		   (1,14,1,'Embalagem'),
		   (2,16,500,'MiliLitros'),
		   (2,3,200,'Gramas'),
		   (2,6,500,'Gramas'),
		   (3,1,2,'KG')
GO

INSERT INTO [dbo].[Instrucao]
           ([designacao]
           ,[duracao]
           ,[posicao]
           ,[id_receita])
     VALUES
		   ('Fazer Arroz', 10, 1,1),
		   ('Meter Atum em cima', 1, 2,1),
		   ('Meter Agua numa panela e ferver', 5, 1,2),
		   ('Meter espinafres e broculos na agua e esperar 15 minutos', 15, 2,2),
		   ('Bruxaria Extrema, apenas acreditar', 30, 1,3)
GO


INSERT INTO [dbo].[Ementa]
           ([id_user]
           ,[data])
     VALUES
		   (1,(convert(datetime,'13-11-18 ',5))),
		   (1,(convert(datetime,'22-11-18 ',5))),
		   (2,(convert(datetime,'30-4-19 ',5))),
		   (3,(convert(datetime,'15-4-19 ',5))),
		   (3,(convert(datetime,'15-5-19 ',5))),
		   (3,(convert(datetime,'23-5-19 ',5))),
		   (4,(convert(datetime,'25-5-19 ',5)))
GO

INSERT INTO [dbo].[Ementa_Receita]
           ([id_receita]
           ,[id_ementa])
     VALUES
           (1,1),
           (2,1),
           (3,1),
           (1,2),
           (1,3),
           (2,4),
		   (1,5),
           (2,5),
           (3,5),
           (3,7),
           (2,7),
           (2,6),
           (1,6)
GO
