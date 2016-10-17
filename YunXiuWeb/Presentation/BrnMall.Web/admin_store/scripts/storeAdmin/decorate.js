
if ($("#dropzone-upload").length == 1) {
    $("#dropzone-upload").dropzone({ url: "/", addRemoveLinks: true });
}

$(document).ready(function () {
    $(".goods-info dt a").each(function () {
        var maxwidth = 27;
        if ($(this).text().length > maxwidth) {
            $(this).text($(this).text().substring(0, maxwidth));
            $(this).html($(this).html() + '…');
        }
    });
    $(".store_decoration_area").sortable({ scroll: true, scrollSensitivity: 40, tolerance: 'pointer' });
    $(".store_decoration_area").disableSelection();

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
            $("#product-div .select-product-list").html($(e.relatedTarget).prev().find("ul").html());
            $("#product-div .select-product-list .goods-info").after("<a href='javascript:void(0);' class='ncbtn-mini product-select-list-delete'><i class='icon-remove'></i></a>");
            BindProductListDeleteClick();
        } else {
            //不是新增编辑则清空内容
            $(".select-product-list").html("");
        }
    });

    $("#images-div").on('show.bs.modal', function (e) {
        var isEdid = $(e.relatedTarget).attr("isEdit");
        if (isEdid != undefined) {
            $("#images-div").attr("edit", "");
            $("#dropzone-upload .dz-preview").remove();
            $(".edit-img-div").html("");
            $(".edit-img-div").show();
            var images = $(e.relatedTarget).prev().prop("outerHTML");
            $(".edit-img-div").html(images);
            $(".edit-img-div").find(".pic_in").after("<a href='javascript:void(0);' class='edit-img-delete'><i class='fa fa-times'></i></a>");
            $(".edit-img-delete").on("click", function () {
                $(this).prev().remove();
                $(this).remove();
            });
        } else {
            $("#images-div").removeAttr("edit");
            $("#dropzone-upload").show();
            $("#dropzone-upload .dz-preview").remove();
            $("#dropzone-upload .dz-message").show();
            $(".edit-img-div").hide();
        }
    });

    $(".modal").on("hide.bs.modal", function () {
        $("#dialog_select").hide();
    });

    $(".modal").on("show.bs.modal", function (e) {
        var index = $(e.relatedTarget).parents(".ncsc-decration-block").attr("bindex");
        if (index != undefined) {
            $("#edit-block-id").val(index);
        }
    });

    $(".edit-html-save").on("click", function () {
        var bIndex = $("#edit-block-id").val();
        $("div[bIndex=" + bIndex + "] .store-decoration-block-1-module").html($(".edit-html").html());
        $("div[bIndex=" + bIndex + "]").attr("editType", "editHtml");
        $("div[bIndex=" + bIndex + "] .ncsc-decration-block-content .editbc").unbind("click").attr({ "data-toggle": "modal", "data-target": "#edit-html-div", "isEdit": "1" }).removeClass("editbc");
    });

    $(".store-home-save").on("click", function () {
        var storeHomeHtml = encodeURIComponent($(".store_decoration_area").html());
        $.post("/storeAdmin/Store/UpdateStoreHome",
        {
            html: storeHomeHtml
        }, function (data) {
            if (data == "1") {
                alert("修改成功");
            }
        });
    });

    EditBlockEvent();
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
$("a.block-add").on("click", function () {
    cname = $(this).children("i").attr("class");
    var btitle = "模块内容";
    if (cname == "images-div") {
        btitle = "图片/轮播图模块";
    } else if (cname == "product-div") {
        btitle = "商品模块";
    } else { btitle = "自定义编辑模块"; };

    var maxIndex = parseInt(GetMaxBlockIndex());
    var block = "";
    block += "<div id='block-" + (maxIndex + 1) + "' class='ncsc-decration-block container no-padding tip' bIndex='" + (maxIndex + 1) + "'>";
    block += "<div class='ncsc-decration-block-content store-decoration-block-1-content'>";
    block += "<div class='header_edit'>"+ btitle +"</div> "
    block += "<div class='store-decoration-block-1-module' nctype='store_decoration_block_module'>"
    block += "</div>"
    block += "<a class='edit editbc' nctype='btn_edit_module' href='javascript:void(0);'data-toggle='modal' data-target='#" + cname + "' isedit='1'><i class='icon-edit'></i></a>";
    block += "</div>";
    block += "<a class='block-delete' href='javascript:void(0);' data-block-id='122' title='删除该布局块'><i class='icon-trash'></i></a>"
    //block += "<a class='block-move' href='javascript:void(0);' title='移动'><i class='icon-move'></i></a>"
    block += "</div>";

    $(".store_decoration_area").sortable({ scroll: true, scrollSensitivity: 40, tolerance: 'pointer' });
    $(".store_decoration_area").disableSelection();

    $(".store_decoration_area").append(block);
    EditBlockEvent();
});

// 编辑模块点击事件
function EditBlockEvent() {
    $(".editbc").unbind("click").on("click", function () {
        var blockID = $(this).parents(".ncsc-decration-block").attr("bIndex");
        $("#edit-block-id").val(blockID);
        //$("#dialog_select").css("display", "block");
    });

    //$(".ncsc-decration-block").unbind("mouseover").on("mouseover", function () {
    //    $(this).children(".block-move").show();
    //});
    //$(".ncsc-decration-block").unbind("mouseleave").on("mouseleave", (function () {
    //    $(this).children(".block-move").hide();
    //}));

    $(".block-delete").on("click", function () {
        $(this).parents(".ncsc-decration-block").remove();
    });
}

//店铺商品搜索
$("#btn_module_goods_search").unbind("click").on("click", function () {
    var storeID = 1;
    var searchKey = "";
    $.post("/storeadmin/Store/GetStoreProduct",

        function (data) {
            var json = $.parseJSON(data);
            CreateProductListHtml(json);

        });
});

$(".product-list-save").unbind("click").on("click", function () {
    var bIndex = $("#edit-block-id").val();
    $("#product-div .select-product-list .product-select-list-delete").remove();
    $("div[bIndex=" + bIndex + "] .store-decoration-block-1-module").html($("#product-div .select-product-list").parent().html());
    $("div[bIndex=" + bIndex + "]").attr("editType", "editPruduct");
    $("div[bIndex=" + bIndex + "] .ncsc-decration-block-content .editbc").unbind("click").attr({ "data-toggle": "modal", "data-target": "#product-div", "isEdit": "1" });
});


$(".save-images").unbind("click").on("click", function () {
    var imagesHtml = "";
    var bIndex = $("#edit-block-id").val();
    var edit = $("#images-div").attr("edit");
    if (edit != undefined) {
        var picEdit = $(".edit-img-div .store-decoration-block-1-module .pic_in");
        for (var i = 0; i < picEdit.length; i++) {
            imagesHtml += "<div class='pic_in'>";
            imagesHtml += $(picEdit[i]).html();
            imagesHtml += "</div>";
        }
        var newImg = $("#dropzone-upload .dz-success .dz-image");
        for (var i = 0; i < newImg.length; i++) {
            imagesHtml += "<div class='pic_in'><h1>";
            //imagesHtml.push(newImg[i]);
            imagesHtml += $(newImg[i]).html();
            imagesHtml += "</h1></div>";
        }     
    } else {
        var uSuccesImages = $("#dropzone-upload .dz-success .dz-image");//上传成功图片
        for (var i = 0; i < uSuccesImages.length; i++) {
            imagesHtml += "<div class='pic_in'><h1>";
            imagesHtml += $(uSuccesImages[i]).html();
            imagesHtml += "</h1></div>";
        }
    }

    $("div[bIndex=" + bIndex + "] .store-decoration-block-1-module").html(imagesHtml);
    $("div[bIndex=" + bIndex + "]").attr("editType", "editImages");
    $("div[bIndex=" + bIndex + "] .ncsc-decration-block-content .editbc").unbind("click").attr({ "data-toggle": "modal", "data-target": "#images-div", "isEdit": "1" }).removeClass("editbc");
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
        html += "<li pID='" + products[i].PID + "' pName='" + products[i].Name + "' imgName='" + products[i].ImgName + "'>";
        html += "<h1>";
        html += "<a href='' title='" + products[i].Name + "'><img src='/images/productImg/" + products[i].ImgName + "'> </a>";
        html += "</h1>";
        html += "<dl class='goods-info'><dt>  <a href='' title='" + products[i].Name + "' pID='" + products[i].PID + "'>" + products[i].Name + "</a></dt><dd>" + products[i].ShopPrice + "</dd></dl>";
        html += "<a class='ncbtn-mini product-list-add' href='javascript:void(0);'><i class='icon-ok'></i></a>";
        html += "</li>";
    }
    $("#product-list").html(html);
    BindProductListAddClick();
}

$("#product-list li").click(function () {
    BindProductListAddClick();
    //$(".product-list-add").click();
})

function BindProductListAddClick() {
    $(".product-list-add").unbind("click").on("click", function () {
        var li = $(this).parents("li");
        if ($("#product-div .select-product-list li[pid=" + $(li).attr("pid") + "]").length == 0) {
            var liHtml = "";
            liHtml += "<li pid=" + $(li).attr("pID") + ">";
            liHtml += "<h1><a href='' title='" + $(li).attr("pName") + "'><img src='/images/productImg/" + $(li).attr("imgName") + "'> </a></h1>";
            liHtml += "<dl class='goods-info'><dt>  <a href='' title='" + $(li).attr("pName") + "' pid='" + $(li).attr("pID") + "'>" + $(li).attr("pName") + "</a></dt><dd>0</dd></dl>";
            liHtml += "<a href='javascript:void(0);' class='ncbtn-mini product-select-list-delete'><i class='icon-remove'></i></a>";
            liHtml += "</li>";

            $("#product-div .select-product-list").append(liHtml);
            BindProductListDeleteClick();
        }
    });
}
function BindProductListDeleteClick() {
    $(".product-select-list-delete").unbind("click").on("click", function () {
        $(this).parents("li").remove();
    });
}


