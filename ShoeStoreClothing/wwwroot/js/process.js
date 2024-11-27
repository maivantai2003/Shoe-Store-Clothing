"use strict"
var connection = new signalR.HubConnectionBuilder().withUrl("/Hub").build()

connection.start().catch(function (err) {
    return console.error(err.toString())
})
connection.on("ProcessOrder", function (id, status,dateTime) {
    var invoice = "#txtStatus-" + id;
    $(invoice).text("Status: " + status)
    $(invoice).css("color", "green");
    $("#statusComment").
    $("#notificationDropdown").append(`<div class="notification-item">
                        <h4 class="notification-title">Thông báo</h4>
                        <p class="notification-time">${dateTime}</p>
                        <p class="notification-content">Đơn hàng Order-${id} đã được xác nhận và sẽ được chuyển đi sớm nhất</p>
                    </div>`)

})
connection.on("LoadInvoice", function () {
    var hostname = window.location.pathname
    if (hostname === "/Admin/Invoice/Index") {
        location.reload()
    }
})
function LoadNotification() {
    $.ajax({
        url: "/Home/Notification",
        method: "get",
        success: function (response) {
            $("#notificationDropdown").empty()
            response.forEach(function (item) {
                $("#notificationDropdown").append(`<div class="notification-item">
                        <h4 class="notification-title">Thông báo</h4>
                        <p class="notification-time">${item.dateTimeSend}</p>
                        <p class="notification-content">${item.notificationContent}</p>
                    </div>`)
            })
        }
    })
}
LoadNotification()