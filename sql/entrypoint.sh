#!/bin/bash
/opt/mssql/bin/sqlservr &

echo "Aguardando SQL Server iniciar..."
sleep 15s

echo "Executando script de inicialização..."
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $SA_PASSWORD -d master -i /init.sql
echo "Banco de dados inicializado com sucesso!"

# Executa o comando padrão do container (mantém ele rodando)
exec "$@" 