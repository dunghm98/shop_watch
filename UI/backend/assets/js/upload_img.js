"use strict";
function dragNdrop(event) {
    var fileName = URL.createObjectURL(event.target.files[0]);
    var preview = document.getElementById("preview");
    var previewImg = document.createElement("img");
    previewImg.setAttribute("src", fileName);
    preview.innerHTML = "";
    preview.appendChild(previewImg);
    var fi = event.target;
    $(fi).attr('changed', 1);
}
function drag() {
    document.getElementById('productImage').parentNode.className = 'draging dragBox';
}
function drop() {
    document.getElementById('productImage').parentNode.className = 'dragBox';
}