function onFocusSearchBox() {
    var thisBtn = $(event.target);
    // var searchBox = thisBtn.closest('.search-form').find('#search');
    thisBtn.attr('placeholder', 'Nhập tên sản phẩm cần tìm');
}
function onBlurSearchBox() {
    var thisBtn = $(event.target);
    thisBtn.attr('placeholder', 'Tìm');
}

// Show loading ****
    $(document).ajaxStart(function () {
        $('#loaderSpinner').children().removeClass('fab').removeClass('fa-searchengin').addClass('fas fa-stroopwafel fa-spin');
    });
    //
    $(document).ajaxStop(function () {
        $('#loaderSpinner').children().addClass('fab').addClass('fa-searchengin').removeClass('fas fa-stroopwafel fa-spin');
    });
    // ./End