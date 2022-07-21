CREATE TABLE [dbo].[Arrivals](
[Id] [int] NOT NULL IDENTITY (1,1),
[EmployeeId] [int] NOT NULL,
[DateArrival] [datetime] NOT NULL,
CONSTRAINT [PK_Arrivals_Id] PRIMARY KEY CLUSTERED ([Id] ASC))

ALTER TABLE [dbo].[Arrivals] 
WITH CHECK ADD CONSTRAINT [FK_Arrivals_Employees_EmployeeId] 
FOREIGN KEY ([EmployeeId]) 
REFERENCES [dbo].[Employees] ([Id])