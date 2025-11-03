# Library Management SQL Design

## Table Definitions

### Books

``` sql
CREATE TABLE Books (
    BookId INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(250) NOT NULL,
    Author NVARCHAR(200) NULL,
    ISBN NVARCHAR(20) NOT NULL UNIQUE,
    PublishedYear INT NULL,
    AvailableCopies INT NOT NULL CHECK (AvailableCopies >= 0)
);
```

### Members

``` sql
CREATE TABLE Members (
    MemberId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL,
    Email NVARCHAR(200) NOT NULL UNIQUE,
    Phone NVARCHAR(30) NULL,
    JoinDate DATETIME NOT NULL DEFAULT GETDATE()
);
```

### BorrowRecords

``` sql
CREATE TABLE BorrowRecords (
    BorrowId INT IDENTITY(1,1) PRIMARY KEY,
    MemberId INT NOT NULL FOREIGN KEY REFERENCES Members(MemberId),
    BookId INT NOT NULL FOREIGN KEY REFERENCES Books(BookId),
    BorrowDate DATETIME NOT NULL DEFAULT GETDATE(),
    ReturnDate DATETIME NULL,
    IsReturned BIT NOT NULL DEFAULT 0
);
```

## SQL Queries

### Top 5 most borrowed books

``` sql
SELECT TOP 5
    b.BookId,
    b.Title,
    b.Author,
    COUNT(br.BorrowId) AS BorrowCount
FROM BorrowRecords br
JOIN Books b ON br.BookId = b.BookId
GROUP BY b.BookId, b.Title, b.Author
ORDER BY COUNT(br.BorrowId) DESC;
```

### Members who borrowed more than 3 books in last month

``` sql
SELECT m.MemberId, m.Name, m.Email, COUNT(br.BorrowId) AS BorrowCount
FROM Members m
JOIN BorrowRecords br ON m.MemberId = br.MemberId
WHERE br.BorrowDate >= DATEADD(MONTH, -1, GETDATE())
GROUP BY m.MemberId, m.Name, m.Email
HAVING COUNT(br.BorrowId) > 3;
```

### Stored procedure to get overdue books

``` sql
CREATE PROCEDURE GetOverdueBooks
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        br.BorrowId,
        br.MemberId,
        m.Name AS MemberName,
        m.Email AS MemberEmail,
        br.BookId,
        b.Title AS BookTitle,
        br.BorrowDate,
        DATEADD(DAY, 14, br.BorrowDate) AS DueDate,
        DATEDIFF(DAY, DATEADD(DAY, 14, br.BorrowDate), GETDATE()) AS DaysOverdue
    FROM BorrowRecords br
    JOIN Members m ON br.MemberId = m.MemberId
    JOIN Books b ON br.BookId = b.BookId
    WHERE br.IsReturned = 0
      AND DATEADD(DAY, 14, br.BorrowDate) < GETDATE()
    ORDER BY DaysOverdue DESC;
END
```

### Stored procedure to get books borrowed by a member

``` sql
CREATE PROCEDURE GetBooksBorrowedByMember
    @MemberId INT
AS
BEGIN
    SELECT
        br.BorrowId,
        br.BookId,
        b.Title,
        b.Author,
        br.BorrowDate,
        br.ReturnDate,
        br.IsReturned
    FROM BorrowRecords br
    JOIN Books b ON br.BookId = b.BookId
    WHERE br.MemberId = @MemberId AND br.IsReturned = 0;
END
```
