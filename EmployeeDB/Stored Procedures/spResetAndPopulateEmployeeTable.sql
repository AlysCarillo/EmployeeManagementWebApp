CREATE PROCEDURE spResetAndPopulateEmployeeTable
AS
BEGIN
    IF OBJECT_Id('Employee', 'U') IS NOT NULL
    BEGIN
        -- Employee table exists, purge all data
        DELETE FROM Employee;
    END
    ELSE
    BEGIN
        -- Employee table does not exist, create it
        CREATE TABLE Employee (
            Id INT PRIMARY KEY,
            EmpNo VARCHAR(6) UNIQUE,
            FirstName VARCHAR(15),
            LastName VARCHAR(15),
            Birthdate DATE,
            ContactNo NUMERIC(11),
            EmailAddress VARCHAR(255)
        );
    END

    -- Insert sample data into the Employee table
    INSERT INTO Employee (EmpNo, FirstName, LastName, Birthdate, ContactNo, EmailAddress)
    VALUES
        ('E00001', 'John', 'Doe', '1990-05-15', 12345678901, 'john.doe@example.com'),
        ('E00002', 'Jane', 'Smith', '1995-08-21', 23456789012, 'jane.smith@example.com'),
        ('E00003', 'Michael', 'Johnson', '1988-12-10', 34567890123, 'michael.johnson@example.com'),
        ('E00004', 'Sarah', 'Williams', '1992-03-27', 45678901234, 'sarah.williams@example.com'),
        ('E00005', 'Robert', 'Brown', '1985-09-02', 56789012345, 'robert.brown@example.com'),
        ('E00006', 'Emily', 'Davis', '1998-06-18', 67890123456, 'emily.davis@example.com'),
        ('E00007', 'Daniel', 'Taylor', '1993-11-14', 78901234567, 'daniel.taylor@example.com'),
        ('E00008', 'Olivia', 'Anderson', '1991-02-25', 89012345678, 'olivia.anderson@example.com'),
        ('E00009', 'Matthew', 'Wilson', '1987-07-09', 90123456789, 'matthew.wilson@example.com'),
        ('E00010', 'Sophia', 'Walker', '1994-04-30', 12345678901, 'sophia.walker@example.com');
END
