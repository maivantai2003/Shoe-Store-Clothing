$(document).ready(function () {
    addBrand();
})

function loadBrands() {
    $.ajax({
        method: "get",
        dataType: "json",
        url: "/Admin/Brand/Index",
        success: function (response) {
            var html = ``;
        }
    })
}
function addBrand() {
    $(document).odd("click", "#submit_Add_Brand", function () {
        var brandName = $("#BrandName").val();
        $.ajax({
            url: "/Admin/Brand/Create",
            method: "post",
            contentType: "application/json",
            data: JSON.stringify({
                brandName: brandName
            }),
            dataType: "json",
            success: function (response) {

            }
        })
    })
}