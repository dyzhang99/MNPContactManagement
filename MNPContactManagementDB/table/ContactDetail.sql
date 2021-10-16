CREATE TABLE [dbo].[ContactDetail]
(
	[ContactID] INT NOT NULL  IDENTITY, 
    [CustomerID] INT NOT NULL, 
    [ContactName] VARCHAR(100) NOT NULL, 
    [JobTitle] VARCHAR(50) NOT NULL, 
    [Address] VARCHAR(200) NOT NULL, 
    [Phone] VARCHAR(50) NULL, 
    [EmailAddress] VARCHAR(150) NULL, 
    [Comments] VARCHAR(500) NULL, 
    [LastDateContacted] DATETIMEOFFSET NOT NULL, 
    CONSTRAINT [FK_ContactDetail_Customer] FOREIGN KEY (CustomerID) REFERENCES [Customer]([CustomerID]), 
    CONSTRAINT [PK_ContactDetail] PRIMARY KEY ([ContactID]),  
)
