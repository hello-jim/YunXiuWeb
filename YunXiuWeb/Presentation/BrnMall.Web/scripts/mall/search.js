$(document).ready(function () {
    $(".collect-product").unbind("click").on("click", function () {
        var pID = $(this).parents("li").attr("pID");
        $.post("/Catalog/AddFavoriteProduct",
            {
                pID: pID
            },
            function (data) {
                if (data == "1") {
                    alert("收藏成功");
                }          
            });
    });

    $(".add-shopping-cart").unbind("click").on("click", function () {
        var pID = $(this).parents("li").attr("pID");
        $.post("/Catalog/AddToShoppingCart",
            {
                pID: pID
            },
            function (data) {
                if (data == "1") {
                    alert("添加成功")
                }
            });
    });
});