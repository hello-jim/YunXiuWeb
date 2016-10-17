// 常见问题分类导航栏效果
$(function(){
	//$(".coll_body").eq(0).show();
	$(".Collapsing").click(function(){
		$(this).toggleClass("current").siblings('.Collapsing').removeClass("current");//切换图标
		$(this).next(".coll_body").slideToggle(500).siblings(".coll_body").slideUp(500);
	});
});

// 热点问题切换效果
$(function(){
	$(".hot-item").mouseover(function(){
		$(this).addClass("hot-active").siblings(".hot-item").removeClass("hot-active");
	});
});

// 页面右侧内容收缩效果
$(function(){
	$(".litem-conten").eq(0).show();
	$(".litemtitle").click(function(){
		$(this).toggleClass("titleactive").siblings('.litemtitle').removeClass("titleactive");//切换图标
		$(this).next(".litem-conten").slideToggle(500).siblings(".litem-conten").slideUp(500);
	});
});