FROM mcr.microsoft.com/mssql/server:2017-latest
 
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=P@ssw0rd

RUN mkdir -p /usr/config
WORKDIR /usr/config
 
COPY . /usr/config
 
RUN chmod +x /usr/config/entrypoint.sh
RUN chmod +x /usr/config/configure-db.sh

ENTRYPOINT ["./entrypoint.sh"]