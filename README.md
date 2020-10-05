# BatePapo com Websocket em Aspnet Core

Este projeto consiste em um serviço de Bate Papo, contendo uma solução composta de 2 projetos: ChatServer e ChatClient (ambos em AspNetCore 3.1).
Não foram utilizadas bibliotecas externas, como SignalR. Apenas as bibliotecas base do AspNet foram utilizadas.

# Para executar o projeto:
Faça o clone em sua máquina local e abra a solution no Visual Studio 2019.
Configure a solution para executar ambos os projetos em paralelo:
 - ChatServer
 - ChatClient

Será aberta uma janela do navegador contendo o cliente de chat. Para simular mais de um usuário, basta abrir várias páginas acessando a url http://localhost:54972/.

O websocket está disponível através da URL: ws://localhost:54985/ws.

# Ajuda
É possível enviar alguns tipos de mensagem:
 - Mensagem pública para a sala
 - Mensagem pública para a sala mensionando algum usuário
 - Mensagem privada para um usuário
 
Para acessar a ajuda nos comandos que podem ser utilizados no Chat (Frontend), basta clicar no botão "Ajuda" no canto superior direito da tela.

# Teste
Foi implementado um simples teste de funcionalidade fim-a-fim, através de um mero código Javascript (TestUtils.js) para orquestrar e tratar as chamadas aos métodos da classe ChatService.js.

O teste foi montado no arquivo \ChatClient\wwwroot\js\TestCase.js e aborda a conexão e troca de 3 usuários em uma sala, sendo o terceiro usuário com um apelido igual ao primeiro e seu login deve ser rejeitado.

Para executar o caso de teste:
 1. Execute a aplicação
 2. Abra o navegador da internet (utilizamos Google Chrome neste exemplo) e abra o console do desenvolvedor (F12)
 3. Acesse a rota http://localhost:54972/Test
 4. Verifique a execução dos passos através do Log
