var chatService = null;

var roomList = document.getElementById("rooms");
var messageList = document.getElementById("messages");

var connectButton = document.getElementById("connectButton");
var sendButton = document.getElementById("sendButton");
var createRoomButton = document.getElementById("createRoomButton");

var textMessage = document.getElementById("textMessage");
var textRoom = document.getElementById("textRoom");
var textName = document.getElementById("textName");


connectButton.addEventListener("click", () => {
    if (connectButton.innerText == "Connectar")
        connect();
    else
        disconnect();

    connectButton.disabled = "disabled";
});

sendButton.addEventListener("click", function () {
    chatService.sendMessage(textMessage.value);
    textMessage.value = "";
});

function connect() {
    var options = {
        username: textName.value,

        onOpen: (evt) => {
            textName.disabled = 'disabled';
            connectButton.innerHTML = "Desconectar";
            connectButton.disabled = "";

            textMessage.disabled = "";
            sendButton.disabled = "";

            textRoom.disabled = "";
            createRoomButton.disabled = "";
        },
        onClose: (etv) => {
            textName.disabled = '';
            connectButton.innerHTML = "Conectar";
            connectButton.disabled = "";
        },
        onMessage: (evt) => {
            message = chatService.parse(evt.data);

            switch (message.command) {
                case ChatService.commands.enteredRoom:
                    appendMessage(message.content, "text-success");
                    break;
                case ChatService.commands.exitedRoom:
                    appendMessage(message.content, "text-danger");
                    break;
                case ChatService.commands.receiveMessage:
                    appendMessage(message.content);
                    break;
            }
        }
    }
    chatService = new ChatService(options);
    chatService.connect();
}

function disconnect() {
    chatService.disconnect();
}

function appendMessage(message,style=null) {
    var item = document.createElement("p");
    item.classList.add("mb-0");
    if (style!=null)
        item.classList.add(style);

    item.appendChild(document.createTextNode(message));
    messageList.appendChild(item);
}
