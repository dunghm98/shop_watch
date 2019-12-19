$(document).ready(function () {
    var actionNav = $('.active-nav').offset();
    $(window).bind('scroll', function () {
        //console.log($(window).height());
        // console.log($(window).scrollTop());
        //console.log(actionNav.top);
        // var navHeight = $(window).height() - 102;$('.active-nav').height() + 
        // console.log(navHeight);
        var fixedPoint = $(window).scrollTop() - actionNav.top;
        if (fixedPoint >= 0) {
            $('.active-nav').addClass('fixed');
            $('.active-nav').removeClass('ml-3 mr-3');
        }
        else {
            $('.active-nav').removeClass('fixed');
            $('.active-nav').addClass('ml-3 mr-3');
        }
    });
});