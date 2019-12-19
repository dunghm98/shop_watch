<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UI.backend.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Đăng Nhập</title>
    <link href="assets/plugins/fontawesome-free/css/all.css" rel="stylesheet"
        type="text/css" />
    <link href="assets/plugins/ionicons/css/ionicons.min.css" rel="stylesheet"
        type="text/css" />
    <link href="assets/plugins/adminlte/adminlte.min.css" rel="stylesheet"
        type="text/css" />
    <link href="assets/plugins/ggfonts/source-sans-pro/source-sans-pro.css"
        rel="stylesheet" type="text/css" />
    <!-- SweetAlert -->
    <link href="assets/plugins/sweetalert2/dist/sweetalert2.min.css" rel="stylesheet"
        type="text/css" />
        <!-- sweet alert -->

    <script src="<%:HttpContext.Current.Request.Url.Segments[0]+"backend/" %>assets/plugins/sweetalert2/dist/sweetalert2.min.js" type="text/javascript"></script>
    
            
<script src="<%:HttpContext.Current.Request.Url.Segments[0]+"backend/" %>assets/plugins/jquery/jquery.min.js" type="text/javascript"></script>
    <script src="<%:HttpContext.Current.Request.Url.Segments[0]+"backend/" %>assets/plugins/jquery.validate.min.js" type="text/javascript"></script>
    <script src="<%:HttpContext.Current.Request.Url.Segments[0]+"backend/" %>assets/js/ltc.js" type="text/javascript"></script>
</head>
<body class="hold-transition login-page">
    <form id="login_form" runat="server">
    <div class="login-box">
  <div class="login-logo">
    <a href="/">
        <img src="<%:HttpContext.Current.Request.Url.Segments[0]+"backend/" %>assets/images/logo-shopwatch_bak.png" alt="SHOP WATCH" />
    </a>
  </div>
  <!-- /.login-logo -->
  <div class="card">
    <div class="card-body login-card-body">
      <p class="login-box-msg">Hãy đăng nhập để có thể sử dụng phun chức năng !!!</p>

      <form action="/../../index3.html" method="post">
        <div class="input-group mb-3">
          <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" placeholder="Email" data-error="#emailError"></asp:TextBox>
          <div class="input-group-append">
            <div class="input-group-text">
              <span class="fas fa-envelope"></span>
            </div>
          </div>
          <label id="emailError" style="display: block; width: 100%;" class="text-danger ml-2 mt-2 small"></label>
        </div>
        <div class="input-group mb-3">
            <asp:TextBox ID="txtPassword" runat="server" placeholder="Mật Khẩu" CssClass="form-control" TextMode="Password" data-error="#passwordError"></asp:TextBox>
          <div class="input-group-append">
            <div class="input-group-text">
              <span class="fas fa-lock"></span>
            </div>
          </div>
          <label id="passwordError" style="display: block; width: 100%;" class="text-danger ml-2 mt-2 small"></label>
        </div>
        <div class="row">
        <!--
          <div class="col-7">
            
            <div class="icheck-primary">
              <input type="checkbox" id="remember">
              <label for="remember">
                Remember Me
              </label>
              
            </div>
            
          </div>-->
          <!-- /.col -->
          <div class="col-12">
              <asp:Button ID="btnLog" runat="server" Text="Đăng Nhập" 
                  CssClass="btn btn-primary btn-block" onclick="doLogin" />
          </div>
          <!-- /.col -->
        </div>
      </form>
      <!--
      <div class="social-auth-links text-center mb-3">
        <p>- OR -</p>
        <a href="#" class="btn btn-block btn-primary">
          <i class="fab fa-facebook mr-2"></i> Sign in using Facebook
        </a>
        <a href="#" class="btn btn-block btn-danger">
          <i class="fab fa-google-plus mr-2"></i> Sign in using Google+
        </a>
      </div>
      <!-- /.social-auth-links -->
      <!--
      <p class="mb-1">
        <a href="forgot-password.html">I forgot my password</a>
      </p>
      <p class="mb-0">
        <a href="register.html" class="text-center">Register a new membership</a>
      </p>-->
    </div>
    <!-- /.login-card-body -->
  </div>
</div>
    </form>

        <script type="text/javascript">
            $(document).ready(function () {
                $('#login_form').validate({
                    rules: {
                        <%=txtEmail.UniqueID %>: {
                            required:true,
                            email: true
                        },
                        <%=txtPassword.UniqueID %>: {required: true,}
                    },
                    messages: {
                        <%=txtEmail.UniqueID %>:{  
                          required: "Không được để trống.",
                          email: "Hãy nhập đúng định dạng email.",
                        },
                        <%=txtPassword.UniqueID %>: {required: "Mật khẩu không được để trống",}
                    },
                    errorPlacement: function(error, element) {
                          console.log(error);
                          var placement = $(element).data('error');
                          if (placement) {
                            $(placement).append(error)
                          } else {
                            error.insertAfter(element);
                          }
                    },
                });
            });
        </script>
</body>
</html>
