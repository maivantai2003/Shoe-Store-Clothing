const connection = new signalR.HubConnectionBuilder()
    .withUrl("/Hub")
    .build();

connection.on("DiscountScheduled", function (message) {
    alert(message)
});
connection.start().catch(function (err) {
    return console.error(err.toString());
});
