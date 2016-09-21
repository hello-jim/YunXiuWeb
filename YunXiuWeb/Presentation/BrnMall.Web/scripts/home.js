$(document).ready(function () {
    $(".searchbtn").on("click", function () {
        var key = $("#search-key").val();
        mallSearch(key);
    });
});