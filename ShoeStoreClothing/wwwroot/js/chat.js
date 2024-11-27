"use strict"
var connection = new signalR.HubConnectionBuilder().withUrl("/Hub").build()
connection.start().catch(function (err) {
    return console.error(err.toString())
})
connection.on("ReceiveMessage", function (mess) {
    LoadMessage()
})
$(document).on("click", "#btnSend", function () {
    var val = $("#txtMessage").val()
    var id = $("#valId").val()
    var regex = /^\s*$/
    if (!regex.test(val)) {
        var content = `<div class="d-flex align-items-center text-right justify-content-end ">
				<div class="pr-2">
					<p class="msg">${val}</p>
				</div>
				<div><img src="https://i.imgur.com/HpF4BFG.jpg" width="30" class="img1" /></div>

			</div>`
        connection.invoke("SendMessage", id, val).catch(function (err) {
            return console.error(err.toString())
        })
        $("#renderMessager").append(content)
        $("#txtMessage").val("")
    }
})
function LoadMessage() {
    var currentId = $("#valId").val()
    console.log(currentId)
    $.ajax({
        url:"/Admin/User/Message",
        method: "get",
        contentType: "application/json",
        data:{
            id: currentId,
        },
        success: function (response) {
            $('#renderMessager').empty();
            response.forEach(function (msg) {
                console.log(msg.senderId, currentId)
                console.log(msg.senderId !== currentId)
                if (msg.senderId === currentId) {
                    var message2 = `<div class="d-flex align-items-center text-right justify-content-end ">
				            <div class="pr-2">
					            <p class="msg">${msg.content}</p>
				            </div>
				            <div><img src="https://i.imgur.com/HpF4BFG.jpg" width="30" class="img1" /></div>
			            </div>`;
                    $('#renderMessager').append(message2);
                } else {
                    var message1 = `<div class="d-flex align-items-center">
				            <div class="text-left pr-1"><img src="https://i.imgur.com/HpF4BFG.jpg" width="30" class="img1" /></div>
				            <div class="pr-2 pl-1">
					            <p class="msg">${msg.content}</p>
				            </div>
			            </div>`;
                    $('#renderMessager').append(message1);
                }
            })
        },error: function (xhr, err) {
            console.log(xhr, err)
        }
    })
}
LoadMessage()
