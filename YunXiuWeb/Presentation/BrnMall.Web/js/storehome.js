$(function(){
	$(".shoucang").click(function(){
		$(this).toggleClass("shouchangitem");//切换图标
	});
});

$(function(){
	$(".left-nav h3").click(function(){
		$(this).addClass("nav-current").siblings().removeClass("nav-current"); //切换选中的按钮高亮状态
		var index=$(this).index(); //获取被按下按钮的索引值，需要注意index是从0开始的
		$(".right-conten > .contenbox").eq(index).show().siblings().hide(); 
	});
});