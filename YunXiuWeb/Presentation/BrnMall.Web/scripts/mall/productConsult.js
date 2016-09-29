$(document).ready(function () {
    $(".submit-consultation").on("click", function () {
        var typeID = $("radio[name=consultTypeId]").val();
        var content = $(".consultation-content").val();
        $.post("/Catalog/AddConsultation",
            {
                typeID: typeID,
                content: content
            },
            function (data) {
                if (data == "1") {
                    alert("成功");
                } else if (data == "-1") {
                    alert("请登录");
                } else {
                    alert("添加失败");
                }
            });
    });

});


