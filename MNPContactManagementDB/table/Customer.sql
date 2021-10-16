CREATE TABLE [dbo].[Customer]
(
	[CustomerID] INT NOT NULL  IDENTITY, 
    [CustomerName] VARCHAR(100) NOT NULL, 
    CONSTRAINT [PK_Customer] PRIMARY KEY ([CustomerID])
)
