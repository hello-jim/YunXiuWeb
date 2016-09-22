//star

rat('star1','result1');
rat('star2','result2');
rat('star-prodes','result-prodes');
rat('star-sevice','result-sevice');
rat('star-express','result-express');
function rat(star,result){
    var star= '#' + star;
	var result= '#' + result;
    var stepW = 24;
    var description = new Array("失望","不满","一般，还过得去吧","满意，是我想要的东西","惊喜，满分");
    var stars = $(star).children("span");
    var descriptionTemp;
    $("#showb").css("width",0);
    stars.each(function(i){
        $(stars[i]).click(function(e){
            var n = i+1;
            //$("#showb").css({"width":stepW*n});
			$(this).closest("dd").find(".current-rating").css({"width":stepW*n});
            descriptionTemp = description[i];
            $(this).find('a').blur();
            $(result).attr("values",n);
            return stopDefault(e);
            return descriptionTemp;
        });
    });
    stars.each(function(i){
        $(stars[i]).hover(
            function(){
                $(result).text(description[i]);
                
            },
			
            function(){
                if(descriptionTemp != null)
                    $(result).text("当前您的评价为："+descriptionTemp);
                else 
                    $(result).text(" ");
            }
        );
    });
};
function stopDefault(e){
    if(e && e.preventDefault)
           e.preventDefault();
    else
           window.event.returnValue = false;
    return false;
};
