# Take-TesteTecnico

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
