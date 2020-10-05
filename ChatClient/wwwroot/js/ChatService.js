﻿class ChatService {
    uri = "ws://localhost:54985/ws";
    socket = null;

    options = {
        username: null,
        onOpen: (event) => { },
        onClose: (event) => { },
        onMessage: (event) => { },
        onError: (event) => { },
    };

    static commands = {
        login: "/l",
        messageToRoom: "/mr",
        messagePrivate: "/mp",
        enteredRoom: "/er",
        exitedRoom: "/xr",
        receiveMessage: "/rm"
    }

    constructor(options) {
        this.options = options;
    }

    connect() {
        this.socket = new WebSocket(this.uri);
        this.configureListener();
    }

    disconnect() {
        this.socket.close();
    }

    configureListener() {
        this.socket.onopen = (event) => {
            console.log("opened connection to " + this.uri);
            this.sendCommand(ChatService.commands.login, this.options.username);

            if (this.options.onOpen != undefined) {
                this.options.onOpen();
            }
        };

        this.socket.onclose = (event) => {
            console.log("closed connection from " + this.uri);
            if (this.options.onClose != undefined) {
                this.options.onClose();
            }
        };

        this.socket.onmessage = (event) => {
            console.log(event.data);
            if (this.options.onMessage != undefined) {
                this.options.onMessage(event);
            }
        };

        this.socket.onerror = (event) => {
            console.log("error: " + event.data);
            if (this.options.onError != undefined) {
                this.options.onError(event);
            }
        };
    }

    sendCommand(command, message) {
        this.socket.send(command + " " + message);
    }

    sendMessage(message) {
        if (message.indexOf("/p") != -1) {//private
            this.socket.send(ChatService.commands.messagePrivate + " " + message.replace("/p ",""));
        } else {
            this.socket.send(ChatService.commands.messageToRoom + " " + message);
        }
    }

    parse(text) {
        return {
            command: text.substr(0, text.indexOf(" ")),
            content: text.substring(text.indexOf(" ") + 1)
        };
    }
}