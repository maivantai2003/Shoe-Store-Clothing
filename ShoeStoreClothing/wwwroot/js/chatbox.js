document.addEventListener("DOMContentLoaded", function () {
    const chatInput = document.getElementById("chat-input")
    const chatBody = document.getElementById("chat-body")
    const sendChatButton = document.getElementById("send-chat")
    sendChatButton.addEventListener("click", async function () {
        const userMessage = chatInput.value.trim();
        if (!userMessage) return;

        console.log(userMessage)
        $.ajax({
            url: "/ChatBot/GetResponse",
            method: "post",
            contentType: "application/json",
            data: {
                message: userMessage
            },
            success: function (response) {
                console.log(response)
            }
        })
        // Send request to server
        //const response = await fetch("/ChatBot/GetResponse", {
        //    method: "POST",
        //    headers: {
        //        "Content-Type": "application/json"
        //    },
        //    body: JSON.stringify({ message: userMessage })
        //});

        //const data = await response.json();
        //console.log(data)
        chatInput.value = "";
    });
    /*function addMessage(sender, message) {
        const messageElement = document.createElement("div");
        messageElement.textContent = `${sender}: ${message}`;
        chatBody.appendChild(messageElement);
        chatBody.scrollTop = chatBody.scrollHeight;
    }*/
})