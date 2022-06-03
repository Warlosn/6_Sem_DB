--TODO:3

-- Task 2
use master;
create database laba10;
use laba10;
select * from users;
create table users(id_user int);
create login perviy with password = 'perviy';
create login vtoroy with password = 'vtoroy';
create  user perviy for login perviy;
create user vtoroy for login vtoroy;

grant connect on database :: laba10 to perviy;
grant create function on database :: laba10 to perviy;
grant create procedure on database :: laba10 to perviy;
grant create table on database :: laba10 to perviy;
grant select, insert, update,delete on database :: laba10 to perviy;
go

-- Task 3
deny select on users to vtoroy;
go
create procedure UseProcGetAddresses
    with execute as 'perviy'
    as
    select * from USERS ;
go;
setuser 'perviy'
grant execute on UseProcGetAddresses to vtoroy;
setuser 'vtoroy';
select * from USERS; 
use laba10
exec UseProcGetAddresses;

-- TAsk 4 run scripts only master
use master;
create server audit ServerAudit
to file
(
    filepath = 'C:\audit\server',
    maxsize = 1GB,
    max_rollover_files = 0,
    reserve_disk_space = off
)
with
(
    queue_delay = 1000,
    on_failure = continue
)

create server audit AppLog to APPLICATION_LOG;
create server audit SecurityLog to SECURITY_LOG;

-- Task 5
CREATE SERVER AUDIT SPECIFICATION ServerAuditSpecification
FOR SERVER AUDIT ServerAudit
    ADD (FAILED_LOGIN_GROUP);
GO

-- Task 6
ALTER SERVER AUDIT ServerAudit
WITH (STATE = ON);
GO

-- Task 7
create server audit DataBaseAudit
to file
(
    filepath = 'C:\audit\server\database\',
    maxsize = 10GB,
    max_rollover_files = 0,
    reserve_disk_space = off
)
with
(
    queue_delay = 1000,
    on_failure = continue
)

-- Task 8
use laba10;
create table jobs(id_job int);

create database audit specification CustomAuditSpecific
for server audit DataBaseAudit
add (select, insert, update , delete on jobs by dbo)
go
use master
-- Task 9
alter server audit DataBaseAudit
with (state = on);
go;

-- Task 10
alter server audit DataBaseAudit
with (state = off);
go;

alter server audit ServerAudit
with (state = off);
go

-- Task 11
create asymmetric key SomeKey
    with algorithm = rsa_2048
    encryption by password = '~qa1kKkalakJ39KDA~';

-- Task 12
declare @text NVARCHAR(100);
declare @cipher varbinary(512);

set @text = 'text for encrypt';
print @text;

set @cipher = ENCRYPTBYASYMKEY(ASYMKEY_ID('SomeKey'), @text);
print @cipher;

set @text = cast(DECRYPTBYASYMKEY(ASYMKEY_ID('SomeKey'), @cipher, N'~qa1kKkalakJ39KDA~') as nvarchar(100));
print @text;

-- Task 13
create certificate SomeCertificate
    encryption by password = N'~qa1kKkalakJ39KDA~'
    with subject = N'Some Certificate',
    expiry_date = N'20231010'
-- drop certificate SomeCertificate;
select * from sys.certificates;
drop certificate SomeCertificate;

-- Task 14
go
declare @text nvarchar(100);
declare @decrypt nvarchar(100);
declare @cipher varbinary(max);

set @text = 'text for encrypt';
print @text;

set @cipher = ENCRYPTBYCERT(CERT_ID('SomeCertificate'), @text);
print @cipher;

set @decrypt = cast(DECRYPTBYCERT(CERT_ID('SomeCertificate'), @cipher, N'~qa1kKkalakJ39KDA~') as nvarchar(max));
print @decrypt;

-- Task 15
create symmetric key SymmetricKey
    with algorithm = AES_256
    encryption by password = '~qa1kKkalakJ39KDA~';

open symmetric key SymmetricKey
    decryption by password = N'~qa1kKkalakJ39KDA~';

create symmetric key SymmetricKeyData
    with algorithm = AES_256
    encryption by symmetric key SymmetricKey;

open symmetric key SymmetricKeyData
    decryption by symmetric key SymmetricKey;

select * from sys.symmetric_keys;
go
-- Task 16
declare @text nvarchar(100);
declare @decrypt nvarchar(100);
declare @cipher varbinary(max);

set @text = 'text for encrypt';
print @text;

set @cipher = ENCRYPTBYKEY(KEY_GUID('SymmetricKeyData'), @text);
print @cipher;

set @decrypt = cast(DECRYPTBYKEY(@cipher) as nvarchar(max));
print @decrypt;

-- Task 17
use master;
create master key encryption by password = '~qa1kKkalakJ39KDA~';

create certificate ServerCert
    with subject = 'DEK Certificate';

use laba10;

create database encryption key
with algorithm = aes_256
encryption by server certificate ServerCert;

alter database laba10
    set encryption on;
alter database laba10
    set encryption off;

select * from sys.dm_database_encryption_keys;
SELECT *
FROM sys.dm_database_encryption_keys
WHERE encryption_state = 3;
use master;
-- Task 18
select hashbytes('sha1', 'text, some text =)');

-- Task 19
go
declare @text nvarchar(100)
declare @sign varbinary(max);
set @text = 'text, some text =)';

set @sign = SignByCert(Cert_ID('SomeCertificate'), @text, N'~qa1kKkalakJ39KDA~' )
select @sign;
select VerifySignedByCert(Cert_ID('SomeCertificate'), @text, @sign)
-- Task 20
backup service master key to file = 'C:\backups\backup_master_key.dat'
    encryption by password = '#asdr3adkDJSa$%z!~';

backup certificate SomeCertificate to file = N'C:\backups\SomeCertificate.crt';
