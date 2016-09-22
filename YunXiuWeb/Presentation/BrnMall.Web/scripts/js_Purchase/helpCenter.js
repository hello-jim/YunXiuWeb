$(function(){    
    /*public*/
    (function(){
        var windowHeight=$(window).height();
        var containerHeight=$("#leftmenu").height();
        if(windowHeight-containerHeight>380){
            $("#leftmenu").css("min-height",windowHeight-380);
        }
    })();    
	$("li>h5",".foldList").bind("click",function(){
	    var li=$(this).parent();
		if(li.hasClass("fold")){
			li.removeClass("fold");
			$(this).find("b").removeClass("UI-bubble").addClass("UI-ask");
			li.find(".foldContent").slideDown();
		}else{
			li.addClass("fold");
			$(this).find("b").removeClass("UI-ask").addClass("UI-bubble");
			li.find(".foldContent").slideUp();
		}
	});
	$("p>a",".foldContent").bind("click",function(){
		var ab=$(this);
		if(ab.hasClass("current")){
			
		}else {
			ab.addClass("current");
			ab.parent().siblings("p").find("a").removeClass("current");
			ab.parents("li").siblings("li").find("a").removeClass("current");
		}
	});
})