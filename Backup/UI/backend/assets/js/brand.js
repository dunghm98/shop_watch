const ENABLE = 1;
const DISABLE = 0;
var currentPageInput= $('input#currentPageInput');
var totalPageInput = $('input#totalPageInput');
var pageSizeDOM = $('select.page-size-select');
var searchBox = $('#searchBox');

$(document).ready(function() {
    fetchBrands(1,parseInt(pageSizeDOM.val()),searchBox.val());

    // Click delete product
    $(document).on('click', '._delete', function () {
        var btnDelete = $(this);
        var brandId = btnDelete.attr('data-value');
        var row = /*(event.target) OR */btnDelete.closest('tr[scope="data-row"]');
        var data = { id : brandId };
        Swal.fire({
            title: 'Nhắc nhẹ?',
            text: "Bạn có muốn xoá nhãn hiệu này không?!",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#28a745',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ừm!',
            cancelButtonText: 'Không',
            showClass: {
                popup: 'animated fadeInDown lower'
            },
            hideClass: {
                popup: 'animated fadeOutUp lower'
            }
        }).then((rs)=> {
            if (rs.value) {
                $.ajax({
                    type: "POST",
                    url: "/backend/WebService.asmx/DeleteABrand",
                    data: JSON.stringify(data),
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: onSuccess,
                    failure: function (response){
                        console.log(response);
                    }
                });
                function onSuccess(response) {
                    response = response.d;
                    if (response.statusCode == 200) {
                        smallTopConnerAlert(response.message);
                        fetchBrands(parseInt(currentPageInput.val()),parseInt(pageSizeDOM.val()),searchBox.val());
                        selectObjects = arrayRemove(selectObjects, brandId);
                        reloadSelectedLabel();
                        // row.remove();
                    }else {
                        errorAlert(response.message);
                    }
                }
            }
        });
    });

    // Searching
    $(document).on('change paste keyup', '#searchBox', function () {
        selectObjects = [];
        reloadSelectedLabel();
        var searchBox = $(this);
        var searchText = searchBox.val().trim();
        fetchBrands(1,parseInt(pageSizeDOM.val()),searchText);
        
    });

    // Click to image
    $(document).on('click', 'a[data-toggle="modal"]', function () {
        var thisImg = $(this);
        var img = `<img src="${thisImg.find('img').attr('src')}" />`;
        if (!img.includes(`no-image.jpg`)) {
            img += `<button data-id="${thisImg.attr('data-id')}" id="delete_image" type="button"` +
                    ` class="btn btn-flat" title="Xoá ảnh này">`+
                    `<i class="fas fa-trash"></i></button>`;
        }
        $('#preview').html(img);
        $('#updateImg').attr('data-id', thisImg.attr('data-id'));
    });
    // update img 
    $(document).on('click', '#updateImg', function () {
        var thisUpdateBtn = $(this),
            previewImg = $('#preview'),
            imageDOM = previewImg.closest('.modal-body').find('input'),
            file,
            formData = new FormData();
        if (file = imageDOM[0].files[0]) {
            var fileType = file["type"],
                validImageTypes = [
                    "image/gif",
                    "image/jpeg",
                    "image/png",
                    "image/dib",
                    "image/jpeg",
                    "image/webp",
                    "image/svgz",
                    "image/tif",
                    "image/xbm",
                    "image/bmp",
                    "image/jfif",
                    "image/pjpeg",
                    "image/pjp",
                    "image/tiff"
                ];
            if ($.inArray(fileType, validImageTypes) > 0){
                formData.append('id', thisUpdateBtn.attr('data-id'));
                formData.append('image', file);
                $.ajax({
                    type: "post",
                    url: "/backend/UpdateBrandLogo.ashx",
                    data: formData,
                    async: false,
                    success: function (response) {
                        if (parseInt(response.statusCode) === 200){
                            smallTopConnerAlert(response.message);
                            previewImg.append(
                                `<button data-id="${thisUpdateBtn.attr('data-id')}" id="delete_image" type="button"` +
                                ` class="btn btn-flat" title="Xoá ảnh này">`+
                                `<i class="fas fa-trash"></i></button>`
                            );
                            imageDOM.val(``);
                            $.each($('a.product-thumbnail'), function () {
                                var thumbnailImg = $(this).attr('data-id'),
                                    updatedImg = thisUpdateBtn.attr('data-id');
                                if (thumbnailImg == updatedImg) {
                                    var thisProductThumbnail = $(this);
                                    var img = thisProductThumbnail.find('img');
                                    img.attr('src', response.data);
                                }
                            });
                        }
                        else {
                            errorAlert(response.message);
                        }
                    },
                    cache: false,
                    processData: false,
                    contentType: false
                });
            }
            else {
                normalAlert(`Hãy chọn file ảnh hợp lệ`);
            }
        } else {
            normalAlert(`Hãy chọn ảnh để tiến hành cập nhật`);
        }
    });

    // Delete image 
    $(document).on('click', '#delete_image', function (){
        var thisDelBtn = $(this);
        var thisPreviewImg = thisDelBtn.closest('#preview').find('img');
        // var formData = new FormData();
        var data = {
            id: thisDelBtn.attr('data-id')
        }
        Swal.fire({
            title: 'Nhắc nhẹ?',
            text: "Bạn có muốn xoá ảnh này không?!",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#28a745',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ừm!',
            cancelButtonText: 'Không',
            showClass: {
                popup: 'animated fadeInDown lower'
            },
            hideClass: {
                popup: 'animated fadeOutUp lower'
            }
        }).then((result) => {
            if (result.value) {
                $.ajax({
                    type: "POST",
                    url: "/backend/WebService.asmx/DeleteBrandImage",
                    data: JSON.stringify(data),
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: onSuccess,
                    failure: function (response){
                        console.log(response);
                    }
                });
                function onSuccess(response) {
                    response = response.d;
                    if (response.statusCode == 200) {
                        smallTopConnerAlert(response.message);
                        thisPreviewImg.attr('src', '/backend/assets/images/no-image.jpg');
                        // update thumbnail img
                        $.each($('a.product-thumbnail'), function () {
                            var thumbnailImg = $(this).attr('data-id'),
                                deletedImg = thisDelBtn.attr('data-id');
                                // find locate of deleted img product
                            if (thumbnailImg == deletedImg) {
                                var thisBrandThumbnail = $(this);
                                var img = thisBrandThumbnail.find('img');
                                img.attr('src', '/backend/assets/images/no-image.jpg');
                            }
                        });
                        thisDelBtn.remove();
                    }else {
                        errorAlert(response.message);
                    }
                }
            }
        });
    });
});

function fetchBrands(currentPage, pageSize, searchText) {
    var data = {
        currentPage: currentPage,
        pageSize: pageSize,
        searchText: searchText
    }
    $.ajax({
        type: "post",
        url: "/backend/WebService.asmx/FetchBrands",
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(data) {
            console.log(data);
            data = data.d;
            if (data.statusCode != 404) {
                var htmlContent = null;
                var dataContentDOM = $('tbody[scope="data-content"');
                var lstBrands = data.data.lstBrands;
                var pagination = data.data.pages;
                dataContentDOM.html(``);
                currentPageInput.val(pagination.currentPage);
                totalPageInput.val(pagination.totalPage);
                $.each(pageSizeDOM.find('option'), function() {
                    if ((parseInt($(this).val()) == pagination.pageSize)){
                        $(this).prop('selected', true);
                    }
                });
                console.log(lstBrands);
                if (lstBrands.length > 0){
                    // duyệt mảng
                    $.each(lstBrands, function(index, val) {
                        htmlContent = `<tr scope="data-row">
                        <td scope="checkbox">
                            <div class="pretty p-svg p-plain p-bigger p-smooth mr-0">
                                <input type="checkbox" class="data-checkbox display-none" id="chkPro_${val.Id}" value="${val.Id}" `;
                                if(selectObjects.includes(val.Id.toString())) {
                                    htmlContent += ` checked`;
                                }
                                htmlContent += `/>
                                <div class="state">
                                    <img class="svg" src="/backend/assets/images/icons/check-square-black.png" width="18px" />
                                    <label style="width: 18px; height: 18px" />
                                </div>
                            </div>
                        </td>
                        <td scope="brand-id">
                            ${val.Id}
                        </td>
                        <td scope="brand-name">
                            ${val.Name}
                        </td>
                        <td scope="brand-logo">
                            <div>
                                ${val.Logo == "" ? "<a data-id='"+val.Id+"' class=\"product-thumbnail\" data-toggle=\"modal\" data-target=\"#modal-default\"><img src='/backend/assets/images/no-image.jpg' /></a>" : "<a data-id='"+val.Id+"' class=\"product-thumbnail\" data-toggle=\"modal\" data-target=\"#modal-default\"><img src='"+val.Logo+"' /></a>"}
                            </div>
                        </td>
                        <td scope="brand-kol">
                            ${val.Kol}
                        </td>
                        <td>
                            <div class="row">
                                <a href="/admin/manage/brand/${val.Id}" class="btn btn-flat btn-info col-12" title="Chi tiết"><i class="fas fa-edit"></i></a>
                            </div>
                            <div class="row">
                                <button type="button" class="btn btn-flat btn-danger col-12 _delete" title="Xoá" data-value="${val.Id}">
                                    <i class="fas fa-trash"></i>
                                </button>
                            </div>
                        </td>
                    </tr>`;
                    if (selectObjects.length > 0) {
                        chkAll.prop('checked', true);
                    }
                    dataContentDOM.append(htmlContent);
                    });
                }
                else {
                    if (searchBox == "") {
                        htmlContent += `<tr scope="no-data-row"><td colspan="9">Chưa có dữ liệu</td></tr>`;
                    } else {
                        htmlContent += `<tr scope="no-data-row"><td colspan="9">Không tìm thấy dữ liệu phù hợp</td></tr>`;
                    }
                    dataContentDOM.html(htmlContent);
                }
            } else {
                errorAlert(data.message);
            }
        },
        error: function(response) {
            console.log(response.responseJSON);
        }
    });
}
// action delete
function DeleteSelection() {
    // var actionDelete = $(event.target);
    var data = { brands : selectObjects };
    if (selectObjects.length === 0) {
        normalAlert("Hãy chọn ít nhất 1 bản ghi để thực hiện chức năng này!");
    } else {
        Swal.fire({
            title: 'Nhắc nhẹ?',
            text: "Bạn có muốn xoá các nhãn hiệu này không?!",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#28a745',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ừm!',
            cancelButtonText: 'Không',
            showClass: {
                popup: 'animated fadeInDown lower'
            },
            hideClass: {
                popup: 'animated fadeOutUp lower'
            }
        }).then((rs)=> {
            if (rs.value) {
                $.ajax({
                    type: "POST",
                    url: "/backend/WebService.asmx/DeleteBrands",
                    data: JSON.stringify(data),
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: onSuccess,
                    failure: function (response){
                        console.log(response);
                    }
                });
                function onSuccess(response) {
                    response = response.d;
                    if (response.statusCode == 200) {
                        smallTopConnerAlert(response.message);
                        /*
                        // Client delete row
                        $.each($('tr[scope="data-row"]'), function() {
                            var row = $(this);
                            var rowBrandId = row.find('td[scope="brand-id"]').text().trim();
                            
                            $.each(selectObjects, function (index, brand) {
                                if (brand == rowBrandId) {
                                    selectObjects = arrayRemove(selectObjects, brand);
                                    row.remove();
                                }
                            });
                            
                        });
                        */
                        selectObjects = [];
                        fetchBrands(parseInt(currentPageInput.val()),parseInt(pageSizeDOM.val()),searchBox.val());
                        reloadSelectedLabel();
                    }else {
                        errorAlert(response.message);
                    }
                }
            }
        });
    }
}

function pageChanging(){
    var currentPageDOM = $(event.target);
    var currentPage = currentPageDOM.val();
    if (isNaN(currentPage)){
        errorAlert("Hãy nhập số trang hợp lệ nào");
    } else {
        fetchBrands(currentPage, parseInt(pageSizeDOM.val()),searchBox.val());
    }

}
function pageSizeChanging() {
    var pageSizeOption = $(event.target);
    var pageSize = parseInt(pageSizeOption.find('option:selected').val());
    fetchBrands(parseInt(currentPageInput.val()),pageSize, searchBox.val());
}
//  Next 
function pageControl(type){
    const NEXT = 1;
    var currentPage = parseInt(currentPageInput.val());
    if (type === NEXT) {
        var totalPage = parseInt(totalPageInput.val());
        if (currentPage < totalPage){
            fetchBrands(currentPage + 1, parseInt(pageSizeDOM.val()),searchBox.val());
            
        }
    } else {
        if (currentPage > 1) {
            fetchBrands(currentPage - 1, parseInt(pageSizeDOM.val()),searchBox.val());
        }
    }
}