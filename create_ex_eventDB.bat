@ECHO off

ECHO Attempting to create the ex_eventDB database...
sqlcmd -S localhost\SQLEXPRESS -E -i _create_ex_eventDB.sql
ECHO.
ECHO If you recieved no error messages, then the database was created.
ECHO.
PAUSE