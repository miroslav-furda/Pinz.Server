﻿IF EXISTS(SELECT * FROM sys.database_principals WHERE name = '<LOGIN_ACCOUNT>')
  DROP USER [<LOGIN_ACCOUNT>]
  DROP LOGIN [<LOGIN_ACCOUNT>]
GO

IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'<LOGIN_ACCOUNT>')
  CREATE LOGIN [<LOGIN_ACCOUNT>] WITH PASSWORD=N'<ACCOUNT_PASSWORD>'
GO