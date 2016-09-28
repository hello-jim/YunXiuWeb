childrenReview = new Array();
$(document).ready(function () {
    var pID = $("#pID").val();
    $.post("/Catalog/GetProductReview",
    {
        pID: pID
    },
    function (data) {
        var json = $.parseJSON(data);
        var sortJson = json.sort(OrderByDate);
        $("#productReviewJson").val(JSON.stringify(sortJson));
        CreateProductReview(sortJson, 0, 10);
    });
});


//创建所有评论
function CreateProductReview(json, index, count) {

    var fList = $.grep(json, function (value) {
        return value.Parent == 0;//筛选出父评论
    });
    var nList = GetListByIndex(fList, index, count);
    for (var i = 0; i < nList.length; i++) {
        //商品信息
        var html = "<div class='box-conten row' >";
        html += "<div class='right-conten col-md-2'>";
        html += "<div class='star star4'></div>";
        html += "<div class='day-item'>下单3天后评论</div>";
        html += "<div class='time-item'>2016-09-01 11:51</div>";
       // html += "<div class='feature'>";
       // html += "<ul><li>"+nList[i].RProduct.Name+"</li></ul>";
       // html += "</div>";
        html += "</div>";

        ///评论
        html += "  <div class='mid-conten col-md-7 clearfix'>";
        html += "<div class='comment'>";
        html += "<span>" + nList[i].RUser.UID + "</span>:";
        html += nList[i].RContent;
        html += "</div>";
        html += "<div class='otherFeature'></div>";
        html += "<div class='comment-operate'>";
        // html += "<a href='javascript:void(0);'>点赞（<span>2</span>）</a>";暂时不用
        html += "<a href='javascript:void(0);' class='reply'>回复（<span class='reply-count'></span>）</a>";
        html += "</div>";
        html += "<div class='reply-textarea clearfix reply_input' >";
        html += "<div class='inner'>";
        html += "<textarea class='reply-input' rID=" + nList[i].RID + " oID=" + (nList[i].ROrder != null ? nList[i].ROrder.OID : 0) + "></textarea>";
        html += "<div class='operate-box'> <span></span>   <a href='javascript:void(0);' class='reply-submit'>提交</a> </div>";
        html += "</div>";
        html += "</div>";
        html += "<div class='comment-replylist'>";
        html += GetReplyList(json, nList[i].RID);
        html += "</div>"
        html += "</div>"

        //客户信息
        html += "<div class='left-conten col-md-3'>";
        html += "<div class='user-item'>";
        html += "<img src='images/b56.gif'>";
        html += "<span class='username'>"+nList[i].RUser.UID+"</span>";
        html += "</div>";
        html += "<div class='type-item'>";
        html += "<span class='vip-level'></span>";
        html += "</div>";
        html += "<div class='user-item'>";
        html += "<span class='user-access'><a href='javascript:void(0);'></a> </span>";
        html += "</div>";
        html += "</div>";
        html += "</div>";
        html += "<div class='row row-hight'></div>";
        var doc = $(html);
        $(doc).find(".reply-count").html(childrenReview.length);
        html = doc;
        $(".all-review-list").append(html);
    }
    if (nList.length > 0) {
        $('div.box-conten').mouseover(function () {
            $(this).find('.comment-operate').css("visibility", "visible");
        });
        $('div.box-conten').mouseleave(function () {
            $(this).find('.comment-operate').css("visibility", "hidden");
        });
        $('.comment-operate .reply').click(function () {
            $(this).parents(".comment-operate").next(".reply_input").toggle();;
            $(this).parents(".comment-operate").nextAll(".comment-replylist").find(".reply-item").toggle();
        });
        $('.reply-input').artTxtCount($('.reply-input').next().find('span'), 120);
    }

    $(".reply-submit").unbind("click").on("click", function (e) {
        var pID = $("#pID").val();
        var obj = $(this).parents(".operate-box").prev();
        var content = $(obj).val();
        var parent = $(obj).attr("rid");
        var oID = $(obj).attr("oid")
        $.post("/Catalog/AddProductReview",
            {
                pID: pID,
                parent: parent,
                content: content,
                oID: oID
            },
            function (data) {
                if (data == "") {
                    alert("评论成功");
                } else if (data == "-1") {
                    alert("评论失败");
                } else {
                    alert("请登录");
                }
            })
    });

}

//评价日期排序
function OrderByDate(a, b) {
    var dt = new Date(a.ReviewTime);
    var dt2 = new Date(b.ReviewTime);
    return dt > dt2 ? -1 : 1;
}

function OrderByDateDesc(a, b) {
    var dt = new Date(a.ReviewTime);
    var dt2 = new Date(b.ReviewTime);
    return dt > dt2 ? -1 : 1;
}

//根据索引获取指定数量的集合
function GetListByIndex(list, index, count) {
    var nlist = new Array();
    if (list.length > 0) {
        if (list.length < count) {
            count = list.length;
        }
        for (var i = index; i < count; i++) {
            nlist.push(list[i]);
        }
    }
    return nlist;
}

//获取回复评价
function GetReplyList(json, pID) {
    var html = "";
    childrenReview.length = 0;
    GetChildrenReview(json, pID);
    if (childrenReview.length > 0) {
        var nList = childrenReview.sort(OrderByDateDesc);
        for (var j = 0; j < nList.length; j++) {
            var storeReply = "";
            if (nList[j].IsStoreReply) {
                storeReply = "store-reply";
            }

            html += "<div class='reply-item " + storeReply + "'>";
            html += "<div class='reply-info'>";
            var parent = GetParentReview(childrenReview, nList[j].Parent);
            if (parent != null) {
                html += "<span class='user-name colorblue'>" + nList[j].RUser.UID + "</span>" + " 回复 " + "<span class='user-name colororange'>" + parent.RUser.UID + "</span>:";
            } else {
                html += "<span class='user-name colororange'>" + nList[j].RUser.UID + "</span>:";
            }
            html += nList[j].RContent;//评论内容    
            html += "</div>";
            html += "<div class='comment-operate'>";
            html += "<a href='javascript:void(0)' class='reply'>回复</a>";
            html += "</div>";
            html += "<div class='reply-textarea txtarea clearfix reply_input'>";
            html += "<div class='inner'>";
            html += "<textarea class='reply-input' rID=" + nList[j].RID + "></textarea>";
            html += "<div class='operate-box'>";
            html += "<span></span>";
            html += "<a href='javascript:void(0);' class='reply-submit'>提交</a>";
            html += "</div>";
            html += "</div>";
            html += "</div>";
            html += "</div>";
        }
    }

    return html;
}

//获取子评论
function GetChildrenReview(json, pID) {
    var fList = $.grep(json, function (value) {
        return value.Parent == pID;//筛选出父评论
    });
    if (fList.length > 0) {
        for (var i = 0; i < fList.length; i++) {
            childrenReview.push(fList[i]);
            GetChildrenReview(json, fList[i].RID);
        }
    }
}

function GetParentReview(reviews, parent) {
    var fList = $.grep(reviews, function (value) {
        return value.RID == parent;
    });
    return fList[0];
}




