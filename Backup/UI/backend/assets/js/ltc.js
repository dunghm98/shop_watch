$(document).ready(function () {
    console.log('jQuery Loaded!');
    // Show loading ****
    $(document).ajaxStart(function () {
        $('.loading-layer').css({
            "display": "flex",
        });
    });
    //
    $(document).ajaxStop(function () {
        $('.loading-layer').css({
            "display": "none",
        });
    });
    // ./End
});

function normalAlert(text){
    Swal.fire({
        title: text,
        showClass: {
        popup: 'animated fadeInDown faster'
        },
        hideClass: {
        popup: 'animated fadeOutUp faster'
        }
    })
}

function errorAlert(text) {
    Swal.fire({
        icon: 'error',
        title: 'Thông báo...',
        text: text
    })
}

function successAlert(text) {
    Swal.fire({
        icon: 'success',
        title: 'Thông báo...',
        text: text
    })
}

function bigTopConnerAlert(text) {
    Swal.fire({
        position: 'top-end',
        icon: 'success',
        title: text,
        showConfirmButton: false,
        timer: 1500
    })
}

function smallTopConnerAlert(text) {
    const Toast = Swal.mixin({
      toast: true,
      position: 'top-end',
      showConfirmButton: false,
      timer: 3000,
      timerProgressBar: true,
      onOpen: (toast) => {
        toast.addEventListener('mouseenter', Swal.stopTimer)
        toast.addEventListener('mouseleave', Swal.resumeTimer)
      }
    })

    Toast.fire({
      icon: 'success',
      title: text
    })
}