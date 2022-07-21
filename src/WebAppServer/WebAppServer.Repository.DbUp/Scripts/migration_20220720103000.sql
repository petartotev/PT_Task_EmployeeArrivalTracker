CREATE TABLE [dbo].[Roles](
[Id] [int] NOT NULL IDENTITY (1,1),
[Name] [nvarchar](100) NOT NULL,
CONSTRAINT [PK_Roles_Id] PRIMARY KEY CLUSTERED ([Id] ASC))

IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE type = 'UQ' AND name = 'RoleName')
   BEGIN
        ALTER TABLE [dbo].[Roles]
        ADD CONSTRAINT [RoleName] UNIQUE NONCLUSTERED ([Name])
   END