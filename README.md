# Tecnologias utilizadas

- .NET 8.0
- MVC Razor
- API Rest
- Arquitetura DDD
- Testes automatizados
- Dapper
- Docker

# Execução do projeto

1. Abrir algum prompt de comando na pasta raiz.
2. Executar o comando "docker-compose up".
3. A aplicação estará disponível no localhost:8080

# Decisões técnicas

A solução foi projetada seguindo uma arquitetura de três camadas:

- Front-end em ASP.NET MVC com Razor, como foi solicitado no documento.
- Back-end feito em uma API REST .NET 8.0, utilizando Dapper como ORM, pois já iriam ser utilizadas procedures para operações mais custosas.
- Banco de dados : SQL Server 2017, conforme solicitado no documento.

# Diretrizes para Desenvolvedores

- Padrões de Codificação: Segue convenções do C#, incluindo uso de async/await, injeção de dependências e separação de responsababilidades.
- Padrão de Camadas: Divisão clara entre controllers, services e repository.

# Diagrama 

![image](https://github.com/user-attachments/assets/ab96cb91-d135-4065-bac8-ed5a939d53e7)






 
