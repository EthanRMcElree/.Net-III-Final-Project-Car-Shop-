ECHO off

sqlcmd -S localhost -E -i CarShop.sql

rem server is localhost

ECHO .
ECHO if no errors appear DB was created
PAUSE
