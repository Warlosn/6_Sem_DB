CREATE USER migrationtest
  IDENTIFIED BY dfyef2001
alter user migrationtest quota UNLIMITED on users; 
grant connect, RESOURCE, create session, create view, create MATERIALIZED VIEW to migrationtest IDENTIFIED BY dfyef2001
select file_name, tablespace_name from dba_data_files;
alter session set container=GIS_PDB;
grant all PRIVILEGES to  migrationtest
drop user  migrationtest



