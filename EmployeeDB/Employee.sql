CREATE TABLE [dbo].[Employee]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [EmpNo] VARCHAR(6) UNIQUE NOT NULL, 
    [FirstName] VARCHAR(15) NOT NULL, 
    [LastName] VARCHAR(15) NOT NULL, 
    [Birthdate] DATE NOT NULL, 
    [ContactNo] NUMERIC(11) NULL, 
    [EmailAddress] VARCHAR(MAX) NULL
)
