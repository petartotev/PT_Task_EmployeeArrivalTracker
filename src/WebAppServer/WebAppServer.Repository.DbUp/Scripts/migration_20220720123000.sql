CREATE TABLE [dbo].[Employees](
[Id] [int] NOT NULL IDENTITY (1,1),
[FirstName] [nvarchar](50) NOT NULL,
[LastName] [nvarchar](50) NOT NULL,
[Email] [nvarchar](100) NOT NULL,
[DateBirth] [datetime] NOT NULL,
[RoleId] [int] NOT NULL,
[ManagerId] [int] NULL REFERENCES Employees(Id),
CONSTRAINT [PK_Employees_Id] PRIMARY KEY CLUSTERED ([Id] ASC))

ALTER TABLE [dbo].[Employees] 
WITH CHECK ADD CONSTRAINT [FK_Employees_Roles_RoleId] 
FOREIGN KEY ([RoleId]) 
REFERENCES [dbo].[Roles] ([Id])