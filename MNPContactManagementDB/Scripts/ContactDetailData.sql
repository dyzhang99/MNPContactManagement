
USE MNPContactManagement
GO 
INSERT INTO ContactDetail(CustomerID, ContactName, JobTitle, Address, Phone, EmailAddress,Comments,LastDateContacted)
VALUES(1, 'Ray Sin', 'Region Manager', '1 Yonge St, Toronto, ON', '1416451245', 'Ray.Sin@bell.ca', 'Good Morning', SYSDATETIMEOFFSET()),
  (1, 'Rita Book', 'Support Centre Manager', '1 Yonge St, Toronto, ON', '1416451666', 'Rita.Book@bell.ca', 'Good Afternoon', SYSDATETIMEOFFSET()),
  (2, 'Paige Turner', 'VP Operation', '31 King St, Toronto, ON', '1416355006', 'Paige.Turner@rogers.com', 'Hare Are You!', SYSDATETIMEOFFSET()),
  (4, 'Eileen Sideways', 'VP Sales', '100 Queen St, Toronto, ON', '14168547814', 'Eileen.Sideways@shaw.com', 'What a day', SYSDATETIMEOFFSET());


   