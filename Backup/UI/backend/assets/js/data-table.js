var selectObjects = [];
var chkAll = $('#chkAll');
$(document).ready(function () {
    // Check on tr
    $(document).on('click', 'tr[scope="data-row"]' , function () {
        let _row = $(this);
        let chkBox = _row.find('input.data-checkbox');
        if (chkBox.is(':checked')) {
            chkBox.prop("checked", false);
            selectObjects = arrayRemove(selectObjects, chkBox.val());
        } else {
            chkBox.prop("checked", true);
            selectObjects.push(chkBox.val());
        }
        if (selectObjects.length > 0) {
            chkAll.prop("checked", true);
        } else {
            chkAll.prop("checked", false);
        }
        $('label[scope="row-selected"]').text(selectObjects.length);
    });
    chkAll.on('change', function () {
        if ($(this).is(':checked')) {
            $.each($('input.data-checkbox'), function (){
                $(this).prop('checked', true);
                selectObjects.push($(this).val());
            });
        } else {
            $.each($('input.data-checkbox'), function (){
                var checkedRow = $(this);
                checkedRow.prop('checked', false);
                selectObjects = arrayRemove(selectObjects, checkedRow.val());
            });
        }
        reloadSelectedLabel();
    });
});

// Remove element
function arrayRemove(arr, value) {
    return arr.filter(function(ele) {
        return ele != value;
    });
}
// reload label 
function reloadSelectedLabel() {
    $('label[scope="row-selected"]').text(selectObjects.length);
}