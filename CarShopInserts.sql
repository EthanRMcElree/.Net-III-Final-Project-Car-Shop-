
USE [CarShop]
GO

print '' print '*** inserting employee test records ***'
GO
INSERT INTO [dbo].[Employee]
		([FirstName], [LastName], [PhoneNumber], [Email], [Role])
	VALUES
		('John', 'Smith', '3197771111', 'john@company.com', 'manager'),
		('Larry', 'Sky', '3197772222', 'larry@company.com', 'sales'),
		('Sam', 'Williams', '3197773333', 'sam@company.com', 'sales'),
		('Porsha', 'Perez', '3197774444', 'porsha@company.com', 'admin'),
		('Jack', 'Rawi', '3197775555', 'jack@company.com', 'admin')
GO

print '' print '*** inserting carinventory test records ***'
GO
INSERT INTO [dbo].[CarInventory]
		([Model], [Year], [Color], [VIN], [Price], [FuelType], [TransmissionType], [EngineSize], [Description])
	VALUES
		('crosstrek', 2023, 'black', '4HDAS138', 24995.00, 'Gasoline', 'automatic', 2.5, 'do not miss out on this car'),
		('forester', 2024, 'blue', '9F1J2AG9', 25800.00, 'E85', 'manual', 2.5, 'another great car'),
		('ascent', 2024, 'red', '1A8HWE40', 27550.00, 'Hybrid', 'automatic', 2.5, 'awesome car'),
		('Semi', 2021, 'White', '7A80US3J', 30150.00, 'Diesel', 'manual', 3.5, 'big truck'),
		('Ferari', 2018, 'Red', 'J7KZ11L9', 50500.00, 'Ethanol', 'automatic', 2.5, 'Very rare awesome car')
GO

print '' print '*** inserting customer test records ***'
GO
INSERT INTO [dbo].[Customer]
		([FirstName], [LastName], [Email], [PhoneNumber])
	VALUES
		('John', 'Doe', 'john@email.com', '3191012222'),
		('Kate', 'Hillard', 'kate@email.com', '3191023333'),
		('Sam', 'Spacer', 'sam@email.com', '3191034444')
GO

print '' print '*** inserting sales test records ***'
GO
INSERT INTO [dbo].[Sales]
		([CarID], [EmployeeID], [CustomerID], [SaleDate], [SalePrice])
	VALUES
		(1, 1, 2, '2023-07-09', 22500.00),
		(2, 3, 3, '2022-09-12', 19295.50),
		(3, 2, 1, '2021-04-19', 20750.00)		
GO

print '' print '*** inserting manufacturer test records ***'
GO
INSERT INTO [dbo].[Manufacturer]
		([ManufacturerName], [CountryOrigin])
	VALUES
		('Volvo', 'Sweden'),
		('Volkswagen', 'Germany'),
		('Toyota', 'Japan'),
		('Ford', 'United States'),
		('Subaru', 'Japan')
GO

print '' print '*** inserting Supplier test records ***'
GO
INSERT INTO [dbo].[Supplier]
		([SupplierName], [ContactPerson], [PhoneNumber])
	VALUES
		('Down Town Auto Service', 'Freddy Brown', '3192221111'),
		('Cedar Rapids Auto Service', 'Shawn Torent', '3192223333'),
		('Iowa City Auto Service', 'Patrick Sky', '3192224444'),
		('Davenport Auto Service', 'Mavis Corres', '3192225555'),
		('New York Auto Service', 'Arin Pace', '3192227777'),
		('Los Angeles Auto Service', 'Dawn Horde', '3192228888')
GO

print '' print '*** inserting servicetype test records ***'
GO
INSERT INTO [dbo].[ServiceType]
		([ServiceTypeID], [Description])
	VALUES
		(1, 'repairs'),
		(2, 'tires'),
		(3, 'oil change'),
		(4, 'alignment'),
		(5, 'brakes')
GO

print '' print '*** inserting ServiceAppointment test records ***'
GO
INSERT INTO [dbo].[ServiceAppointment]
		([CarID], [CustomerID], [ServiceTypeID])
	VALUES
		(2, 1, 1),
		(2, 2, 2),
		(3, 1, 3),
		(3, 3, 4),
		(1, 3, 5)
GO

print '' print '*** inserting warrantytype test records ***'
GO
INSERT INTO [dbo].[WarrantyType]
		([WarrantyTypeID], [Description])
	VALUES
		(1, 'bumper to bumper'),
		(2, 'powertrain'),
		(3, 'corrosion'),
		(4, 'emissions'),
		(5, 'accessories')
GO

print '' print '*** inserting Warranty test records ***'
GO
INSERT INTO [dbo].[Warranty]
		([CarID], [WarrantyTypeID])
	VALUES
		(3, 1),
		(1, 4),
		(3, 2),
		(2, 3),
		(2, 5),
		(1, 2)
GO

print '' print '*** inserting RepairInvoice test records ***'
GO
INSERT INTO [dbo].[RepairInvoice]
		([CarID], [EmployeeID], [IssueDescription])
	VALUES
		(1, 1, "Replace oxygen sensor"),
		(3, 3, "Replace catalytic converter"),
		(1, 5, "Replace ignition coil"),
		(2, 2, "Replace spark plug"),
		(2, 4, "Replace spark plug wires"),
		(3, 5, "Replace mass air flow sensor")
GO

print '' print '*** inserting Financing test records ***'
GO
INSERT INTO [dbo].[Financing]
		([SaleID], [LoanProvider], [LoanAmount], [InterestRate])
	VALUES		
		(3, "EliteCash Services", 450.00, .0811),		
		(2, "LibertyLoans", 325.00, .0450),
		(1, "PrestoFunds", 400.50, .0550)
GO
