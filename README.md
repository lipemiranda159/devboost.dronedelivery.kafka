# devboost.dronedelivery.kafka
Grupo 4 - Projeto Itaú (Kafka)

## Tecnologias:

- ASP.NET Core 3.1
- ASP.NET WebApi Core with JWT Identity Authentication
- Entity Framework Core 3.1
- Kafka
- Hangfire

## Arquitetura:

- DDD
- Repository
- BDD
- TDD

## Grupo 4 - Desenvolvedores

- Italo Vinicios
- Felipe Miranda
- Lucas Scheid 
- Marcos Alves 

## Instruções para rodar a cobertura de código
 
 - Ir até a pasta do projeto de testes
 - Executar o comando: dotnet test --collect:"XPlat Code Coverage" 
 - Após a execução do comando, dentro do mesmo local será gerada a pasta TesteResults + GUID identificador
 - Entrar nesta pasta <TesteResults + GUID identificador>
 - Executar o comando: reportgenerator "-reports:coverage.cobertura.xml" "-targetdir:C:\Temp" "-reporttypes:HTML"
 
 - Observação: No comando acima, verificar o local na opção -targetdir:C:\Temp, caso necessário, este local pode ser modificado

## Considerações
 - Levamos em conta que a Api de pagamentos estaria publicada com bloqueio de qualquer ip que não seja o da api de Pedidos
 - Deixamos a api utilizando banco em memória para facilitar os testes da mesma
 - Os dados do cartão são passados em uma string, dessa forma facilitamos alterações para que a api possa aceitar outros tipos de pagamanto