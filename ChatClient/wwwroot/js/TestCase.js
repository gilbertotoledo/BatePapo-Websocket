(function () {
    //Caso de teste para 3 usuários em uma sala, sendo o terceiro usuário com um apelido igual ao primeiro.


    var connectionA = null;
    var connectionB = null;
    var connectionA_duplicate = null;

    var optionsA = {
        username: "A",
        onOpen: (evt) => {
            //console.log("Connection open to 'A'");
        },
        onClose: (etv) => {
            //console.log("Connection close to 'A'");
        },
        onMessage: (evt) => {
            console.log("Data received by 'A': " + evt.data);
            message = ChatService.parse(evt.data);
            //console.log(message.content);
        }
    }

    var optionsB = {
        username: "B",
        onOpen: (evt) => {
            //console.log("Connection open to 'B'");
        },
        onClose: (etv) => {
            //console.log("Connection close to 'B'");
        },
        onMessage: (evt) => {
            console.log("Data received by 'B': " + evt.data);
            message = ChatService.parse(evt.data);
            //console.log(message.content);
        }
    }

    var optionsA_duplicate = {
        username: "A",
        onOpen: (evt) => {
            //console.log("Connection open to 'A_duplicate'");
        },
        onClose: (etv) => {
            //console.log("Connection close to 'A_duplicate'");
        },
        onMessage: (evt) => {
            //console.log("Data received to 'A': " + evt.data);
            message = ChatService.parse(evt.data);

            it("Login 'A' duplicado - deve falhar", () => {
                assert(message.command != "/lerr");
            });

            if (message.command == "/lerr")
                connectionA_duplicate.disconnect();
        }
    }
       
    setTimeout(() => {
        it('Abrir conexão para usuário A e fazer login', () => {
            connectionA = new ChatService(optionsA);
            connectionA.connect();
        });
    }, 1000);

    setTimeout(() => {
        it('Abrir conexão para usuário B e fazer login', () => {
            connectionB = new ChatService(optionsB);
            connectionB.connect();
        });
    }, 2500);

    setTimeout(() => {
        it('Abrir conexão para usuário A_duplicate e fazer login', () => {
            connectionA_duplicate = new ChatService(optionsA_duplicate);
            connectionA_duplicate.connect();
        });
    }, 4000);


    setTimeout(() => {
        it('Usuário A enviar mensagem pública', () => {
            connectionA.sendMessage("Olá pessoal!");
        });
    }, 5000);

    setTimeout(() => {
        it('Usuário B enviar mensagem pública', () => {
            connectionB.sendMessage("Olá a todos!");
        });
    }, 6000);

    setTimeout(() => {
        it('Usuário A enviar mensagem mensionando usuário B', () => {
            connectionA.sendMessage("@B Olá, B!");
        });
    }, 7000);

    setTimeout(() => {
        it('Usuário B enviar mensagem privada para usuário A', () => {
            connectionB.sendMessage("/p @A Preciso te contar um segredo.");
        });
    }, 8000);

    setTimeout(() => {
        it('Sair usuário A', () => {
            connectionA.disconnect();
        });
    }, 10000);

    setTimeout(() => {
        it('Sair usuário B', () => {
            connectionB.disconnect();
        });
    }, 11000);

})();