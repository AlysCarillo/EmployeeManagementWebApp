CREATE PROCEDURE spDeleteEmployeeById
(
	@Id int,
	@ReturnMessage varchar(50) OUTPUT
)
AS 
BEGIN
	BEGIN TRY
		BEGIN TRAN 
			DELETE FROM Employee
			WHERE Id = @Id
			SET @ReturnMessage = 'Employee deleted successfully!'
		COMMIT TRAN
	END TRY
BEGIN CATCH
	ROLLBACK TRAN 
	SET @ReturnMessage = ERROR_MESSAGE()
END CATCH
END