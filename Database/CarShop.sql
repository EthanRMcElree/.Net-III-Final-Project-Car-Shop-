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
USE [CarShop]
GO

/* User Table */
print '' print '*** creating User table ***'
GO
CREATE TABLE [dbo].[User] (
	[UserID] 		[int] IDENTITY(1, 1) 		NOT NULL,
	[FirstName] 	[nvarchar] (50)				NOT NULL,
	[LastName] 		[nvarchar] (50)				NOT NULL,
	[Password] 		[nvarchar] (100)			NOT NULL DEFAULT
		'ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae',
	[PhoneNumber] 	[nvarchar] (11)				NOT NULL,
	[Email] 		[nvarchar] (250) 			NOT NULL,
	[Role] 			[nvarchar] (100)			NOT NULL,
	[IsLoggedIn]	[bit]		DEFAULT 0		NOT NULL,
	[IsAdmin]		[bit]		DEFAULT 0		NOT NULL,
	CONSTRAINT [pk_UserID] PRIMARY KEY([UserID]),
	CONSTRAINT [ak_Email] UNIQUE([Email])
)
GO

print '' print '*** inserting User test records ***'
GO
INSERT INTO [dbo].[User]
		([FirstName], [LastName], [PhoneNumber], [Email], [Role])
	VALUES
		('John', 'Smith', '3197771111', 'john@company.com', 'manager'),
		('Larry', 'Sky', '3197772222', 'larry@company.com', 'sales'),
		('Sam', 'Williams', '3197773333', 'sam@company.com', 'sales'),
		('Porsha', 'Perez', '3197774444', 'porsha@company.com', 'admin'),
		('Jack', 'Rawi', '3197775555', 'jack@company.com', 'admin'),
		('Zoe', 'Morn', '3197776666', 'zoe@company.com', 'customer'),
		('Jim', 'Cole', '3198887777', 'jim@company.com', 'customer')
GO

/* CarInventory Table */
print '' print '*** creating carinventory table ***'
GO
CREATE TABLE [dbo].[CarInventory] (
	[CarID]					[int] IDENTITY(1, 1) 	NOT NULL,
	[CustomerEmail]			[nvarchar] (256)		DEFAULT '',
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

print '' print '*** inserting carinventory test records ***'
GO
INSERT INTO [dbo].[CarInventory]
		([CustomerEmail], [Model], [Year], [Color], [VIN], [Price], [FuelType], [TransmissionType], [EngineSize], [Description])
	VALUES
		('sam@gmail.com' ,'Suv', 2020, 'Crimson', '4HDAS410', 27850.00, 'Gasoline', 'automatic', 2.5, 'this is a great truck'),
		('sam@gmail.com' ,'jeep', 2022, 'purple', '9F1J2AZ3', 30500.00, 'Gasoline', 'automatic', 2.5, 'Enjoy the rubber duckies you will get with this car')
GO

/* Sales Table */
print '' print '*** creating sales table ***'
GO
CREATE TABLE [dbo].[Sales] (
	[SaleID]				[int] IDENTITY(1, 1) 	NOT NULL,
	[UserID]				[int]					NOT NULL,
	[CarID]					[int] 			 		NOT NULL,
	[SaleDate]				[datetime],
	[SalePrice]				[float],
	CONSTRAINT [fk_Sales_CarID] FOREIGN KEY([CarID])
		REFERENCES [dbo].[CarInventory]([CarID])
		ON DELETE CASCADE,
	CONSTRAINT [pk_SaleID] PRIMARY KEY([SaleID])	
)
GO

print '' print '*** inserting sales test records ***'
GO
INSERT INTO [dbo].[Sales]
		([CarID], [UserID], [SaleDate], [SalePrice])
	VALUES
		(1, 2, '2023-07-09', 22500.00),
		(2, 3, '2022-09-12', 19295.50),
		(3, 1, '2021-04-19', 20750.00)		
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

/* ServiceType Table */
print '' print '*** creating servicetype table ***'
GO
CREATE TABLE [dbo].[ServiceType] (
	[ServiceTypeID]			[int] 						NOT NULL,
	[Description]			[nvarchar] (250)			NOT NULL,
	CONSTRAINT [pk_ServiceTypeID] PRIMARY KEY([ServiceTypeID])
)
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

/* ServiceAppointment Table */
print '' print '*** creating serviceappointment table ***'
GO
CREATE TABLE [dbo].[ServiceAppointment] (
	[AppointmentID]			[int] IDENTITY(1, 1) 				NOT NULL,
	[CarID]					[int]  								NOT NULL,
	[CustomerEmail]			[nvarchar] (256) 					NOT NULL,
	[ServiceTypeID]			[int]  								NOT NULL,
	[CustomerComments]		[nvarchar] (300),
	[ScheduleDate]			[datetime]						    NOT NULL,
	CONSTRAINT [fk_ServiceAppointment_CarID] FOREIGN KEY([CarID])
		REFERENCES [dbo].[CarInventory]([CarID])
		ON DELETE CASCADE,
	CONSTRAINT [fk_ServiceAppointment_ServiceTypeID] FOREIGN KEY([ServiceTypeID])
		REFERENCES [dbo].[ServiceType]([ServiceTypeID])
		ON DELETE CASCADE,
	CONSTRAINT [pk_AppointmentID] PRIMARY KEY([AppointmentID])
)
GO

print '' print '*** inserting ServiceAppointment test records ***'
GO
INSERT INTO [dbo].[ServiceAppointment]
		([CarID], [CustomerEmail], [ServiceTypeID], [ScheduleDate])
	VALUES
		(6, 'sam@gmail.com', 1, '2020-11-05'),
		(7, 'sam@gmail.com', 3, '2021-04-19')
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
		REFERENCES [dbo].[CarInventory]([CarID])
		ON DELETE CASCADE,
	CONSTRAINT [fk_Warranty_WarrantyTypeID] FOREIGN KEY([WarrantyTypeID])
		REFERENCES [dbo].[WarrantyType]([WarrantyTypeID])
		ON DELETE CASCADE,
	CONSTRAINT [pk_WarrantyID] PRIMARY KEY([WarrantyID])
)
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

/* RepairInvoice Table */
print '' print '*** creating RepairInvoice table ***'
GO
CREATE TABLE [dbo].[RepairInvoice] (
	[InvoiceID]				[int] IDENTITY(1, 1) 				NOT NULL,
	[CarID]					[int] 								NOT NULL,
	[UserID]				[int] 								NOT NULL,
	[IssueDescription]		[nvarchar] (250) 					NOT NULL,
	[RepairDate]			[datetime],
	CONSTRAINT [fk_RepairInvoice_CarID] FOREIGN KEY([CarID])
		REFERENCES [dbo].[CarInventory]([CarID])
		ON DELETE CASCADE,
	CONSTRAINT [fk_RepairInvoice_UserID] FOREIGN KEY([UserID])
		REFERENCES [dbo].[User]([UserID])
		ON DELETE CASCADE,
	CONSTRAINT [pk_InvoiceID] PRIMARY KEY([InvoiceID]),
)
GO

print '' print '*** inserting RepairInvoice test records ***'
GO
INSERT INTO [dbo].[RepairInvoice]
		([CarID], [UserID], [IssueDescription])
	VALUES
		(1, 1, "Replace oxygen sensor"),
		(3, 3, "Replace catalytic converter"),
		(1, 5, "Replace ignition coil"),
		(2, 2, "Replace spark plug"),
		(2, 4, "Replace spark plug wires"),
		(3, 5, "Replace mass air flow sensor")
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
		REFERENCES [dbo].[Sales]([SaleID])
		ON DELETE CASCADE,
	CONSTRAINT [pk_FinancingID] PRIMARY KEY([FinancingID])
)
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

/* stored procedures */

print '' print '*** creating sp_create_user_account ***'
GO
CREATE PROCEDURE [dbo].[sp_create_user_account]
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
		INSERT INTO [dbo].[User] ([FirstName], [LastName], [Password], [PhoneNumber], [Email], [Role])
		VALUES (@FirstName, @LastName, @Password, @PhoneNumber, @Email, @Role)
	END
GO



print '' print '*** creating sp_authenticate_user ***'
GO
CREATE PROCEDURE [dbo].[sp_authenticate_user]
(
	@Password		[nvarchar] (100),
	@Email			[nvarchar] (250)
)
AS
	BEGIN
		SELECT [UserID], [FirstName], [LastName], [Password], [PhoneNumber], [Email], [Role] 
		FROM [User]
		WHERE @Password = [Password]
		AND @Email = [Email]
	END
GO

print '' print '*** creating sp_select_user_by_id ***'
GO
CREATE PROCEDURE [dbo].[sp_select_user_by_id]
(
	@UserID			[int]
)
AS
	BEGIN
		SELECT [UserID], [FirstName], [LastName], [Password], [PhoneNumber], [Email], [Role] 
		FROM [User]
		WHERE @UserID = [UserID]
	END
GO

print '' print '*** creating sp_select_user_by_email ***'
GO
CREATE PROCEDURE [dbo].[sp_select_user_by_email]
(
	@Email			[nvarchar] (250)
)
AS
	BEGIN
		SELECT [UserID], [FirstName], [LastName], [Password], [PhoneNumber], [Email], [Role], [IsLoggedIn], [IsAdmin] 
		FROM [User]
		WHERE @Email = [Email]
	END
GO

print '' print '*** creating sp_select_all_user ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_user]
AS
	BEGIN
		SELECT [UserID], [FirstName], [LastName], [Password], [PhoneNumber], [Email], [Role] 
		FROM [User]		
	END
GO

print '' print '*** creating sp_change_user_password ***'
GO
CREATE PROCEDURE [dbo].[sp_change_user_password]
(
	@Email			[nvarchar] (250),
	@OldPassword	[nvarchar]	(100),
	@NewPassword	[nvarchar]	(100)
)
AS
	BEGIN		
		UPDATE [dbo].[User] SET [Password] = @NewPassword WHERE [Email] = @Email			
	END		
GO

print '' print '*** creating sp_reset_user_password ***'
GO
CREATE PROCEDURE [dbo].[sp_reset_user_password]
(
	@Email			[nvarchar] (250),
	@NewPassword	[nvarchar]	(100)
)
AS
	BEGIN
		UPDATE [dbo].[User] SET [Password] = @NewPassword WHERE [Email] = @Email
	END
GO

print '' print '*** creating sp_delete_user_account ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_user_account]
(
	@UserID			[nvarchar] (250)
)
AS
	BEGIN
		DELETE FROM [dbo].[User] WHERE [UserID] = @UserID
	END
GO

print '' print '*** creating sp_view_car_inventory ***'
GO
CREATE PROCEDURE [dbo].[sp_view_car_inventory]
AS
	BEGIN
		SELECT [CarID], [Model], [Year], [Color], [VIN], [Price], [Mileage], [FuelType], [TransmissionType], [EngineSize], [Description], 
		[CustomerEmail]
		FROM [dbo].[CarInventory]
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
		SELECT [CarID], [Model], [Year], [Color], [VIN], [Price], [Mileage], [FuelType], [TransmissionType], [EngineSize], [Description], 
		[CustomerEmail] 
		FROM [dbo].[CarInventory] WHERE CarInventory.CarID = @CarID
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
		SELECT [CarID], [Model], [Year], [Color], [VIN], [Price], [Mileage], [FuelType], [TransmissionType], [EngineSize], [Description] FROM [dbo].[CarInventory] WHERE [Model] = @Model
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
		SELECT [CarID], [Model], [Year], [Color], [VIN], [Price], [Mileage], [FuelType], [TransmissionType], [EngineSize], [Description] FROM [dbo].[CarInventory] WHERE [Year] = @Year
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
		SELECT [CarID], [Model], [Year], [Color], [VIN], [Price], [Mileage], [FuelType], [TransmissionType], [EngineSize], [Description] FROM [dbo].[CarInventory] WHERE [FuelType] = @FuelType
	END
GO

print '' print '*** creating sp_filter_car_inventory_by_high_mileage ***'
GO
CREATE PROCEDURE [dbo].[sp_filter_car_inventory_by_high_mileage]
AS
	BEGIN
		SELECT [CarID], [Model], [Year], [Color], [VIN], [Price], [Mileage], [FuelType], [TransmissionType], [EngineSize], [Description] FROM [dbo].[CarInventory] WHERE [Mileage] > 75000
	END
GO

print '' print '*** creating sp_filter_car_inventory_by_low_mileage ***'
GO
CREATE PROCEDURE [dbo].[sp_filter_car_inventory_by_low_mileage]
AS
	BEGIN
		SELECT [CarID], [Model], [Year], [Color], [VIN], [Price], [Mileage], [FuelType], [TransmissionType], [EngineSize], [Description] FROM [dbo].[CarInventory] WHERE [Mileage] < 10000
	END
GO

print '' print '*** creating sp_filter_car_inventory_by_moderate_mileage ***'
GO
CREATE PROCEDURE [dbo].[sp_filter_car_inventory_by_moderate_mileage]
AS
	BEGIN
		SELECT [CarID], [Model], [Year], [Color], [VIN], [Price], [Mileage], [FuelType], [TransmissionType], [EngineSize], [Description] FROM [dbo].[CarInventory] WHERE [Mileage] > 10000 AND [Mileage] < 75000
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
		SELECT [CarID], [Model], [Year], [Color], [VIN], [Price], [Mileage], [FuelType], [TransmissionType], [EngineSize], [Description], 
		[CustomerEmail] 
		FROM [dbo].[CarInventory] 
		WHERE [CarID] = @CarID
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
		INSERT INTO [dbo].[CarInventory] ([Model], [Year], [Color], [VIN], [Price], [Mileage], [FuelType], [TransmissionType], [EngineSize], [Description])
		VALUES (@Model, @Year, @Color, @VIN, @Price, @Mileage, @FuelType, @TransmissionType, @EngineSize, @Description)
	END
GO

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
	@Description		[nvarchar]	(250),
	@CustomerEmail 		[nvarchar]  (256)
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
			[Description] = @Description,
			[CustomerEmail] = @CustomerEmail			
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
	@CarID				[int],
	@CustomerEmail		[nvarchar] (256),
	@ServiceTypeID		[int],
	@CustomerComments 	[nvarchar] (300),
	@ScheduleDate 	  	[datetime]	
)
AS
	BEGIN
		INSERT INTO [dbo].[ServiceAppointment]
			([CarID], [CustomerEmail], [ServiceTypeID], [CustomerComments], [ScheduleDate])
		VALUES
			(@CarID, @CustomerEmail, @ServiceTypeID, @CustomerComments, @ScheduleDate)
		SELECT SCOPE_IDENTITY()
	END
GO

print '' print '*** creating sp_view_all_service_appointments ***'
GO
CREATE PROCEDURE [dbo].[sp_view_all_service_appointments]
AS
	BEGIN
		SELECT [AppointmentID], [CarID], [CustomerEmail], [ServiceTypeID], [ScheduleDate] FROM [dbo].[ServiceAppointment]
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
		SELECT [AppointmentID], [ServiceAppointment].[CarID], [ServiceAppointment].[CustomerEmail], [ServiceAppointment].[ServiceTypeID], [CustomerComments], 
		[ScheduleDate], [CarInventory].[Model], [ServiceType].[Description]
		FROM [dbo].[ServiceAppointment]
	    JOIN [dbo].[CarInventory] ON [CarInventory].[CarID] = [ServiceAppointment].[CarID]
		JOIN [dbo].[ServiceType] ON [ServiceType].[ServiceTypeID] = [ServiceAppointment].[ServiceTypeID]
		WHERE [AppointmentID] = @AppointmentID 
	END
GO

print '' print '*** creating sp_update_service_appointment ***'
GO
CREATE PROCEDURE [dbo].[sp_update_service_appointment]
(
	@AppointmentID	[int],
	@CarID			[int],
	@CustomerEmail	[nvarchar] (256),
	@ServiceTypeID	[int],
	@CustomerComments [nvarchar] (300),
	@ScheduleDate	[datetime]
)
AS
	BEGIN
		UPDATE [dbo].[ServiceAppointment] SET 
			[CarID] = @CarID,
			[CustomerEmail] = @CustomerEmail,
			[ServiceTypeID] = @ServiceTypeID,
			[CustomerComments] = @CustomerComments,
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
	@UserID		[int],
	@CarID		[int],
	@SaleDate	[datetime],
	@SalePrice	[float]

)
AS
	BEGIN
		INSERT INTO [dbo].[Sales]
			([UserID], [CarID], [SaleDate], [SalePrice])
		VALUES
			(@UserID, @CarID, @SaleDate, @SalePrice)
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
		SELECT [SaleID], [UserID], [CarID], [SaleDate], [SalePrice] FROM [dbo].[Sales] WHERE Sales.SaleID = @SaleID
	END
GO

print '' print '*** creating sp_view_all_sales ***'
GO
CREATE PROCEDURE [dbo].[sp_view_all_sales]
AS
	BEGIN
		SELECT [SaleID], [UserID], [CarID], [SaleDate], [SalePrice] FROM [dbo].[Sales]
	END
GO

print '' print '*** creating sp_view_sales_for_User ***'
GO
CREATE PROCEDURE [dbo].[sp_view_sales_for_User]
(
	@UserID		[int]
)
AS
	BEGIN
		SELECT [SaleID], [UserID], [CarID], [SaleDate], [SalePrice] FROM [dbo].[Sales] WHERE [UserID] = @UserID
	END
GO

print '' print '*** creating sp_update_User ***'
GO
CREATE PROCEDURE [dbo].[sp_update_User]
(	
	@FirstName			[nvarchar] (50),
	@LastName			[nvarchar] (50),	
	@PhoneNumber		[nvarchar] (11),
	@Email				[nvarchar] (250),
	@Role				[nvarchar] (100),
	@IsLoggedIn			[bit],
	@IsAdmin			[bit]
)
AS
	BEGIN
		UPDATE [dbo].[User] SET 
			[FirstName] = @FirstName,
			[LastName] = @LastName,			
			[PhoneNumber] = @PhoneNumber,
			[Email] = @Email,
			[Role] = @Role,
			[IsLoggedIn] = @IsLoggedIn,
			[IsAdmin] = @IsAdmin
			WHERE [Email] = @Email
	END
GO

print '' print '*** creating sp_create_repair_invoice ***'
GO
CREATE PROCEDURE [dbo].[sp_create_repair_invoice]
(
	@CarID				[int],
	@UserID				[int],
	@IssueDescription	[nvarchar] (250),
	@RepairDate			[datetime]
)
AS
	BEGIN
		INSERT INTO [dbo].[RepairInvoice]
			([CarID], [UserID], [IssueDescription], [RepairDate])
		VALUES
			(@CarID, @UserID, @IssueDescription, @RepairDate)
	END
GO

print '' print '*** creating sp_view_all_sales_data ***'
GO
CREATE PROCEDURE [dbo].[sp_view_all_sales_data]
AS
	BEGIN
		SELECT sls.SaleID, sls.UserID, sls.CarID, sls.SaleDate, sls.SalePrice, usr.FirstName, usr.LastName, car.Model, car.Year
		FROM [dbo].[Sales] sls
		JOIN [dbo].[User] usr ON [sls].[UserID] = [usr].[UserID]
		JOIN [dbo].[CarInventory] car ON [sls].[CarID] = [car].[CarID]
	END
GO

print '' print '*** creating sp_view_all_service_appointments_data ***'
GO
CREATE PROCEDURE [dbo].[sp_view_all_service_appointments_data]
AS
	BEGIN
		SELECT sap.AppointmentID, sap.CarID, sap.CustomerEmail, sap.ServiceTypeID, sap.CustomerComments, sap.ScheduleDate
		FROM [dbo].[ServiceAppointment] sap
	END
GO

print '' print '*** creating sp_retrieve_service_type_by_type_id ***'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_service_type_by_type_id]
(
	@ServiceTypeID [int]
)
AS
	BEGIN
		SELECT [ServiceTypeID], [Description]
		FROM [dbo].[ServiceType]
		WHERE ServiceTypeID = @ServiceTypeID
	END
GO

print '' print '*** creating sp_retrieve_service_type_by_description ***'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_service_type_by_description]
(
	@Description [nvarchar](250)
)
AS
	BEGIN
		SELECT [ServiceTypeID], [Description]
		FROM [dbo].[ServiceType]
		WHERE Description = @Description
	END
GO

/*Must be run separately after identity migrations*/
/*print '' print '*** creating sp_get_all_customer_emails ***'
GO
CREATE PROCEDURE [dbo].[sp_get_all_customer_emails]
AS
	BEGIN
		SELECT [Email]
		FROM [dbo].[AspNetUsers]
		JOIN [dbo].[AspNetUserRoles]
		ON [dbo].[AspNetUsers].[Id] = [dbo].[AspNetUserRoles].[UserId]
		JOIN [dbo].[AspNetRoles]
		ON [dbo].[AspNetRoles].[Id] = [dbo].[AspNetUserRoles].[RoleId]
		WHERE [dbo].[AspNetRoles].[Name] = 'CUSTOMER'
	END
GO*/















