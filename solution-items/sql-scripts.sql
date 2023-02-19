use DebeziumTicketDb
go
exec sys.sp_cdc_enable_db

use DebeziumTicketDb
go
exec sys.sp_cdc_enable_table
@source_schema = N'dbo',
@source_name = N'TicketOutbox',
@role_name = NULL,
@filegroup_name = N'',
@supports_net_changes = 0

select
    name,
    is_cdc_enabled
from sys.databases;

select
    name,
    is_tracked_by_cdc
from sys.tables;

select * from TicketOutbox

delete from TicketOutbox