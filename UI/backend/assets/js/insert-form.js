// lick vào thanh thêm mới
$(document).on('click', '.insert-form', function () {
    test($(this));
});

function test(selector) {
    var thisCtrl = selector;
    thisCtrl.toggleClass('collapsed-card');
    thisCardToolIcon = thisCtrl.children('.card-tools').children().children();
    thisCardBody = thisCtrl.next();
    if (thisCardToolIcon.hasClass('fa-plus')) {
        thisCardToolIcon.removeClass('fa-plus').addClass('fa-minus');
        thisCardBody.slideToggle(300);
    } else {
        thisCardToolIcon.removeClass('fa-minus').addClass('fa-plus');
        thisCardBody.slideToggle(300);
    }
    console.log(thisCtrl.children('.card-tools').children().children());
}