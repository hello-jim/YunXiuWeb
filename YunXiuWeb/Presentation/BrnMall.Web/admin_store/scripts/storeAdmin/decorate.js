$(function () {
    $("#sortable").sortable();
    $("#dropzone-upload").dropzone({ url: "/", addRemoveLinks: true });
    $('.edit-html').wysiwyg({ fileUploadError: showErrorAlert });
    window.prettyPrint && prettyPrint();
    $('#edit-html-div').on('show.bs.modal', function (e) {
        var isEdid = $(e.relatedTarget).attr("isEdit");
        if (isEdid) {
            $(".edit-html").html($(e.relatedTarget).prev().html());
        } else {
            $(".edit-html").html("");//不是新增编辑则清空内容
        }
    });
    $('#edit-html-div').on('shown.bs.modal', function (e) {
        $('[data-role=magic-overlay]').each(function () {
            initToolbarBootstrapBindings();
        });
    });

    $("#product-div").on('show.bs.modal', function (e) {
        var isEdid = $(e.relatedTarget).attr("isEdit");
        if (isEdid) {
            $("#product-div .select-product-list").html($(e.relatedTarget).prev().html());
            $("#product-div .select-product-list .goods-info").after("<a href='javascript:void(0);' class='ncbtn-mini product-list-delete'><i class='icon-ban-circle'></i>删除 </a>");
        } else {
            //不是新增编辑则清空内容
            $(".select-product-list").html("");
        }
    });



    $(".edit-html-save").on("click", function () {
        var bIndex = $("#edit-block-id").val();
        $("div[bIndex=" + bIndex + "] .store-decoration-block-1-module").html($(".edit-html").html());
        $("div[bIndex=" + bIndex + "]").attr("editType", "editHtml");
        $("div[bIndex=" + bIndex + "] .ncsc-decration-block-content .editbc").unbind("click").attr({ "data-toggle": "modal", "data-target": "#edit-html-div", "isEdit": "1" }).removeClass("editbc");
    });

    $(".store-home-save").on("click", function () {
        var storeHomeHtml =escape($(".store_decoration_area").html());
        $.post("/Home/AddStoreHome",
        {
            html:storeHomeHtml
        }, function (data) {
            alert(data);
        });
    });
});

function initToolbarBootstrapBindings() {
    var fonts = ['Serif', 'Sans', 'Arial', 'Arial Black', 'Courier',
          'Courier New', 'Comic Sans MS', 'Helvetica', 'Impact', 'Lucida Grande', 'Lucida Sans', 'Tahoma', 'Times',
          'Times New Roman', 'Verdana'],
          fontTarget = $('[title=Font]').siblings('.dropdown-menu');
    $.each(fonts, function (idx, fontName) {
        fontTarget.append($('<li><a data-edit="fontName ' + fontName + '" style="font-family:\'' + fontName + '\'">' + fontName + '</a></li>'));
    });
    $('a[title]').tooltip({ container: 'body' });
    $('.dropdown-menu input').click(function () { return false; })
        .change(function () { $(this).parent('.dropdown-menu').siblings('.dropdown-toggle').dropdown('toggle'); })
    .keydown('esc', function () { this.value = ''; $(this).change(); });

    $('[data-role=magic-overlay]').each(function () {
        var overlay = $(this), target = $(overlay.data('target'));
        overlay.css('opacity', 0).css('position', 'absolute').offset(target.offset()).width(target.outerWidth()).height(target.outerHeight());
    });
    if ("onwebkitspeechchange" in document.createElement("input")) {
        var editorOffset = $('.editor-html').offset();
        $('#voiceBtn').css('position', 'absolute').offset({ top: editorOffset.top, left: editorOffset.left + $('.editor-html').innerWidth() - 35 });
    } else {
        $('#voiceBtn').hide();
    }
};

function showErrorAlert(reason, detail) {
    var msg = '';
    if (reason === 'unsupported-file-type') { msg = "Unsupported format " + detail; }
    else {
        console.log("error uploading file", reason, detail);
    }
    $('<div class="alert"> <button type="button" class="close" data-dismiss="alert">&times;</button>' +
     '<strong>File upload error</strong> ' + msg + ' </div>').prependTo('#alerts');
};



//添加块
$("i.block-add").click(function () {
    var maxIndex = parseInt(GetMaxBlockIndex());
    var block = "";
    block += "<div id='block-" + (maxIndex + 1) + "' class='ncsc-decration-block store-decoration-block-1 tip' bIndex='" + (maxIndex + 1) + "'>";
    block += "<div class='ncsc-decration-block-content store-decoration-block-1-content'>";
    block += "<div class='store-decoration-block-1-module' nctype='store_decoration_block_module'></div>"
    block += "<a class='edit editbc' nctype='btn_edit_module' href='javascript:void(0);'><i class='icon-edit'></i>编辑模块 </a>";
    block += "</div>";
    block += "<a class='block-delete' href='javascript:void(0);' data-block-id='122' title='删除该布局块'><i class='icon-trash'></i>删除布局块</a>"
    block += "</div>";

    $(".store_decoration_area").sortable();
    $(".store_decoration_area").disableSelection();

    $(".store_decoration_area").append(block);

    $(".ncsc-decration-block").unbind("mouseover").on("mouseover", function () {
        $(this).children(".block-delete").show();
    });
    $(".ncsc-decration-block").unbind("mouseleave").on("mouseleave", (function () {
        $(this).children(".block-delete").hide();
    }));
    $(".block-delete").on("click", function () {
        $(this).parents(".ncsc-decration-block").remove();
    });
    // 编辑模块点击事件
    $(".editbc").unbind("click").on("click", function () {
        var blockID = $(this).parents(".ncsc-decration-block").attr("bIndex");
        $("#edit-block-id").val(blockID);
        $("#dialog_select").css("display", "block");
    });
});

//店铺商品搜索
$("#btn_module_goods_search").unbind("click").on("click", function () {
    var storeID = 1;
    var searchKey = "";
    $.post("/Home/GetProductByStore",
        {
            storeID: storeID
        },
        function (data) {
            var json = $.parseJSON(data);
            CreateProductListHtml(json);

        });
});

$(".product-list-save").unbind("click").on("click", function () {
    var bIndex = $("#edit-block-id").val();
    $("#product-div .select-product-list .product-list-delete").remove();
    $("div[bIndex=" + bIndex + "] .store-decoration-block-1-module").html($("#product-div .select-product-list").parent().html());
    $("div[bIndex=" + bIndex + "]").attr("editType", "editPruduct");
    $("div[bIndex=" + bIndex + "] .ncsc-decration-block-content .editbc").unbind("click").attr({ "data-toggle": "modal", "data-target": "#product-div", "isEdit": "1" });
});


$(".save-images").unbind("click").on("click", function () {
    var imagesHtml = "";
    var bIndex = $("#edit-block-id").val();
    var uSuccesImages = $("#dropzone-upload .dz-success");//上传成功图片
    for (var i = 0; i < uSuccesImages.length; i++) {
        imagesHtml += $(uSuccesImages[i]).find(".dz-image").html();
    }
    $("div[bIndex=" + bIndex + "] .store-decoration-block-1-module").html(imagesHtml);
    $("div[bIndex=" + bIndex + "]").attr("editType", "editImages");
    $("div[bIndex=" + bIndex + "] .ncsc-decration-block-content .editbc").unbind("click").attr({ "data-toggle": "modal", "data-target": "#edit-html-div", "isEdit": "1" }).removeClass("editbc");
});

// 删除按钮点击事件
$("a.delete").click(function () {
    $(this).parents(".ncsc-decration-block").css("display", "none");
});



$("i.slide").click(function () {
    $("#dialog_select").css("display", "none");
    $("#dialog_module_slide").css("display", "block");
});
$("i.hotarea").click(function () {
    $("#dialog_module_hot_area").css("display", "block");
    $("#dialog_select").css("display", "none");
});
$("i.goods").click(function () {
    $("#product-div").css("display", "block");
    $("#dialog_select").css("display", "none");
});
$("i.html").click(function () {
    $("#dialog_module_html").css("display", "block");
    $("#dialog_select").css("display", "none");
});

function GetMaxBlockIndex() {
    var maxIndex = 0;
    var cObj = $(".store_decoration_area").children();
    if (cObj.length !== 0) {
        var sortObj = cObj.sort(SortBlockIndex);
        maxIndex = $(sortObj[0]).attr("bIndex");
    }
    return maxIndex;
}
function SortBlockIndex(a, b) {
    var aBlock = parseInt($(a).attr("bIndex"));
    var bBlock = parseInt($(b).attr("bIndex"));
    return aBlock > bBlock ? -1 : 1;
}

function CreateProductListHtml(products) {
    var html = "";
    for (var i = 0; i < products.length; i++) {
        html += "<li>";
        html += "<div class='goods-thumb'>";
        html += "<a href='' title='" + products[i].Name + "'><img src='images/kefu.png'> </a>";
        html += "</div>";
        html += "<dl class='goods-info'><dt>  <a href='' title='" + products[i].Name + "' pID='" + products[i].PID + "'>" + products[i].Name + "</a></dt><dd>" + products[i].ShopPrice + "</dd></dl>";
        html += "<a class='ncbtn-mini product-list-add' href='javascript:void(0);'><i class='icon-ban-circle'></i>添加 </a>";
        html += "</li>";
    }
    $("#product-list").html(html);
    BindProductListAddClick();
}

function BindProductListAddClick() {
    $(".product-list-add").unbind("click").on("click", function () {
        var li = $(this).parents("li");
        $(li).find(".ncbtn-mini").addClass("product-list-delete").removeClass("product-list-add").html("删除");
        $(li).appendTo("#product-div .select-product-list");
        BindProductListDeleteClick();
    });
}

function BindProductListDeleteClick() {
    $(".product-list-delete").unbind("click").on("click", function () {
        $(this).addClass("product-list-add").removeClass("product-list-delete").html("添加");
        $(this).parents("li").appendTo("#product-list");
        BindProductListAddClick();
    });
}