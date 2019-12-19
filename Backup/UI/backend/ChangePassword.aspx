<%@ Page Title="" Language="C#" MasterPageFile="~/backend/App.Master" AutoEventWireup="true"
    CodeBehind="ChangePassword.aspx.cs" Inherits="UI.backend.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head_title" runat="server">
    Đổi mật khẩu
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main_content" runat="server">
    <!-- Content Header (Page header) -->
    <div class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1 class="m-0 text-dark">
                        Đổi mật khẩu</h1>
                </div>
                <!-- /.col -->
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="/admin/dashboard">Home</a></li>
                        <li class="breadcrumb-item active">Đổi mật khẩu</li>
                    </ol>
                </div>
                <!-- /.col -->
            </div>
            <!-- /.row -->
        </div>
        <!-- /.container-fluid -->
    </div>
    <!-- Main content -->
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="card card-primary">
                    <div class="card-header">
                        <h3 class="card-title">
                            Đổi mật khẩu</h3>
                        <div class="card-tools">
                            <button type="button" class="btn btn-tool" data-card-widget="collapse" data-toggle="tooltip"
                                title="Collapse">
                                <i class="fas fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="form-group">
                            <label for="inputName">
                                Mật khẩu hiện tại</label>
                            <input name="curPass" class="form-control" type="Password" data-error="#CurrentPassword-error" />
                            <label id="CurrentPassword-error" style="display: block; width: 100%;" class="text-danger ml-2 mt-2 small">
                            </label>
                        </div>
                        <div class="form-group">
                            <label for="inputProjectLeader">
                                Mật khẩu mới</label>
                            <input name="newPass" class="form-control" type="password" id="newPass_id" data-error="#NewPassword-error" />
                            <label id="NewPassword-error" style="display: block; width: 100%;" class="text-danger ml-2 mt-2 small">
                            </label>
                        </div>
                        <div class="form-group">
                            <label for="inputProjectLeader">
                                Xác nhận lại mật khẩu mới</label>
                            <input name="reNewPass" class="form-control" type="password" data-error="#ReTypeNewPassword-error" />
                            <label id="ReTypeNewPassword-error" style="display: block; width: 100%;" class="text-danger ml-2 mt-2 small">
                            </label>
                        </div>
                    </div>
                    <!-- /.card-body -->
                </div>
                <!-- /.card -->
            </div>
        </div>
        <div class="row">
            <div class="col-12">
                <a href="#" id="btnCancel" runat="server" class="btn btn-secondary">Huỷ bỏ</a>
                <input type="button" value="Cập nhật mật khẩu" class="btn btn-success float-right"
                    id="btnUpdatePassword">
                <!-- <asp:Button ID="btnUpdate" runat="server" Text="Cập nhật" CssClass="btn btn-success float-right"></asp:Button> -->
            </div>
        </div>
    </section>
    <!-- /.content -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script_content" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            
            var form = $("#app");


            form.validate({
                rules: {
                    curPass: {
                        required: true,
                    },
                    newPass: {
                        required: true,
                        minlength: 6,
                        maxlength: 32,
                    },
                    reNewPass: {
                        equalTo: "#newPass_id",
                    }
                },
                messages: {
                    curPass: {
                        required: "Không được để trống",
                    },
                    newPass: {
                        required: "Không được để trống",
                        minlength: "Độ dài tối thiểu 6 ký tự",
                        maxlength: "Độ dài tối đa 32 ký tự",
                    },
                    reNewPass: {
                        equalTo: "Mật khẩu nhập lại chưa khớp",
                    },
                },
                errorPlacement: function (error, element) {
                    var placement = $(element).data('error');
                    if (placement) {
                        $(placement).append(error)
                    } else {
                        error.insertAfter(element);
                    }
                }
            });

            $( "#btnUpdatePassword" ).on('click', function () {
                if (form.valid()) {
                    $.ajax({
                        type: "post",
                        url: "/backend/ChangePassword.aspx/DoChangePassword",
                        data: JSON.stringify({
                            curPass: $("input[name='curPass']").val(),
                            newPass: $("input[name='newPass']").val(),
                        }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            console.log(data);
                            if (data.d.status_code == 1) {
                                bigTopConnerAlert("Đổi mật khẩu thành công");
                                $("input[name='curPass']").val("");
                                $("input[name='newPass']").val("");
                                $("input[name='reNewPass']").val("")
                            }
                            else {
                                errorAlert("Mật khẩu cũ không đúng, vui lòng thử lại");
                            }
                        },
                        error: function (data) {
                            console.log(data);
                            alert(data.responseJSON.Message);
                        }
                    });
                } else {
                    Swal.fire({
                        
                          icon: 'error',
                          title: 'Oops...',
                          text: 'Hãy nhập vào dữ liệu hợp lệ!',
                    });
                }
            });
        });
    </script>
</asp:Content>
