USE [Olivia]
GO


INSERT INTO [dbo].[Ingredient]
           ([Name]
           ,[Category],
		   [Active])
     VALUES
           ('Ovos','Ovos',1),
           ('Enguia','Pescado',1),
           ('Broculos','Horticolas',1),
           ('Bacalhau','Pescado',1),
           ('Cogumelos','Horticolas',1),
           ('Espinafres','Horticolas',1),
           ('Bife de Peru','Carne',1),
           ('Salmao','Pescado',1),
           ('Maca','Fruta',1),
           ('Kiwi','Fruta',1),
           ('Feijao Preto','Leguminosas',1),
           ('Arroz','Acompanhamento',1),
           ('Massa','Acompanhamento',1),
		   ('Atum','Pescado',1),
		   ('Salsichas','Carne',1),
		   ('Agua','Agua',1)
GO

INSERT INTO [dbo].[User]
           ([Username]
           ,[Password]
           ,[Email]
           ,[Type]
           ,[Preference]
           ,[Name],
		   [Active])
     VALUES
           ('Megadeus','superseguro2000','62random@gmail.com',1,0,'varchar[megadeus]',1),
           ('Randao','1','62random@hotmail.com',1,0,'Gajo aleatorio',1),
           ('Amigo da Dora','2','62random@iol.pt',1,0,'Amor para a vida toda',1),
           ('Nimbus2000','3','62random@live.com.pt',1,0,'Apetece-me algo',1)
GO
	


INSERT INTO [dbo].[Recipe]
           ([Name]
           ,[Description]
           ,[Creator]
           ,[Type]
           ,[Calories]
           ,[Fat]
           ,[Carbs]
           ,[Protein]
           ,[Fiber]
           ,[Sodium],[Active])
     VALUES
           ('Massa com Atum','Massa com Atum, exelente para quem precisa de uma refeicao rapida e saudavel',1,1,359,370,23,0.1,0.0001,5.1,1),
           ('Sopa de espinafres','Sopa extremamente saudavel',1,1,0,0,0,100000,0.0001,5.1,1),
           ('Punheta de Bacalhau','Name auto explicativo',2,1,12,75,23,0.1,0.23,5.12,1)
GO

SET DATEFORMAT dmy;  
INSERT INTO [dbo].[History]
           ([Id_User]
           ,[Id_Recipe]
           ,[Date])
     VALUES
           (1,1,(convert(datetime,'13-11-18 ',5))),           
           (2,2,(convert(datetime,'14-10-18 ',5))),
           (1,2,(convert(datetime,'17-3-19 ',5))),
           (3,1,(convert(datetime,'5-4-19 ',5))),
           (4,1,(convert(datetime,'16-5-19 ',5))),
           (4,2,(convert(datetime,'26-4-19 ',5)))
GO

INSERT INTO [dbo].[Rating]
           ([Id_User]
           ,[Id_Recipe]
           ,[Rating])
     VALUES
           (1,1,3),           
           (2,2,1),
           (1,2,2),
           (4,1,5),
           (4,2,3)
GO

INSERT INTO [dbo].[Favorite]
           ([User_key]
           ,[Recipe_key])
     VALUES
           (1,1),
           (1,2),
           (1,3),
           (2,2),
           (3,2)
GO


INSERT INTO [dbo].[Recipe_Ingredient]
           ([Id_Recipe]
           ,[Id_Ingredient]
           ,[Quantity]
           ,[Unit])
     VALUES
		   (1,13,500,'Gramas'),
		   (1,14,1,'Embalagem'),
		   (2,16,500,'MiliLitros'),
		   (2,3,200,'Gramas'),
		   (2,6,500,'Gramas'),
		   (3,1,2,'KG')
GO

INSERT INTO [dbo].[Instruction]
           ([Designation]
           ,[Duration]
           ,[Position]
           ,[Id_Recipe])
     VALUES
		   ('Fazer Arroz', 10, 1,1),
		   ('Meter Atum em cima', 1, 2,1),
		   ('Meter Agua numa panela e ferver', 5, 1,2),
		   ('Meter espinafres e broculos na agua e esperar 15 minutos', 15, 2,2),
		   ('Bruxaria Extrema, apenas acreditar', 30, 1,3)
GO


INSERT INTO [dbo].[Menu]
           ([id_user]
           ,[Date])
     VALUES
		   (1,(convert(datetime,'13-11-18 ',5))),
		   (1,(convert(datetime,'22-11-18 ',5))),
		   (2,(convert(datetime,'30-4-19 ',5))),
		   (3,(convert(datetime,'15-4-19 ',5))),
		   (3,(convert(datetime,'15-5-19 ',5))),
		   (3,(convert(datetime,'23-5-19 ',5))),
		   (4,(convert(datetime,'25-5-19 ',5)))
GO

INSERT INTO [dbo].[Menu_Recipe]
           ([Id_Recipe]
           ,[Id_Menu])
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
