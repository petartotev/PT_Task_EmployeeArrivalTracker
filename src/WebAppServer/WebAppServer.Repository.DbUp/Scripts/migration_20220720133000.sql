CREATE TABLE [dbo].[TeamsEmployees](
[Id] [int] NOT NULL IDENTITY (1,1),
[TeamId] [int] NOT NULL,
[EmployeeId] [int] NOT NULL,
CONSTRAINT [PK_TeamsEmployees_Id] PRIMARY KEY CLUSTERED ([Id] ASC))

ALTER TABLE [dbo].[TeamsEmployees] 
WITH CHECK ADD CONSTRAINT [FK_TeamsEmployees_Employees_EmployeeId] 
FOREIGN KEY ([EmployeeId]) 
REFERENCES [dbo].[Employees] ([Id])

ALTER TABLE [dbo].[TeamsEmployees] 
WITH CHECK ADD CONSTRAINT [FK_TeamsEmployees_Teams_TeamId] 
FOREIGN KEY ([TeamId]) 
REFERENCES [dbo].[Teams] ([Id])