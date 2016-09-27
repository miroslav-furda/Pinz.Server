CREATE USER [pinzServiceWe] FOR LOGIN [pinzServiceWe] WITH DEFAULT_SCHEMA=[dbo]
ALTER ROLE [db_owner] ADD MEMBER [pinzServiceWe]
GO