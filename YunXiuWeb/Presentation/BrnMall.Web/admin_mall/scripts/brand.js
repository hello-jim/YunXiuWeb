function setSelectedCategory(selectedCateId, selectedCateName) {
    $(openCategorySelectLayerBut).parent().find(".CateId").val(selectedCateId);
    $(openCategorySelectLayerBut).val(selectedCateName).parent().find(".CategoryName").val(selectedCateName);
    $.jBox.close('categorySelectLayer');

    if (productPageType == 0 || productPageType == 3) {
        $("#commonAttributeTable,#skuAttributeTable").find("tr:not('.keepTr')").remove();

        $.get("/malladmin/category/aandvjsonlist?cateId=" + selectedCateId + "&time=" + new Date(), function (data) {
            var result = eval("(" + data + ")");

            if (productPageType == 0) {//添加商品页面时
                $("#commonAttributeTable").prepend(buildCommonAttrSelectTable(result));
            }
            else if (productPageType == 3) {//添加sku页面时
                $("#addSkuBut").before(buildSKUAttrSelectTable(result));
            }
        });
    }
}