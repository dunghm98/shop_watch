<%@ Page Title="" Language="C#" MasterPageFile="~/backend/App.Master" AutoEventWireup="true" CodeBehind="ShopInfo.aspx.cs" Inherits="UI.backend.WebForm3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head_title" runat="server">
	Chỉnh sửa thông tin về shop
</asp:Content>
<asp:Content ID="script_head" ContentPlaceHolderID="head_script" runat="server">
	<script type="text/javascript">
		$(document).ready(function () {
			$('.custom-radio-container').on('click', function () {
				console.log($(this).find('input'));
				$(this).find('input').prop("checked", true);
			});
			// Click thêm chính sách
			$(document).on('click', '#insertNew', function () {
				var txtNoiDung = $('input[name="noiDung"]');
				var opt1 = $('input[id="opt1"]');
				var shopWatchID = $('input[name="shopWatchID"]').val();
				var chinhSachApDung = null;
				if (txtNoiDung.val() == "") {
					txtNoiDung.addClass('error-input');
				}
				if (opt1.is(':checked')) {
					chinhSachApDung = 1;
				}
				else {
					chinhSachApDung = 0;
				}
				if (txtNoiDung.val() != "" && shopWatchID != -1) {
					$.ajax({
						type: "post",
						url: "/backend/ShopInfo.aspx/InsertNewRecord",
						data: JSON.stringify({
                            noi_dung: txtNoiDung.val(),
                            chinh_sach: chinhSachApDung,
							shopWatchID: shopWatchID,
                        }),
						contentType: "application/json; charset=utf-8",
                        dataType: "json",
						success: function (data) {
							console.log(data);
							if (data.d.status_code == 1) {
								Swal.fire({
									title: 'Nhắc nhẹ!',
									text: data.d.message,
									type: 'info',
									showCancelButton: false,
									confirmButtonColor: '#3085d6',
									cancelButtonColor: '#d33',
									confirmButtonText: 'OK!',
								}).then((result) => {
									if (result.value) {
										setTimeout(function () {
											window.location.href = '<%:Page.GetRouteUrl("ShopInfo", null) %>';
										}, 300);
									}
								});
								
							} else {
								this.alert(data.d.message);
							}
						},
						error: function (data) {
                            alert(data.responseJSON.Message);
                        }
					});
				}
				else {
					Swal.fire("Chưa có dữ liệu về chính sách của shop nên không thể thêm chinh sách bảo hành!");
					txtNoiDung.val('');
				}
			});
			// check input nội dung khi thay đổi giá trị
			$(document).on('change', 'input[name="noiDung"]', function () {
				if ($(this).val() == "") {
					$(this).addClass('error-input');
				} else {
					$(this).removeClass('error-input');
				}
			});
		});
		function wf_baoHanh_ClientValidation(sender, args) {
			// $("[id$=wf_baoHanh]").attr("id") lấy id của webControl
			if ($('#' + $("[id$=wf_baoHanh]").attr("id")).val() == "") {
				$('#wf_baoHanh_errorLbl').slideDown(200);
				$('#wf_baoHanh_errorLbl').text('Vui lòng không để trống!');

				return args.IsValid = false;
			}
			if (isNaN(args.Value)) {
				$('#wf_baoHanh_errorLbl').slideDown(200);
				$('#wf_baoHanh_errorLbl').text('Vui lòng nhập số');
				return args.IsValid = false;
			}
			else {
				$('#wf_baoHanh_errorLbl').slideUp(200);
				return args.IsValid = true;
			}
		}
		// validate email
		function wf_email_ClientValidation(sender, args) {
			var regex = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
			/**
			if ($('#' + $("[id$=wf_email]").attr("id")).val() == "") {
			$('#wf_email_errorLbl').slideDown(200);
			$('#wf_email_errorLbl').text('Vui lòng không để trống!');
			return args.IsValid = false;
			}*/
			if (!regex.test(args.Value)) {
				$('#wf_email_errorLbl').slideDown(200);
				$('#wf_email_errorLbl').text('Không đúng dạng Email');
				return args.IsValid = false;
			} else {
				$('#wf_email_errorLbl').slideUp(200);
				return args.IsValid = true;
			}
		}
		// Validate phone
		function wf_hotLine_validate(sender, args) {
			var regex = /^(\+84|0)\d{9}$/;
			if (!regex.test(args.Value)) {
				$('#wf_hotLine_errorMsg').slideDown(200);
				$('#wf_hotLine_errorMsg').text('Vui lòng nhập đúng định dạng số điện thoại (bắt đầu bằng +84 hoặc 0 và có 10 số)');
				return args.IsValid = false;
			}
			else {
				$('#wf_hotLine_errorMsg').slideUp(200);
				return args.IsValid = true;
			}
		}

	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main_content" runat="server">
	<!-- Content Header (Page header) -->
	<section class="content-header">
		<div class="container-fluid">
			<div class="row mb-2">
				<div class="col-sm-6">
					<h1>Thông tin shop</h1>
				</div>
				<div class="col-sm-6">
					<ol class="breadcrumb float-sm-right">
						<li class="breadcrumb-item"><a href="<%:Page.GetRouteUrl("Dashboard", null) %>">Home</a></li>
						<li class="breadcrumb-item active">Thông tin shop</li>
					</ol>
				</div>
			</div>
		</div>
		<!-- /.container-fluid -->
	</section>
	<section class="content">
		<div class="container-fluid">
			<div class="row">
				<div class="col-md-5">
					<div class="card card-primary">
						<div class="card-header">
							<h3 class="card-title">
								Thông tin</h3>
							<div class="card-tools">
								<button type="button" class="btn btn-primary btn-sm" data-card-widget="collapse" data-toggle="tooltip" title="Collapse">
									<i class="fas fa-minus"></i>
								</button>
							</div>
						</div>
						<asp:FormView ID="FV_shopInfo" runat="server" DataKeyNames="id" RenderOuterTable="false" OnModeChanging="FV_shopInfo_ModeChanging" OnItemUpdating="FV_shopInfo_ItemUpdating" oniteminserting="FV_shopInfo_ItemInserting">
							<ItemTemplate>
								<!-- /.card-header -->
								<div class="card-body">
									<strong><i class="fas fa-map-marker-alt mr-1"></i>Vị trí</strong>
									<p class="text-muted">
										<%# Eval("locate") %>
									</p>
									<hr />
									<strong><i class="fas fa-phone-square-alt mr-1"></i>Hotline</strong>
									<p class="text-muted">
										<%# Eval("hotline") %></p>
									<hr />
									<strong><i class="fas fa-map-marker-alt mr-1"></i>Website</strong>
									<p class="text-muted">
										<%# Eval("website") %></p>
									<hr />
									<strong><i class="fas fa-envelope mr-1"></i>Email</strong>
									<p class="text-muted">
										<%# Eval("email") %></p>
									<hr />
									<strong><i class="far fa-clock mr-1"></i>Thời gian phục vụ trong ngày</strong>
									<p class="text-muted">
										<%# Eval("opentime") %></p>
									<hr />
									<strong><i class="far fa-calendar-minus mr-1"></i>Các ngày phục vụ trong tuần</strong>
									<p class="text-muted">
										<%# Eval("opendates") %></p>
									<hr />
									<strong><i class="fab fa-facebook-f mr-1" style="color: Blue;"></i>Facebook</strong>
									<p class="text-muted">
										<a href='<%# Eval("fb") %>' title='<%# Eval("fb") %>'>
											<%# Eval("fb") %></a>
									</p>
									<hr />
									<strong><i class="fab fa-youtube mr-1" style="color: Red;"></i>Youtube</strong>
									<p class="text-muted">
										<a href='<%# Eval("ytb") %>' title='<%# Eval("ytb") %>'>
											<%# Eval("ytb") %></a>
									</p>
									<hr />
									<strong><i class="fas fa-map-marked-alt mr-1"></i>Bản đồ</strong>
									<p class="text-muted dau-ba-cham">
										<a href='<%# Eval("maps") %>' title='<%# Eval("maps") %>'>
											<%# Eval("maps") %></a>
									</p>
									<hr />
									<strong><i class="fas fa-images mr-1"></i>Hình ảnh</strong>
									<p class="text-muted">
										<%# (String)Eval("reviewimg") == "" ? "Chưa cập nhật ảnh rì viu" : "<img src='" +  Eval("reviewimg") + "'alt=\"Shop Watch\" style=\"max-width: 100%;\" />"%>
									</p>
								</div>
								<!-- /.card-body -->
								<div class="card-footer">
									<asp:LinkButton ID="EditButton" runat="server" CausesValidation="True" CommandName="Edit" Text="Chỉnh Sửa" CssClass="btn btn-block btn-primary" />
								</div>
							</ItemTemplate>
							<EditItemTemplate>
								<!-- /.card-header -->
								<div class="card-body">
									<strong>Vị trí</strong>
									<div class="input-group mt-2">
										<div class="input-group-prepend">
											<span class="input-group-text"><i class="fas fa-map-marker-alt"></i></span>
										</div>
										<asp:TextBox ID="wf_locate" Text='<%# Bind("locate") %>' runat="server" CssClass="form-control" placeholder="Vị trí" />
									</div>
									<hr />
									<strong>Hotline</strong>
									<div class="input-group mt-2">
										<div class="input-group-prepend">
											<span class="input-group-text"><i class="fas fa-phone-square-alt"></i></span>
										</div>
										<asp:TextBox ID="wf_hotLine" Text='<%# Bind("hotline") %>' runat="server" CssClass="form-control" placeholder="Hotline" />
										<asp:CustomValidator ID="wf_hotLine_customValidator" runat="server" ControlToValidate="wf_hotLine" SetFocusOnError="true" ClientValidationFunction="wf_hotLine_validate" Display="None"></asp:CustomValidator>
										<label id="wf_hotLine_errorMsg" class="text-danger mt-2 ml-2 small" style="display: none; width: 100%;">
										</label>
									</div>
									<hr />
									<strong>Website</strong>
									<div class="input-group mt-2">
										<div class="input-group-prepend">
											<span class="input-group-text"><i class="fas fa-map-marker-alt"></i></span>
										</div>
										<asp:TextBox ID="wf_website" Text='<%# Bind("website") %>' runat="server" CssClass="form-control" placeholder="Website" />
									</div>
									<hr />
									<strong>Email</strong>
									<div class="input-group mt-2">
										<div class="input-group-prepend">
											<span class="input-group-text"><i class="fas fa-envelope"></i></span>
										</div>
										<asp:TextBox ID="wf_email" Text='<%# Bind("email") %>' runat="server" TextMode="Email" CssClass="form-control" placeholder="Email" />
										<asp:CustomValidator ID="wf_email_CustomValidator" runat="server" ControlToValidate="wf_email" SetFocusOnError="true" ClientValidationFunction="wf_email_ClientValidation" Display="None"></asp:CustomValidator>
										<label id="wf_email_errorLbl" class="text-danger mt-2 ml-2 small" style="display: none; width: 100%;">
										</label>
									</div>
									<hr />
									<strong>Thời gian phục vụ trong ngày</strong>
									<div class="input-group mt-2">
										<div class="input-group-prepend">
											<span class="input-group-text"><i class="far fa-clock"></i></span>
										</div>
										<asp:TextBox ID="wf_openTime" Text='<%# Bind("opentime") %>' runat="server" CssClass="form-control" placeholder="Thời gian phục vụ trong ngày" />
									</div>
									<hr />
									<strong>Các ngày phục vụ trong tuần</strong>
									<div class="input-group mt-2">
										<div class="input-group-prepend">
											<span class="input-group-text"><i class="far fa-calendar-minus"></i></span>
										</div>
										<asp:TextBox ID="wf_openDates" Text='<%# Bind("opendates") %>' runat="server" CssClass="form-control" placeholder="Các ngày phục vụ trong tuần" />
									</div>
									<hr />
									<strong>Facebook</strong>
									<div class="input-group mt-2">
										<div class="input-group-prepend">
											<span class="input-group-text"><i class="fab fa-facebook-f" style="color: Blue;"></i></span>
										</div>
										<asp:TextBox ID="wf_fb" Text='<%# Bind("fb") %>' runat="server" CssClass="form-control" placeholder="Facebook" />
									</div>
									<hr />
									<strong>Youtube</strong>
									<div class="input-group mt-2">
										<div class="input-group-prepend">
											<span class="input-group-text"><i class="fab fa-youtube" style="color: Red;"></i></span>
										</div>
										<asp:TextBox ID="wf_ytb" Text='<%# Bind("ytb") %>' runat="server" CssClass="form-control" placeholder="Youtube" />
									</div>
									<hr />
									<strong>Bản đồ</strong>
									<div class="input-group mt-2">
										<div class="input-group-prepend">
											<span class="input-group-text"><i class="fas fa-map-marked-alt"></i></span>
										</div>
										<asp:TextBox ID="wf_maps" Text='<%# Bind("maps") %>' runat="server" CssClass="form-control" placeholder="Maps" />
									</div>
									<hr />
									<strong><i class="fas fa-images mr-1"></i>Hình ảnh</strong>
									<br />
									<div class="input-group">
										<div class="custom-file">
											<asp:FileUpload ID="wf_images" runat="server" CssClass="custom-file-input" Style="cursor: pointer;"></asp:FileUpload>
											<asp:Label ID="lb_images" CssClass="custom-file-label" runat="server" AssociatedControlID="wf_images">Chọn ảnh</asp:Label>
										</div>
									</div>
								</div>
								<!-- /.card-body -->
								<div class="card-footer">
									<asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Cập nhật" CssClass="btn btn-block btn-primary" />
									<asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Huỷ" CssClass="btn btn-block btn-secondary" />
								</div>
							</EditItemTemplate>
							<EmptyDataTemplate>
								<div class="card-body">
									<div class="empty-data-title">
										<h3>
											<i class="fas fa-info-circle"></i>
											<br />
											Chưa có dữ liệu về thông tin của shop
										</h3>
									</div>
								</div>
								<div class="card-footer">
									<asp:LinkButton ID="InsertButton" runat="server" CausesValidation="true" CommandName="New" Text="Thêm mới" CssClass="btn btn-block  btn-success"></asp:LinkButton>
								</div>
							</EmptyDataTemplate>
							<InsertItemTemplate>
								<div class="card-body">
									<strong>Vị trí</strong>
									<div class="input-group mt-2">
										<div class="input-group-prepend">
											<span class="input-group-text"><i class="fas fa-map-marker-alt"></i></span>
										</div>
										<asp:TextBox ID="wf_locate" Text='<%# Bind("locate") %>' runat="server" CssClass="form-control" placeholder="Vị trí" />
									</div>
									<hr />
									<strong>Hotline</strong>
									<div class="input-group mt-2">
										<div class="input-group-prepend">
											<span class="input-group-text"><i class="fas fa-phone-square-alt"></i></span>
										</div>
										<asp:TextBox ID="wf_hotLine" Text='<%# Bind("hotline") %>' runat="server" CssClass="form-control" placeholder="Hotline" />
										<asp:CustomValidator ID="wf_hotLine_customValidator" runat="server" ControlToValidate="wf_hotLine" SetFocusOnError="true" ClientValidationFunction="wf_hotLine_validate" Display="None"></asp:CustomValidator>
										<label id="wf_hotLine_errorMsg" class="text-danger mt-2 ml-2 small" style="display: none; width: 100%;">
										</label>
									</div>
									<hr />
									<strong>Website</strong>
									<div class="input-group mt-2">
										<div class="input-group-prepend">
											<span class="input-group-text"><i class="fas fa-map-marker-alt"></i></span>
										</div>
										<asp:TextBox ID="wf_website" Text='<%# Bind("website") %>' runat="server" CssClass="form-control" placeholder="Website" />
									</div>
									<hr />
									<strong>Email</strong>
									<div class="input-group mt-2">
										<div class="input-group-prepend">
											<span class="input-group-text"><i class="fas fa-envelope"></i></span>
										</div>
										<asp:TextBox ID="wf_email" Text='<%# Bind("email") %>' runat="server" TextMode="Email" CssClass="form-control" placeholder="Email" />
										<asp:CustomValidator ID="wf_email_CustomValidator" runat="server" ControlToValidate="wf_email" SetFocusOnError="true" ClientValidationFunction="wf_email_ClientValidation" Display="None"></asp:CustomValidator>
										<label id="wf_email_errorLbl" class="text-danger mt-2 ml-2 small" style="display: none; width: 100%;">
										</label>
									</div>
									<hr />
									<strong>Thời gian phục vụ trong ngày</strong>
									<div class="input-group mt-2">
										<div class="input-group-prepend">
											<span class="input-group-text"><i class="far fa-clock"></i></span>
										</div>
										<asp:TextBox ID="wf_openTime" Text='<%# Bind("opentime") %>' runat="server" CssClass="form-control" placeholder="Thời gian phục vụ trong ngày" />
									</div>
									<hr />
									<strong>Các ngày phục vụ trong tuần</strong>
									<div class="input-group mt-2">
										<div class="input-group-prepend">
											<span class="input-group-text"><i class="far fa-calendar-minus"></i></span>
										</div>
										<asp:TextBox ID="wf_openDates" Text='<%# Bind("opendates") %>' runat="server" CssClass="form-control" placeholder="Các ngày phục vụ trong tuần" />
									</div>
									<hr />
									<strong>Facebook</strong>
									<div class="input-group mt-2">
										<div class="input-group-prepend">
											<span class="input-group-text"><i class="fab fa-facebook-f" style="color: Blue;"></i></span>
										</div>
										<asp:TextBox ID="wf_fb" Text='<%# Bind("fb") %>' runat="server" CssClass="form-control" placeholder="Facebook" />
									</div>
									<hr />
									<strong>Youtube</strong>
									<div class="input-group mt-2">
										<div class="input-group-prepend">
											<span class="input-group-text"><i class="fab fa-youtube" style="color: Red;"></i></span>
										</div>
										<asp:TextBox ID="wf_ytb" Text='<%# Bind("ytb") %>' runat="server" CssClass="form-control" placeholder="Youtube" />
									</div>
									<hr />
									<strong>Bản đồ</strong>
									<div class="input-group mt-2">
										<div class="input-group-prepend">
											<span class="input-group-text"><i class="fas fa-map-marked-alt"></i></span>
										</div>
										<asp:TextBox ID="wf_maps" Text='<%# Bind("maps") %>' runat="server" CssClass="form-control" placeholder="Maps" />
									</div>
									<hr />
									<strong><i class="fas fa-images mr-1"></i>Hình ảnh</strong>
									<br />
									<div class="input-group">
										<div class="custom-file">
											<asp:FileUpload ID="wf_images" runat="server" CssClass="custom-file-input" Style="cursor: pointer;"></asp:FileUpload>
											<asp:Label ID="lb_images" CssClass="custom-file-label" runat="server" AssociatedControlID="wf_images">Chọn ảnh</asp:Label>
										</div>
									</div>
								</div>
								<div class="card-footer">
									<asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Lưu" CssClass="btn btn-block btn-success" />
									<asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Huỷ" CssClass="btn btn-block btn-secondary" />
								</div>
							</InsertItemTemplate>
						</asp:FormView>
					</div>
				</div>
				<div class="col-md-7">
					<asp:FormView ID="FV_ShopWatch" runat="server" RenderOuterTable="false" DataKeyNames="id" OnItemUpdating="FV_ShopWatch_ItemUpdating" OnModeChanging="FV_ShopWatch_ModeChanging" EmptyDataText="No rows" OnItemInserting="FV_ShopWatch_ItemInserting">
						<ItemTemplate>
							<div class="card card-primary">
								<div class="card-header">
									<h3 class="card-title">
										Shop Watch</h3>
									<div class="card-tools">
										<button type="button" class="btn btn-primary btn-sm" data-card-widget="collapse" data-toggle="tooltip" title="Thu gọn">
											<i class="fas fa-minus"></i>
										</button>
									</div>
								</div>
								<!-- /.card-header -->
								<div class="card-body">
									<strong><i class="fas fa-book mr-1"></i>Bảo hiểm SHOP WATCH</strong>
									<p class="text-muted">
										<%# Eval("BaoHiem") %>
									</p>
									<hr>
									<strong><i class="fas fa-map-marker-alt mr-1"></i>Bảo hành SHOP WATCH</strong>
									<p class="text-muted">
										<%# Eval("BaoHanh") %>
										Năm
									</p>
									<hr>
									<strong><i class="fas fa-pencil-alt mr-1"></i>Thẩm định</strong>
									<p class="text-muted">
										<%#Eval("thamdinh") %>
									</p>
									<hr>
									<strong><i class="far fa-file-alt mr-1"></i>Giao hàng</strong>
									<p class="text-muted">
										<%# Eval("giaohang") %>
									</p>
									<strong><i class="far fa-file-alt mr-1"></i>Thời gian bảo hành</strong>
									<p class="text-muted">
										<%#Eval("thoigianbaohanh") %>
									</p>
								</div>
								<div class="card-footer">
									<asp:LinkButton ID="EditButton" runat="server" CausesValidation="True" CommandName="Edit" Text="Chỉnh Sửa" CssClass="btn btn-block btn-primary" />
								</div>
								<!-- /.card-body -->
							</div>
						</ItemTemplate>
						<EditItemTemplate>
							<div class="card card-primary">
								<div class="card-header">
									<h3 class="card-title">
										Chính sách Shop Watch</h3>
									<div class="card-tools">
										<button type="button" class="btn btn-primary btn-sm" data-card-widget="collapse" data-toggle="tooltip" title="Collapse">
											<i class="fas fa-minus"></i>
										</button>
									</div>
								</div>
								<!-- /.card-header -->
								<div class="card-body">
									<strong>Bảo hiểm SHOP WATCH</strong>
									<div class="input-group mt-2">
										<div class="input-group-prepend">
											<span class="input-group-text"><i class="fas fa-book"></i></span>
										</div>
										<asp:TextBox ID="wf_baoHiem" Text='<%# Bind("baohiem") %>' runat="server" CssClass="form-control" placeholder="Bảo hiểm" />
									</div>
									<hr>
									<strong>Bảo hành SHOP WATCH</strong>
									<div class="input-group mt-2">
										<div class="input-group-prepend">
											<span class="input-group-text"><i class="fas fa-book"></i></span>
										</div>
										<asp:TextBox ID="wf_baoHanh" Text='<%# Bind("BaoHanh") %>' runat="server" CssClass="form-control" placeholder="Bảo hành" />
										<!-- for client validator -->
										<!-- /. -->
										<asp:CustomValidator ID="wf_baoHanh_CustomValidator" runat="server" ControlToValidate="wf_baoHanh" SetFocusOnError="true" OnServerValidate="wf_baoHanh_validation" ClientValidationFunction="wf_baoHanh_ClientValidation" Display="None" ValidateEmptyText="true"></asp:CustomValidator>
										<label id="wf_baoHanh_errorLbl" class="text-danger mt-2 ml-2 small" style="display: none; width: 100%;">
										</label>
									</div>
									<hr>
									<strong>Thẩm định</strong>
									<div class="input-group mt-2">
										<div class="input-group-prepend">
											<span class="input-group-text"><i class="fas fa-book"></i></span>
										</div>
										<asp:TextBox ID="wf_thamDinh" Text='<%# Bind("thamdinh") %>' runat="server" CssClass="form-control" placeholder="Thẩm định" />
									</div>
									<hr>
									<strong>Giao hàng</strong>
									<div class="input-group mt-2">
										<div class="input-group-prepend">
											<span class="input-group-text"><i class="fas fa-book"></i></span>
										</div>
										<asp:TextBox ID="wf_giaoHang" Text='<%# Bind("giaohang") %>' runat="server" CssClass="form-control" placeholder="Giao hàng" />
									</div>
									<strong>Thời gian bảo hành</strong>
									<div class="input-group mt-2">
										<div class="input-group-prepend">
											<span class="input-group-text"><i class="fas fa-book"></i></span>
										</div>
										<asp:TextBox ID="wf_thoiGianBaoHanh" Text='<%# Bind("thoigianbaohanh") %>' runat="server" CssClass="form-control" placeholder="Thời gian bảo hành" />
									</div>
								</div>
								<div class="card-footer">
									<asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Cập nhật" CssClass="btn btn-block btn-primary" />
									<asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Huỷ" CssClass="btn btn-block btn-secondary" />
								</div>
								<!-- /.card-body -->
							</div>
						</EditItemTemplate>
						<EmptyDataTemplate>
							<div class="card card-primary">
								<div class="card-header">
									<h3 class="card-title">
										Chính sách Shop Watch
									</h3>
									<div class="card-tools">
										<button type="button" class="btn btn-primary btn-sm" data-card-widget="collapse" data-toggle="tooltip" title="Collapse">
											<i class="fas fa-minus"></i>
										</button>
									</div>
								</div>
								<div class="card-body">
									<div class="empty-data-title">
										<h3>
											<i class="fas fa-info-circle"></i>
											<br />
											Chưa có dữ liệu về chính sách của shop
										</h3>
									</div>
								</div>
								<div class="card-footer">
									<asp:LinkButton ID="InsertButton" runat="server" CausesValidation="true" CommandName="New" Text="Thêm mới" CssClass="btn btn-block  btn-success"></asp:LinkButton>
								</div>
							</div>
						</EmptyDataTemplate>
						<InsertItemTemplate>
							<div class="card card-primary">
								<div class="card-header">
									<h3 class="card-title">
										Chính sách Shop Watch</h3>
									<div class="card-tools">
										<button type="button" class="btn btn-primary btn-sm" data-card-widget="collapse" data-toggle="tooltip" title="Collapse">
											<i class="fas fa-minus"></i>
										</button>
									</div>
								</div>
								<div class="card-body">
									<strong>Bảo hiểm SHOP WATCH</strong>
									<div class="input-group mt-2">
										<div class="input-group-prepend">
											<span class="input-group-text"><i class="fas fa-book"></i></span>
										</div>
										<asp:TextBox ID="wf_baoHiem" runat="server" CssClass="form-control" placeholder="Bảo hiểm" />
									</div>
									<hr>
									<strong>Bảo hành SHOP WATCH</strong>
									<div class="input-group mt-2">
										<div class="input-group-prepend">
											<span class="input-group-text"><i class="fas fa-book"></i></span>
										</div>
										<asp:TextBox ID="wf_baoHanh" runat="server" CssClass="form-control" placeholder="Bảo hành" />
										<!-- for client validator -->
										<!-- /. -->
										<asp:CustomValidator ID="wf_baoHanh_CustomValidator" runat="server" ControlToValidate="wf_baoHanh" SetFocusOnError="true" OnServerValidate="wf_baoHanh_validation" ClientValidationFunction="wf_baoHanh_ClientValidation" Display="None" ValidateEmptyText="true"></asp:CustomValidator>
										<label id="wf_baoHanh_errorLbl" class="text-danger mt-2 ml-2 small" style="display: none; width: 100%;">
										</label>
									</div>
									<hr>
									<strong>Thẩm định</strong>
									<div class="input-group mt-2">
										<div class="input-group-prepend">
											<span class="input-group-text"><i class="fas fa-book"></i></span>
										</div>
										<asp:TextBox ID="wf_thamDinh" runat="server" CssClass="form-control" placeholder="Thẩm định" />
									</div>
									<hr>
									<strong>Giao hàng</strong>
									<div class="input-group mt-2">
										<div class="input-group-prepend">
											<span class="input-group-text"><i class="fas fa-book"></i></span>
										</div>
										<asp:TextBox ID="wf_giaoHang" runat="server" CssClass="form-control" placeholder="Giao hàng" />
									</div>
									<strong>Thời gian bảo hành</strong>
									<div class="input-group mt-2">
										<div class="input-group-prepend">
											<span class="input-group-text"><i class="fas fa-book"></i></span>
										</div>
										<asp:TextBox ID="wf_thoiGianBaoHanh" runat="server" CssClass="form-control" placeholder="Thời gian bảo hành" />
									</div>
								</div>
								<div class="card-footer">
									<asp:LinkButton ID="InsertButton" runat="server" CausesValidation="true" CommandName="Insert" Text="Lưu" CssClass="btn btn-block btn-success" />
									<asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Huỷ" CssClass="btn btn-block btn-secondary" />
								</div>
							</div>
						</InsertItemTemplate>
					</asp:FormView>
					<div class="card card-primary">
						<div class="card-header">
							<h3 class="card-title">
								Chính sách</h3>
							<div class="card-tools">
								<div class="input-group input-group-sm" style="width: 150px; float: left; margin-right: 10px;">
									<asp:TextBox ID="tb_timKiemChinhSach" runat="server" CssClass="form-control float-right" placeholder="Tìm kiếm" name="table_search" ontextchanged="tb_timKiemChinhSach_TextChanged" AutoPostBack="true"></asp:TextBox>
									
									<div class="input-group-append">
										<button type="submit" class="btn btn-default">
											<i class="fas fa-search"></i>
										</button>
									</div>
								</div>
								<button type="button" class="btn btn-primary btn-sm" data-card-widget="collapse" data-toggle="tooltip" title="Thu gọn">
									<i class="fas fa-minus"></i>
								</button>
							</div>
						</div>
						<div class="card-body table-responsive p-0" style="height: 500px;">
							<asp:GridView ID="gv_ChinhSach" runat="server" DataKeyNames="id" AutoGenerateColumns="false" CssClass="table table-head-fixed border-0" ShowFooter="true" ShowHeaderWhenEmpty="true" OnRowCancelingEdit="gv_ChinhSach_RowCancelingEdit" OnRowEditing="gv_ChinhSach_RowEditing" OnRowUpdating="gv_ChinhSach_RowUpdating" OnRowDeleting="gv_ChinhSach_RowDeleting">
								<HeaderStyle CssClass="data-table" />
								<RowStyle CssClass="data-table-row" />
								<FooterStyle CssClass="data-table-footer" />
								<EmptyDataRowStyle CssClass="data-table-row-empty" />
								<Columns>
									<asp:TemplateField HeaderText="Nội dung chính sách">
										<ItemTemplate>
											<%#Eval("noidung") %>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox ID="edit_tb_noiDung" runat="server" CssClass="form-control form-control-sm" Text='<%# Bind("noidung") %>' />
										</EditItemTemplate>
										<FooterTemplate>
											<input type="text" name="noiDung" value="" placeholder="Nhập nội dung bảo hành..." class="form-control form-control-sm" />
											<input type="hidden" name="shopWatchID" value="<%:FV_ShopWatch.DataKey.Value != null ? int.Parse(FV_ShopWatch.DataKey.Value.ToString()) : -1 %>" />
										</FooterTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Miễn phí trọn đời">
										<ItemTemplate>
											<%#(Int32)Eval("chinhsachapdung") == 1 ? "<i class=\"fas fa-check\"></i>" : "" %>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:RadioButton runat="server" type="radio" ID="edit_opt1" GroupName="edit_policyGr" Checked='<%#(Int32)Eval("chinhsachapdung") == 1 %>' />
										</EditItemTemplate>
										<FooterTemplate>
											<div class="custom-radio-container">
												<input type="radio" class="custom-radio-content" name="policyGr" id="opt1" value="1" />
												<label for="main_content_gv_ChinhSach_opt1" id="opt1_lbl" class="custom-radio-label">
												</label>
											</div>
										</FooterTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Hỗ trợ 50% chi phí">
										<ItemTemplate>
											<%#(Int32)Eval("chinhsachapdung") == 0 ? "<i class=\"fas fa-check\"></i>" : "" %>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:RadioButton runat="server" type="radio" ID="edit_opt0" GroupName="edit_policyGr" Checked='<%#(Int32)Eval("chinhsachapdung") == 0 %>' />
										</EditItemTemplate>
										<FooterTemplate>
											<div class="custom-radio-container">
												<input type="radio" class="custom-radio-content" name="policyGr" id="opt0" value="0" checked="checked" />
												<label for="main_content_gv_ChinhSach_opt0" id="opt0_lbl" class="custom-radio-label">
												</label>
											</div>
										</FooterTemplate>
									</asp:TemplateField>
									<asp:CommandField ShowEditButton="true" CancelText='<button type="button" class="btn btn-flat btn-warning text-white"><i class="fas fa-undo">&nbsp;Huỷ</i></button>' UpdateText='<button type="button" class="btn btn-flat btn-success"><i class="far fa-save">&nbsp;Lưu</i></button>' EditText='<button type="button" class="btn btn-flat btn-primary"><i class="fas fa-edit    "></i>&nbsp; Sửa</button>' HeaderText="Sửa" />
									<asp:TemplateField HeaderText="Xoá">
										<ItemTemplate>
											<span onclick="return confirm('Bạn muốn xoá sản phầm này không?')">
												<asp:LinkButton ID="lnkDelete" runat="Server" Text='<button type="button" class="btn btn-flat btn-danger"><i class="fas fa-trash    "></i>&nbsp;&nbsp; Xoá</button>' ForeColor="Red" CommandName="Delete"></asp:LinkButton>
											</span>
										</ItemTemplate>
										<FooterTemplate>
											<a id="insertNew" href="javascript:void(0);" class="btn-new" title="Thêm mới"><i class='fas fa-plus'></i></a>
										</FooterTemplate>
									</asp:TemplateField>
								</Columns>
								<EmptyDataTemplate>
									<div class="card-body table-responsive p-0">
										<table class="table table-head-fixed empty-table">
											<tbody>
												<tr>
													<td colspan="4">
														<i class="fas fa-info-circle" style="font-size: 2rem;"></i>
														<br />
														Chưa có chính sách nào !!!
													</td>
												</tr>
												<tr>
													<td>
														<input type="text" name="noiDung" value="" placeholder="Nhập nội dung bảo hành..." class="form-control form-control-sm" />
														<input type="hidden" name="shopWatchID" value="<%:FV_ShopWatch.DataKey.Value != null ? int.Parse(FV_ShopWatch.DataKey.Value.ToString()) : -1 %>" />
													</td>
													<td>
														<div class="custom-radio-container">
															<input type="radio" class="custom-radio-content" name="policyGr" id="opt1" value="1" />
															<label for="main_content_gv_ChinhSach_opt1" id="opt1_lbl" class="custom-radio-label">
															</label>
														</div>
													</td>
													<td>
														<div class="custom-radio-container">
															<input type="radio" class="custom-radio-content" name="policyGr" id="opt0" value="0" checked="true" />
															<label for="main_content_gv_ChinhSach_opt0" id="opt0_lbl" class="custom-radio-label">
															</label>
														</div>
													</td>
													<td>
														<a id="insertNew" href="javascript:void(0);" class="btn-new" title="Thêm mới"><i class='fas fa-plus'></i></a>
													</td>
												</tr>
											</tbody>
										</table>
									</div>
								</EmptyDataTemplate>
							</asp:GridView>
						</div>
					</div>
				</div>
			</div>
		</div>
	</section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script_content" runat="server">
	<script src="<%:HttpContext.Current.Request.Url.Segments[0]+"backend/" %>assets/plugins/bs-custom-file-input/bs-custom-file-input.min.js" type="text/javascript"></script>
	<script type="text/javascript">
		bsCustomFileInput.init();
        
	</script>
</asp:Content>
