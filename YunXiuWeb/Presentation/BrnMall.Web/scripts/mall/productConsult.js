$(document).ready(function () {
    $(".submit-consultation").on("click", function () {
        var typeID = $("radio[name=consultTypeId]").val();
        var content = $(".consultation-content").val();
        $.post("",
            {
                typeID: typeID,
                content: content
            },
            function (data) {
                if (data == "") {
                    alert("成功");
                } else {
                    alert("失败")
                }
            });
    });

});


function GetStarLevel(product){
    var goodStars = product.OneStar + TwoStar + ThreeStar;
    var allStars = goodStars + FourStar + FiveStar;

    if (allStars == 0)
        return 100;
    return goodStars * 100 / allStars;
}