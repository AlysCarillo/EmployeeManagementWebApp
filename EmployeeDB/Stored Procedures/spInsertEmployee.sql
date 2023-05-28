CREATE PROCEDURE spInsertEmployee
    @EmpNo VARCHAR(6),
    @FirstName VARCHAR(15),
    @LastName VARCHAR(15),
    @Birthdate DATE,
    @ContactNo NUMERIC(11),
    @EmailAddress VARCHAR(255)
AS
BEGIN
     IF EXISTS (SELECT 1 FROM Employee WHERE EmpNo = @EmpNo)
    BEGIN
        RAISERROR('Employee with the specified EmpNo already exists.', 16, 1);
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM Employee WHERE FirstName = @FirstName AND LastName = @LastName)
    BEGIN
        RAISERROR('Duplicate combination of FirstName and LastName is not allowed.', 16, 1);
        RETURN;
    END
	BEGIN TRY
		BEGIN TRAN
			INSERT INTO Employee (EmpNo, FirstName, LastName, Birthdate, ContactNo, EmailAddress)
			VALUES (@EmpNo, @FirstName, @LastName, @Birthdate, @ContactNo, @EmailAddress)
		COMMIT TRAN
	END TRY
BEGIN CATCH
	ROLLBACK TRAN
	SELECT ERROR_MESSAGE()
END CATCH
END