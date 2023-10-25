window.onload = function () {
    HideLoader();
}
function GetProduct(val,searchVal) {
    $.ajax({
        url: '/Home/ProductClasses',
        method: "post",
        datatype: "json",
        headers: {
            "RequestVerificationToken": $('input[name = __RequestVerificationToken]').val()
        },
        data: { categ_id: '' + val + '', search: '' + searchVal + '' },
        success: function (response) {
            $("#prod-card").html(response);
            HideLoader();
        },
        error: function (error) {
            console.log("operation failed...", error);
        }
    });
}
function GetProductsAsync(id, $this) {
    var val_Search = '';
    if ($this.nextElementSibling.nodeName === "INPUT") {
        var val_Search = $this.nextElementSibling.value;
    }
    ShowLoader();
    DeleteProductSection();
    setTimeout(GetProduct, 2000, id, val_Search);
}
function DeleteProductSection() {
    const proCard = document.getElementById("prod-card");
    while (proCard.firstChild) {
        proCard.removeChild(proCard.firstChild);
    }
}
function ShowLoader() {
    const getLoaderClass = document.getElementById("mainLoader");
    getLoaderClass.style.display = 'block';
}
function HideLoader() {
    const getLoaderClass = document.getElementById("mainLoader");
    getLoaderClass.style.display = 'none';
}
