"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    li.textContent = `${user} says ${message}`;
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;

    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    
    if (message.substr(0, 6) == "/stock") {

        var request = `"{'command': '\/stock', 'parameter': '${message.substr(7, message.length-1)}'}"`;

        $.post("http://localhost:5158/commandMessage", request, function (data) {
            
            var quotationResponse = data;

            connection.invoke("SendMessage", user, quotationResponse).catch(function (err) {
                return console.error(err.toString());
            });
        });
    }
    
    event.preventDefault();
});