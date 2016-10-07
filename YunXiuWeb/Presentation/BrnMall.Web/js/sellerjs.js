$(".purchase").slide({
    mainCell:".center-l ul",
    autoPlay:true,
    effect:"leftMarquee",
    vis:5,interTime:40,
    trigger:"click"
});

$(".adv-item").mouseover(function(){
	$(this).find(".adv-conten img").slideUp("slow");
});
$(".adv-item").mouseleave(function(){
	$(this).find(".adv-conten img").slideDown("slow");
});


$(document).ready(function(){
	$(".profit").waypoint(function(){
		$(".pbc").animate({width:'100%'},1500);
	},{offset:'50%'});
	$(".profit").waypoint(function(){
		$(".pb2").animate({width:'70%'},1500);
	},{offset:'50%'});

	$("#choiceBanner").waypoint(function(){
		var sumWidth =0;
		sumWidth += $(document).width()/2;
		sumWidth += 234;
		// sumWidth += $(".choicetxt").width()*0.40;
		$(".choicetxt").animate({right: sumWidth},1000);
	},{offset:'40%'});
});