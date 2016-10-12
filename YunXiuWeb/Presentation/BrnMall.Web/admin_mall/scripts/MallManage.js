$(document).ready(function () {
    $(".add-role").on("click", function () {
        var body = $(this).parents("body");
        var rName = $(body).find("input[name='RName']").val();
        var describe = $(body).find("input[name='Describe']").val();
        $.post("/MallManage/AddRolePost",
            {
                rName: rName,
                describe: describe
            },
            function (data) {
                if (data == "1") {
                    alert("添加成功");
                } else {
                    alert("添加失败");
                }
            });
    });

    $(".del-role").on("click", function () {
        if (confirm("确认删除")) {
            var tr = $(this).parents("tr");
            var rID = $(tr).attr("rid");
            $.post("/MallManage/DeleteRole",
                {
                    rID: rID
                },
                function (data) {
                    if (data == "1") {
                        alert("删除成功");
                        $(tr).remove();
                    } else {
                        alert("删除失败");
                    }
                });
        }
    });

    $(".edit-role").on("click", function () {
        var tr = $(this).parents("tr");
        var rID = $(tr).attr("rid");
        var rName = $(tr).attr("rName");
        var describe = $(tr).attr("describe");
        window.parent.frames[2].location.href = "/malladmin/MallManage/RoleUpdate?rID=" + rID + "&rName=" + rName + "&describe=" + describe
    });


    $(".edit-save").on("click", function () {
        var body = $(this).parents("body");
        var rID = $(body).find("input[name='rID']").val();
        var rName = $(body).find("input[name='rName']").val();
        var describe = $(body).find("input[name='describe']").val();

        $.post("/MallManage/RoleUpdatePost",
            {
                rID: rID,
                rName: rName,
                describe: describe
            },
            function (data) {
                if (data == "1") {
                    alert("修改成功");
                }
                else {
                    alert("修改失败");
                }
            });
    });


    $(".add-permission").on("click", function () {
        var body = $(this).parents("body");
        var pName = $(body).find("input[name='pName']").val();
        var pKey = $(body).find("input[name='pKey']").val();
        var describe = $(body).find("input[name='describe']").val();
        $.post("/MallManage/AddPermissionPost",
            {
                pName: pName,
                pKey: pKey,
                describe: describe
            },
            function (data) {
                if (data == "1") {
                    alert("添加成功");
                } else {
                    alert("添加失败");
                }
            });
    });

    $(".del-permission").on("click", function () {
        if (confirm("确认删除")) {
            var tr = $(this).parents("tr");
            var pID = $(tr).attr("pid");
            $.post("/MallManage/DeletePermission",
                {
                    pID: pID
                },
                function (data) {
                    if (data == "1") {
                        alert("删除成功");
                        $(tr).remove();
                    } else {
                        alert("删除失败");
                    }
                });
        }
    });
    $(".edit-permission").on("click", function () {
        var tr = $(this).parents("tr");
        var pID = $(tr).attr("pID");
        var pName = $(tr).attr("pName");
        var pKey = $(tr).attr("pKey");
        var describe = $(tr).attr("describe");
        window.parent.frames[2].location.href = "/malladmin/MallManage/UpdatePermission?pID=" + pID + "&pName=" + pName + "&pKey=" + pKey + "&describe=" + describe
    });


    $(".permission-edit-save").on("click", function () {
        var body = $(this).parents("body");
        var pID = $(body).find("input[name='pID']").val();
        var pName = $(body).find("input[name='pName']").val();
        var pKey = $(body).find("input[name='pName']").val();
        var describe = $(body).find("input[name='describe']").val();

        $.post("/MallManage/UpdatePermissionPost",
            {
                pID: pID,
                pName: pName,
                pKey: pKey,
                describe: describe
            },
            function (data) {
                if (data == "1") {
                    alert("修改成功");
                }
                else {
                    alert("修改失败");
                }
            });
    });

    $(".permission-select").on("click", function () {
        var action = "";
        if ($(this).is(':checked')) {
            action = "AddRolePermission";
        } else {
            action = "DeleteRolePermission";
        }
        var body = $(this).parents("body");
        var rID = $(body).find("input[name='rID']").val();
        var pID = $(this).val();
        $.post("/MallManage/" + action,
            {
                rID: rID,
                pID: pID
            },
            function (data) {
                if (data == "-1") {
                    alert("修改失败");
                }
            });

    });
});