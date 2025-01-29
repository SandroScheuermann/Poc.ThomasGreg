FROM mcr.microsoft.com/mssql/server:latest

ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=P@ssw0rd
ENV MSSQL_PID=Express

COPY init.sql /init.sql

CMD /bin/bash -c 'sleep 20 && /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "$SA_PASSWORD" -d master -i /init.sql && tail -f /dev/null'
