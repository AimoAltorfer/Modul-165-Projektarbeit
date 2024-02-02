USE [SkiServiceManagement];

--Exportieren der SQL-Daten Orders
bcp "SELECT * FROM SkiServiceManagement.dbo.Orders FOR JSON PATH" queryout orders.json -c -t, -S sql_server_instance -U username -P password

--Exportieren der SQL-Daten Employees
bcp "SELECT * FROM SkiServiceManagement.dbo.Employees FOR JSON PATH" queryout employees.json -c -t, -S sql_server_instance -U username -P password
