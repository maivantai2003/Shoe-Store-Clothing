"use strict"
var connection = new signalR.HubConnectionBuilder().withUrl("/Hub").build()
connection.start().catch(function (err) {
    return console.error(err.toString())
})
function loadComment(id) {
    //var id = $("#productDetailId").val()
    $("#renderComment").empty()
    $.ajax({
        url: "/Comment/GetAllCommentProduct",
        method: "get",
        data: {
            productDetailId: id
        },
        dataType: "json",
        success: function (response) {
            response.data.forEach((item, index) => {
                const commentDate = new Date(item.createComment);
                $("#renderComment").append(
                    `
                                <div class="d-flex">
                        <img src="" alt="UserName" class="user-image" style="width: 50px; height: 50px; border-radius: 50%;">
                        <div class="comment-content ml-3">
                            <h5>${item.user.fullName}</h5>
                            <p>${item.commentText}</p>
                                     <small>${commentDate.toLocaleString('en-GB')}</small>
                            
                        </div>
                    </div>
                        `
                )
            });
        }, error: (xhr, status, error) => {
            console.log(error)
        }
    })
}
var id = $("#productDetailId").val()
if (id !== undefined || id !== null) {
    loadComment(id)
}
connection.on("LoadComment", function (id) {
    loadComment(id)
})