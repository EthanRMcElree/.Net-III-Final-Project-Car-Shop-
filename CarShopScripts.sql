/* check to see if the database exists, if so, drop it */
IF EXISTS(SELECT 1 FROM master.dbo.sysdatabases
			WHERE name = 'CarShop')
BEGIN
	DROP DATABASE CarShop	
	print '' print '*** dropping database CarShop ***'
END
GO

print '' print '*** creating databse CarShop ***'
GO
CREATE DATABASE CarShop
GO

print '' print '*** using database CarShop ***'
GO
USE [CarShop]
GO

/* Employee Table */
print '' print '*** creating employee table ***'
GO
CREATE TABLE [dbo].[Employee] (
	[EmployeeID] 	[int] IDENTITY(1, 1) 	NOT NULL,
	[FirstName] 	[nvarchar] (50)				NOT NULL,
	[LastName] 		[nvarchar] (50)				NOT NULL,
	[Password] 		[nvarchar] (100)			NOT NULL DEFAULT
		'ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae',
	[PhoneNumber] 	[nvarchar] (11)				NOT NULL,
	[Email] 		[nvarchar] (250) 			NOT NULL,
	[Role] 			[nvarchar] (100)			NOT NULL,
	CONSTRAINT [pk_EmployeeID] PRIMARY KEY([EmployeeID]),
	CONSTRAINT [ak_Email] UNIQUE([Email])
)
GO

/* CarInventory Table */
print '' print '*** creating carinventory table ***'
GO
CREATE TABLE [dbo].[CarInventory] (
	[CarID]					[int] IDENTITY(1, 1) 	NOT NULL,
	[Model]					[nvarchar] (50) 		NOT NULL,
	[Year]					[int]  					NOT NULL,
	[Color]					[nvarchar] (50) 		NOT NULL,
	[VIN]					[nvarchar] (17) 		NOT NULL,
	[Price]					[float] 		 		NOT NULL,
	[Mileage]				[int]  					DEFAULT 0,
	[FuelType]				[nvarchar] (50) 		NOT NULL,
	[TransmissionType]		[nvarchar] (50) 		NOT NULL,
	[EngineSize]			[float]  				NOT NULL,
	[Description]			[nvarchar] (250) 		NOT NULL,
	CONSTRAINT [pk_CarID] PRIMARY KEY([CarID]),
	CONSTRAINT [ak_VIN] UNIQUE([VIN])
)
GO

/* Customer Table */
print '' print '*** creating customer table ***'
GO
CREATE TABLE [dbo].[Customer] (
	[CustomerID]			[int] IDENTITY(1, 1) 	NOT NULL,
	[FirstName]				[nvarchar] (50) 		NOT NULL,
	[LastName]				[nvarchar] (50) 		NOT NULL,
	[Email]					[nvarchar] (250) 		NOT NULL,
	[Password]				[nvarchar] (100) 		NOT NULL DEFAULT
	'2597a7caf656e89e9ab35e12326d557ebfe9b7b5dcbe4c564e74070fa5cfcbe5', 
	[PhoneNumber]			[nvarchar] (11) 		NOT NULL,
	CONSTRAINT [pk_CustomerID] PRIMARY KEY([CustomerID])
)
GO

/* Sales Table */
print '' print '*** creating sales table ***'
GO
CREATE TABLE [dbo].[Sales] (
	[SaleID]				[int] IDENTITY(1, 1) 	NOT NULL,
	[EmployeeID]			[int]					NOT NULL,
	[CarID]					[int] 			 		NOT NULL,
	[CustomerID]			[int] 					NOT NULL,
	[SaleDate]				[datetime],
	[SalePrice]				[float],
	CONSTRAINT [fk_Sales_CarID] FOREIGN KEY([CarID])
		REFERENCES [dbo].[CarInventory]([CarID]),
	CONSTRAINT [fk_Sales_EmployeeID] FOREIGN KEY([EmployeeID])
		REFERENCES [dbo].[Employee]([EmployeeID]),
	CONSTRAINT [fk_Sales_CustomerID] FOREIGN KEY([CustomerID])
		REFERENCES [dbo].[Customer]([CustomerID]),
	CONSTRAINT [pk_SaleID] PRIMARY KEY([SaleID])	
)
GO

/* Manufacturer Table */
print '' print '*** creating manufacturer table ***'
GO
CREATE TABLE [dbo].[Manufacturer] (
	[ManufacturerID]		[int] IDENTITY(1, 1) 				NOT NULL,
	[ManufacturerName]		[nvarchar] (50) 					NOT NULL,
	[CountryOrigin]			[nvarchar] (50) 					NOT NULL,
	CONSTRAINT [pk_ManufacturerID] PRIMARY KEY([ManufacturerID])
)
GO

/* Supplier Table */
print '' print '*** creating Supplier table ***'
GO
CREATE TABLE [dbo].[Supplier] (
	[SupplierID]			[int] IDENTITY(1, 1) 				NOT NULL,
	[SupplierName]			[nvarchar] (50) 					NOT NULL,
	[ContactPerson]			[nvarchar] (50) 					NOT NULL,
	[PhoneNumber]			[nvarchar] (11) 					NOT NULL,
	CONSTRAINT [pk_SupplierID] PRIMARY KEY([SupplierID]),
	CONSTRAINT [ak_SupplierName] UNIQUE([SupplierName])
)
GO

/* ServiceType Table */
print '' print '*** creating servicetype table ***'
GO
CREATE TABLE [dbo].[ServiceType] (
	[ServiceTypeID]			[int] 						NOT NULL,
	[Description]			[nvarchar] (250)			NOT NULL,
	CONSTRAINT [pk_ServiceTypeID] PRIMARY KEY([ServiceTypeID])
)
GO

/* ServiceAppointment Table */
print '' print '*** creating serviceappointment table ***'
GO
CREATE TABLE [dbo].[ServiceAppointment] (
	[AppointmentID]			[int] IDENTITY(1, 1) 				NOT NULL,
	[CarID]					[int]  								NOT NULL,
	[CustomerID]			[int] 								NOT NULL,
	[ServiceTypeID]			[int]  								NOT NULL,
	[SupplierID]			[int],
	[ScheduleDate]			[datetime],
	CONSTRAINT [fk_ServiceAppointment_CarID] FOREIGN KEY([CarID])
		REFERENCES [dbo].[CarInventory]([CarID]),
	CONSTRAINT [fk_ServiceAppointment_CustomerID] FOREIGN KEY([CustomerID])
		REFERENCES [dbo].[Customer]([CustomerID]),
	CONSTRAINT [fk_ServiceAppointment_ServiceTypeID] FOREIGN KEY([ServiceTypeID])
		REFERENCES [dbo].[ServiceType]([ServiceTypeID]),
	CONSTRAINT [fk_ServiceAppointment_SupplierID] FOREIGN KEY([SupplierID])
		REFERENCES [dbo].[Supplier]([SupplierID]),
	CONSTRAINT [pk_AppointmentID] PRIMARY KEY([AppointmentID])
)
GO

/* WarrantyType Table */
print '' print '*** creating warrantytype table ***'
GO
CREATE TABLE [dbo].[WarrantyType] (
	[WarrantyTypeID]		[int] 						NOT NULL,
	[Description]			[nvarchar] (250)			NOT NULL,
	CONSTRAINT [pk_WarrantyTypeID] PRIMARY KEY([WarrantyTypeID])
)
GO

/* Warranty Table */
print '' print '*** creating Warranty table ***'
GO
CREATE TABLE [dbo].[Warranty] (
	[WarrantyID]			[int] IDENTITY(1, 1) 				NOT NULL,
	[CarID]					[int]  								NOT NULL,
	[WarrantyTypeID]		[int] 								NOT NULL,
	[WarrantyStartDate]		[datetime],
	[WarrantyEndDate]		[datetime],
	CONSTRAINT [fk_Warranty_CarID] FOREIGN KEY([CarID])
		REFERENCES [dbo].[CarInventory]([CarID]),
	CONSTRAINT [fk_Warranty_WarrantyTypeID] FOREIGN KEY([WarrantyTypeID])
		REFERENCES [dbo].[WarrantyType]([WarrantyTypeID]),
	CONSTRAINT [pk_WarrantyID] PRIMARY KEY([WarrantyID])
)
GO

/* RepairInvoice Table */
print '' print '*** creating RepairInvoice table ***'
GO
CREATE TABLE [dbo].[RepairInvoice] (
	[InvoiceID]				[int] IDENTITY(1, 1) 				NOT NULL,
	[CarID]					[int] 								NOT NULL,
	[EmployeeID]			[int] 								NOT NULL,
	[IssueDescription]		[nvarchar] (250) 					NOT NULL,
	[RepairDate]			[datetime],
	CONSTRAINT [fk_RepairInvoice_CarID] FOREIGN KEY([CarID])
		REFERENCES [dbo].[CarInventory]([CarID]),
	CONSTRAINT [fk_RepairInvoice_EmployeeID] FOREIGN KEY([EmployeeID])
		REFERENCES [dbo].[Employee]([EmployeeID]),
	CONSTRAINT [pk_InvoiceID] PRIMARY KEY([InvoiceID]),
)
GO

/* Financing Table */
print '' print '*** creating Financing table ***'
GO
CREATE TABLE [dbo].[Financing] (
	[FinancingID]			[int] IDENTITY(1, 1) 				NOT NULL,
	[SaleID]				[int] 								NOT NULL,
	[LoanProvider]			[nvarchar] (50) 					NOT NULL,
	[LoanAmount]			[float]  							NOT NULL,
	[InterestRate]			[float]							NOT NULL,
	CONSTRAINT [fk_Financing_SaleID] FOREIGN KEY([SaleID])
		REFERENCES [dbo].[Sales]([SaleID]),
	CONSTRAINT [pk_FinancingID] PRIMARY KEY([FinancingID])
)
GO

/* stored procedures */

print '' print '*** creating sp_create_employee_account ***'
GO
CREATE PROCEDURE [dbo].[sp_create_employee_account]
(
	@FirstName		[nvarchar] (50),
	@LastName		[nvarchar] (50),
	@Password		[nvarchar] (100),
	@PhoneNumber	[nvarchar] (11),
	@Email			[nvarchar] (250),
	@Role			[nvarchar] (100)
)
AS
	BEGIN
		INSERT INTO [dbo].[Employee] ([FirstName], [LastName], [Password], [PhoneNumber], [Email], [Role])
		VALUES (@FirstName, @LastName, @Password, @PhoneNumber, @Email, @Role)
	END
GO

print '' print '*** creating sp_create_customer_account ***'
GO
CREATE PROCEDURE [dbo].[sp_create_customer_account]
(
	@FirstName		[nvarchar] (50),
	@LastName		[nvarchar] (50),
	@Password		[nvarchar] (100),
	@PhoneNumber	[nvarchar] (11),
	@Email			[nvarchar] (250)
)
AS
	BEGIN
		INSERT INTO [dbo].[Customer] ([FirstName], [LastName], [Password], [PhoneNumber], [Email])
		VALUES (@FirstName, @LastName, @Password, @PhoneNumber, @Email)
	END
GO

print '' print '*** creating sp_select_customer_by_id ***'
GO
CREATE PROCEDURE [dbo].[sp_select_customer_by_id]
(
	@CustomerID			[int]
)
AS
	BEGIN
		SELECT * FROM [dbo].[Customer] WHERE Customer.CustomerID = @CustomerID
	END
GO

print '' print '*** creating sp_select_customer_by_email ***'
GO
CREATE PROCEDURE [dbo].[sp_select_customer_by_email]
(
	@Email			[nvarchar] (250)
)
AS
	BEGIN
		SELECT [CustomerID], [FirstName], [LastName], [Password], [PhoneNumber], [Email]
		FROM [Customer]
		WHERE @Email = [Email]
	END
GO

print '' print '*** creating sp_authenticate_employee ***'
GO
CREATE PROCEDURE [dbo].[sp_authenticate_employee]
(
	@Password		[nvarchar] (100),
	@Email			[nvarchar] (250)
)
AS
	BEGIN
		SELECT [EmployeeID], [FirstName], [LastName], [Password], [PhoneNumber], [Email], [Role] 
		FROM [Employee]
		WHERE @Password = [Password]
		AND @Email = [Email]
	END
GO

print '' print '*** creating sp_authenticate_customer ***'
GO
CREATE PROCEDURE [dbo].[sp_authenticate_customer]
(
	@Password		[nvarchar] (100),
	@Email			[nvarchar] (250)
)
AS
	BEGIN
		SELECT [CustomerID], [FirstName], [LastName], [Password], [PhoneNumber], [Email]
		FROM [Customer]
		WHERE @Password = [Password]
		AND @Email = [Email]
	END
GO

print '' print '*** creating sp_select_employee_by_id ***'
GO
CREATE PROCEDURE [dbo].[sp_select_employee_by_id]
(
	@EmployeeID			[int]
)
AS
	BEGIN
		SELECT [EmployeeID], [FirstName], [LastName], [Password], [PhoneNumber], [Email], [Role] 
		FROM [Employee]
		WHERE @EmployeeID = [EmployeeID]
	END
GO

print '' print '*** creating sp_select_employee_by_email ***'
GO
CREATE PROCEDURE [dbo].[sp_select_employee_by_email]
(
	@Email			[nvarchar] (250)
)
AS
	BEGIN
		SELECT [EmployeeID], [FirstName], [LastName], [Password], [PhoneNumber], [Email], [Role] 
		FROM [Employee]
		WHERE @Email = [Email]
	END
GO

print '' print '*** creating sp_change_employee_password ***'
GO
CREATE PROCEDURE [dbo].[sp_change_employee_password]
(
	@Email			[nvarchar] (250),
	@OldPassword	[nvarchar]	(100),
	@NewPassword	[nvarchar]	(100)
)
AS
	BEGIN		
		UPDATE [dbo].[Employee] SET [Password] = @NewPassword WHERE [Email] = @Email			
	END		
GO

print '' print '*** creating sp_change_customer_password ***'
GO
CREATE PROCEDURE [dbo].[sp_change_customer_password]
(
	@Email			[nvarchar] (250),
	@OldPassword	[nvarchar]	(50),
	@NewPassword	[nvarchar]	(50)
)
AS
	BEGIN
		IF EXISTS (SELECT 1 FROM [dbo].[Customer] WHERE @Email = [Email] AND @OldPassword = [Password])
		BEGIN
			UPDATE [dbo].[Customer] SET [Password] = @NewPassword WHERE [Email] = @Email
			SELECT 'Password has been changed' AS [Result]
		END
		ELSE 
			SELECT 'Password change failed' AS [Result]
	END
GO

print '' print '*** creating sp_reset_employee_password ***'
GO
CREATE PROCEDURE [dbo].[sp_reset_employee_password]
(
	@Email			[nvarchar] (250),
	@NewPassword	[nvarchar]	(100)
)
AS
	BEGIN
		UPDATE [dbo].[Employee] SET [Password] = @NewPassword WHERE [Email] = @Email
	END
GO

print '' print '*** creating sp_reset_customer_password ***'
GO
CREATE PROCEDURE [dbo].[sp_reset_customer_password]
(
	@Email			[nvarchar] (250),
	@NewPassword	[nvarchar]	(100)
)
AS
	BEGIN
		UPDATE [dbo].[Customer] SET [Password] = @NewPassword WHERE [Email] = @Email
	END
GO

print '' print '*** creating sp_delete_employee_account ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_employee_account]
(
	@Email			[nvarchar] (250)
)
AS
	BEGIN
		DELETE FROM [dbo].[Employee] WHERE [Email] = @Email
	END
GO

print '' print '*** creating sp_delete_customer_account ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_customer_account]
(
	@Email			[nvarchar] (250)
)
AS
	BEGIN
		DELETE FROM [dbo].[Customer] WHERE [Email] = @Email
	END
GO

print '' print '*** creating sp_view_car_inventory ***'
GO
CREATE PROCEDURE [dbo].[sp_view_car_inventory]
AS
	BEGIN
		SELECT * FROM [dbo].[CarInventory]
	END
GO

print '' print '*** creating sp_select_car_by_id ***'
GO
CREATE PROCEDURE [dbo].[sp_select_car_by_id]
(
	@CarID		[int]
)
AS
	BEGIN
		SELECT * FROM [dbo].[CarInventory] WHERE CarInventory.CarID = @CarID
	END
GO

print '' print '*** creating sp_filter_car_inventory_by_model ***'
GO
CREATE PROCEDURE [dbo].[sp_filter_car_inventory_by_model]
(
	@Model			[nvarchar] (50)
)
AS
	BEGIN
		SELECT * FROM [dbo].[CarInventory] WHERE [Model] = @Model
	END
GO

print '' print '*** creating sp_filter_car_inventory_by_year ***'
GO
CREATE PROCEDURE [dbo].[sp_filter_car_inventory_by_year]
(
	@Year			[int]
)
AS
	BEGIN
		SELECT * FROM [dbo].[CarInventory] WHERE [Year] = @Year
	END
GO

print '' print '*** creating sp_filter_car_inventory_by_fuel_type ***'
GO
CREATE PROCEDURE [dbo].[sp_filter_car_inventory_by_fuel_type]
(
	@FuelType			[nvarchar] (50)
)
AS
	BEGIN
		SELECT * FROM [dbo].[CarInventory] WHERE [FuelType] = @FuelType
	END
GO

print '' print '*** creating sp_filter_car_inventory_by_high_mileage ***'
GO
CREATE PROCEDURE [dbo].[sp_filter_car_inventory_by_high_mileage]
AS
	BEGIN
		SELECT * FROM [dbo].[CarInventory] WHERE [Mileage] > 75000
	END
GO

print '' print '*** creating sp_filter_car_inventory_by_low_mileage ***'
GO
CREATE PROCEDURE [dbo].[sp_filter_car_inventory_by_low_mileage]
AS
	BEGIN
		SELECT * FROM [dbo].[CarInventory] WHERE [Mileage] < 10000
	END
GO

print '' print '*** creating sp_filter_car_inventory_by_moderate_mileage ***'
GO
CREATE PROCEDURE [dbo].[sp_filter_car_inventory_by_moderate_mileage]
AS
	BEGIN
		SELECT * FROM [dbo].[CarInventory] WHERE [Mileage] > 10000 AND [Mileage] < 75000
	END
GO

print '' print '*** creating sp_view_car_detail ***'
GO
CREATE PROCEDURE [dbo].[sp_view_car_detail]
(
	@CarID		[int]
)
AS
	BEGIN
		SELECT * FROM [dbo].[CarInventory] WHERE [CarID] = @CarID
	END
GO

print '' print '*** creating sp_insert_new_car ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_new_car]
(
	@Model				[nvarchar]	(50),
	@Year				[int],	
	@Color				[nvarchar]	(50),
	@VIN				[nvarchar]	(17),
	@Price				[float],
	@Mileage			[int],	
	@FuelType			[nvarchar]	(50),
	@TransmissionType	[nvarchar]	(50),
	@EngineSize			[float],	
	@Description		[nvarchar]	(250)
)
AS
	BEGIN
		INSERT INTO [dbo].[CarInventory] VALUES 
		(@Model, @Year, @Color, @VIN, @Price, @Mileage, @FuelType, @TransmissionType, @EngineSize, @Description)
	END
GO
EXEC sp_insert_new_car @Model = 'Jeep',@Year = 2020, @Color = 'White', @VIN = 'Z2H0LU64', @Price = 35750.00, @Mileage = 0, @FuelType = 'Gasoline', @TransmissionType = 'Automatic', @EngineSize = 2.5, @Description = 'This will get ducked';

print '' print '*** creating sp_update_car ***'
GO
CREATE PROCEDURE [dbo].[sp_update_car]
(
	@CarID				[int],
	@Model				[nvarchar]	(50),
	@Year				[int],	
	@Color				[nvarchar]	(50),
	@VIN				[nvarchar]	(17),
	@Price				[float],
	@Mileage			[int],	
	@FuelType			[nvarchar]	(50),
	@TransmissionType	[nvarchar]	(50),
	@EngineSize			[float],	
	@Description		[nvarchar]	(250)
)
AS
	BEGIN
		UPDATE [dbo].[CarInventory] SET 
			[Model] = @Model, 
			[Year] = @Year, 
			[Color] = @Color,
			[VIN] = @VIN,
			[Price] = @Price,
			[Mileage] = @Mileage,
			[FuelType] = @FuelType,
			[TransmissionType] = @TransmissionType,
			[EngineSize] = @EngineSize,
			[Description] = @Description	
		WHERE [CarID] = @CarID
	END
GO

print '' print '*** creating sp_delete_car ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_car]
(
	@CarID				[int]
)
AS
	BEGIN
		DELETE FROM [dbo].[CarInventory] WHERE [CarID] = @CarID
	END
GO

print '' print '*** creating sp_create_service_appointment ***'
GO
CREATE PROCEDURE [dbo].[sp_create_service_appointment]
(
	@CarID			[int],
	@CustomerID		[int],
	@ServiceTypeID	[int]
)
AS
	BEGIN
		INSERT INTO [dbo].[ServiceAppointment]
			([CarID], [CustomerID], [ServiceTypeID])
		VALUES
			(@CarID, @CustomerID, @ServiceTypeID)
		SELECT SCOPE_IDENTITY()
	END
GO

print '' print '*** creating sp_retrieve_service_appointment_by_appointment_id ***'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_service_appointment_by_appointment_id]
(
	@AppointmentID			[int]
)
AS
	BEGIN
		SELECT * FROM [dbo].[ServiceAppointment] WHERE [AppointmentID] = @AppointmentID 
	END
GO

print '' print '*** creating sp_update_service_appointment ***'
GO
CREATE PROCEDURE [dbo].[sp_update_service_appointment]
(
	@AppointmentID	[int],
	@CarID			[int],
	@CustomerID		[int],
	@ServiceTypeID	[int],
	@SupplierID		[int],
	@ScheduleDate	[datetime]
)
AS
	BEGIN
		UPDATE [dbo].[ServiceAppointment] SET 
			[CarID] = @CarID,
			[CustomerID] = @CustomerID,
			[ServiceTypeID] = @ServiceTypeID,
			[SupplierID] = @SupplierID,
			[ScheduleDate] = @ScheduleDate
			WHERE [AppointmentID] = @AppointmentID
	END
GO

print '' print '*** creating sp_delete_service_appointment_by_appointment_id ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_service_appointment_by_appointment_id]
(
	@AppointmentID			[int]
)
AS
	BEGIN
		DELETE FROM [dbo].[ServiceAppointment] WHERE [AppointmentID] = @AppointmentID 
	END
GO

print '' print '*** creating sp_create_sale ***'
GO
CREATE PROCEDURE [dbo].[sp_create_sale]
(
	@EmployeeID	[int],
	@CarID		[int],
	@CustomerID	[int],
	@SaleDate	[datetime],
	@SalePrice	[float]

)
AS
	BEGIN
		INSERT INTO [dbo].[Sales]
			([EmployeeID], [CarID], [CustomerID], [SaleDate], [SalePrice])
		VALUES
			(@EmployeeID, @CarID, @CustomerID, @SaleDate, @SalePrice)
	END
GO

print '' print '*** creating sp_delete_sale ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_sale]
(
	@SaleID				[int]
)
AS
	BEGIN
		DELETE FROM [dbo].[Sales] WHERE [SaleID] = @SaleID
	END
GO

print '' print '*** creating sp_select_sale_by_id ***'
GO
CREATE PROCEDURE [dbo].[sp_select_sale_by_id]
(
	@SaleID		[int]
)
AS
	BEGIN
		SELECT * FROM [dbo].[Sales] WHERE Sales.SaleID = @SaleID
	END
GO

print '' print '*** creating sp_view_all_sales ***'
GO
CREATE PROCEDURE [dbo].[sp_view_all_sales]
AS
	BEGIN
		SELECT * FROM [dbo].[Sales]
	END
GO

print '' print '*** creating sp_view_sales_for_employee ***'
GO
CREATE PROCEDURE [dbo].[sp_view_sales_for_employee]
(
	@EmployeeID		[int]
)
AS
	BEGIN
		SELECT * FROM [dbo].[Sales] WHERE [EmployeeID] = @EmployeeID
	END
GO

print '' print '*** creating sp_update_employee ***'
GO
CREATE PROCEDURE [dbo].[sp_update_employee]
(
	@EmployeeID			[int],
	@FirstName			[nvarchar] (50),
	@LastName			[nvarchar] (50),
	@Password			[nvarchar] (100),
	@PhoneNumber		[nvarchar] (11),
	@Email				[nvarchar] (250),
	@Role				[nvarchar] (100)
)
AS
	BEGIN
		UPDATE [dbo].[Employee] SET 
			[FirstName] = @FirstName,
			[LastName] = @LastName,
			[Password] = @Password,
			[PhoneNumber] = @PhoneNumber,
			[Email] = @Email,
			[Role] = @Role
			WHERE [EmployeeID] = @EmployeeID
	END
GO

print '' print '*** creating sp_update_customer ***'
GO
CREATE PROCEDURE [dbo].[sp_update_customer]
(
	@CustomerID			[int],
	@FirstName			[nvarchar] (50),
	@LastName			[nvarchar] (50),
	@Password			[nvarchar] (100),
	@PhoneNumber		[nvarchar] (11),
	@Email				[nvarchar] (250)
)
AS
	BEGIN
		UPDATE [dbo].[Customer] SET 
			[FirstName] = @FirstName,
			[LastName] = @LastName,
			[Password] = @Password,
			[PhoneNumber] = @PhoneNumber,
			[Email] = @Email
			WHERE [CustomerID] = @CustomerID
	END
GO

print '' print '*** creating sp_create_repair_invoice ***'
GO
CREATE PROCEDURE [dbo].[sp_create_repair_invoice]
(
	@CarID				[int],
	@EmployeeID			[int],
	@IssueDescription	[nvarchar] (250),
	@RepairDate			[datetime]
)
AS
	BEGIN
		INSERT INTO [dbo].[RepairInvoice]
			([CarID], [EmployeeID], [IssueDescription], [RepairDate])
		VALUES
			(@CarID, @EmployeeID, @IssueDescription, @RepairDate)
	END
GO
















