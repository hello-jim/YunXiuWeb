$.ajax({
    url: 'Home/index',
    dataType: 'json',
    success: function(data) {
        var items = [];
        $.each(data, function(key, val) {
            items.push('<li id="' + key + '">' + val + '</li>');
        });
        $('<ul/>', {
            'class': 'list',
            html: items.join('')
        }).appendTo('body');
    },
    statusCode: {
        404: function() {
            alert("没有找到相关文件~~");
        }
    }
});
