$(function(){
	$(".coll_body").eq(0).show();
	$(".Collapsing").click(function(){
	    $(this).toggleClass("current").siblings('.Collapsing').removeClass("current");//切换图标
	    $(this).next(".coll_body").slideToggle(500).siblings(".coll_body").slideUp(500);
	});
});

$(".comment-input").artTxtCount($('.artcount'), 1000);