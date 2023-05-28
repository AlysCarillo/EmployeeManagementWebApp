CREATE PROCEDURE [dbo].[spGetEmployeeByID]
(
@Id int
)
AS 
BEGIN 
	SELECT * FROM Employee
	Where Id = @Id;
END