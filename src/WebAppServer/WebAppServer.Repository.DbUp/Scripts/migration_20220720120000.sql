CREATE TABLE [dbo].[Teams](
[Id] [int] NOT NULL IDENTITY (1,1),
[Name] [nvarchar](50) NOT NULL,
CONSTRAINT [PK_Teams_Id] PRIMARY KEY CLUSTERED ([Id] ASC))

IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE type = 'UQ' AND name = 'TeamName')
   BEGIN
        ALTER TABLE [dbo].[Teams]
        ADD CONSTRAINT [TeamName] UNIQUE NONCLUSTERED ([Name])
   END