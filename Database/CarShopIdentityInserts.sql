USE [CarShop]
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'37bce22a-5642-4c08-9c69-14331f4356f4', N'martin@gmail.com', 0, N'ACis0r1S+vRWumQC3+zgoK2bzWvvYT3ZGTh1mdCTBEX75yVdQ3s7PYaaMh5s0E4JJA==', N'3558f533-0c79-4168-a4fa-053386b92351', NULL, 0, 0, NULL, 1, 0, N'martin@gmail.com')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'43f848fe-f9e3-4ec8-a1b7-1e244f2e8bf3', N'ethan@company.com', 0, N'AL7VEx6NIdfQOX7932uek6rSvVXKDJ9k9KW7EZNNvGgkCOtt66VUBZRuDf+UTKW16A==', N'5d2b3c47-46a4-420b-bdfe-699b75da1260', NULL, 0, 0, NULL, 1, 0, N'ethan@company.com')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'788975d8-5ccb-426a-a78d-251e74651935', N'jake@company.com', 0, N'ABnAxBhaDiVPG4ZOM/r0Xfq+SmqLWi/oBQCfnuRdEzuwNX2AlpJJcO62JzGSHRPrmQ==', N'50924e29-87d7-47fd-908e-90fad619f010', NULL, 0, 0, NULL, 1, 0, N'jake@company.com')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'8eb91931-7ac1-41bd-9ee3-444d7b2de3c7', N'john@company.com', 0, N'ANS7sRNkn3tIAsp4ys+zBmI1x1abCvw3arMk7BgN9GxR4eQBoVb9mfR0wp0vdqeOLg==', N'057ae1c7-c454-4f53-b537-4ab906b229f5', NULL, 0, 0, NULL, 1, 0, N'john@company.com')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'c552178e-3dfd-491f-a97a-951c850f34fb', N'sam@gmail.com', 0, N'AOp4UbTcku75mYgJJVlh5qWDzb/r5lkkXyWjKn1IEdy6q8dj2O5Pvosa11YklHxnLA==', N'a385efc0-c6aa-4a90-a88a-af1a7f010752', NULL, 0, 0, NULL, 1, 0, N'sam@gmail.com')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'5', N'ADMIN')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'2', N'CUSTOMER')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'3', N'EMPLOYEE')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'4', N'MANAGER')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'1', N'USER')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'37bce22a-5642-4c08-9c69-14331f4356f4', N'1')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'43f848fe-f9e3-4ec8-a1b7-1e244f2e8bf3', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'788975d8-5ccb-426a-a78d-251e74651935', N'4')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'8eb91931-7ac1-41bd-9ee3-444d7b2de3c7', N'5')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c552178e-3dfd-491f-a97a-951c850f34fb', N'2')
GO