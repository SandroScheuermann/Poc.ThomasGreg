#!/bin/bash
/opt/mssql/bin/sqlservr &

sleep 15

/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $SA_PASSWORD -d master -i /init.sql

tail -f /dev/null
