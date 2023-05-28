CREATE PROCEDURE [dbo].[spUpdateEmployee]
(
@Id int,
@ContactNo numeric(11,0),
@EmailAddress varchar(max)
)
AS
BEGIN
	UPDATE Employee SET ContactNo = @ContactNo, EmailAddress = @EmailAddress 
	WHERE Id = @Id;
END