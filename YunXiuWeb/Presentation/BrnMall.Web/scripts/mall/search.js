$(document).ready(function () {
    $(".collect-product").unbind("click").on("click", function () {
        var pID = $(this).parents("li").attr("pID");
        addProductToFavorite(pID);
    });

    $(".add-shopping-cart").unbind("click").on("click", function () {
        var pID = $(this).parents("li").attr("pID");
        addProductToCart(pID,1);
    });
});