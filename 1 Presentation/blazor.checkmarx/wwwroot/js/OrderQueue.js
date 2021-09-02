
var connection = new signalR.HubConnectionBuilder().withUrl("/orderHub").build();

//Disable send button until connection is established
//document.getElementById("sendButton").disabled = true;

connection.on("orderReceived", function (message) {
    var li = document.createElement("li");
    document.getElementById("orderList").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    li.textContent = `${message}`;
   
});


connection.on("deliveryReceived", function ( message) {
    var li = document.createElement("li");
    document.getElementById("receivedList").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    li.textContent = `${message}`;
   
}); 

connection.start().then(function () {
   // document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});


