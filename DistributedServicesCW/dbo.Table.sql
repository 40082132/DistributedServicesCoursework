CREATE TABLE [dbo].[Table]
(
	[Username] VARCHAR(50) NOT NULL PRIMARY KEY, 
    [Password] NVARCHAR(MAX) NOT NULL, 
    [Email] VARCHAR(50) NOT NULL, 
    [First_Name] NVARCHAR(50) NOT NULL, 
    [Last_Name] NVARCHAR(MAX) NOT NULL
)
