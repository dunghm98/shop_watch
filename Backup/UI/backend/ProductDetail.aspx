<%@ Page Title="" Language="C#" MasterPageFile="~/backend/App.Master" AutoEventWireup="true" CodeBehind="ProductDetail.aspx.cs" Inherits="UI.backend.WebForm9" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head_title" runat="server">
	<%=product.Name %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head_script" runat="server">
	<link href="<%:HttpContext.Current.Request.Url.Segments[0]+"backend/" %>assets/plugins/pretty-checkbox.min.css" rel="stylesheet" type="text/css" />
	<link href="<%:HttpContext.Current.Request.Url.Segments[0]+"backend/" %>assets/css/sweet_switch.css" rel="stylesheet" type="text/css" />
	<link href="<%:HttpContext.Current.Request.Url.Segments[0]+"backend/" %>assets/css/upload_img.css" rel="stylesheet" type="text/css" />
	<link href="<%:HttpContext.Current.Request.Url.Segments[0]+"backend/" %>assets/css/fix-on-top.css" rel="stylesheet" type="text/css" />
	<link href="<%:HttpContext.Current.Request.Url.Segments[0]+"backend/" %>assets/css/dropdown_box.css" rel="stylesheet" type="text/css" />
	<!-- Summernote -->
	<link href="<%:HttpContext.Current.Request.Url.Segments[0]+"backend/" %>assets/plugins/summernote/summernote-bs4.css" rel="stylesheet" type="text/css" />
	<!-- Select2 -->
	<link href="<%:HttpContext.Current.Request.Url.Segments[0]+"backend/" %>assets/plugins/select2/css/select2.css" rel="stylesheet" type="text/css" />
	<link href="<%:HttpContext.Current.Request.Url.Segments[0]+"backend/" %>assets/plugins/animate.min.css" rel="stylesheet" type="text/css" />
	<link href="<%:HttpContext.Current.Request.Url.Segments[0]+"backend/" %>assets/plugins/select2-bootstrap4-theme/select2-bootstrap4.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_content" runat="server">
	<!-- Content Header (Page header) -->
	<section class="content-header">
		<div class="container-fluid">
			<div class="row mb-2">
				<div class="col-sm-6">
					<h1>Chi tiết sản phẩm -
						<%#product.Name %></h1>
				</div>
				<div class="col-sm-6">
					<ol class="breadcrumb float-sm-right">
						<li class="breadcrumb-item"><a href="<%:Page.GetRouteUrl("Dashboard", null) %>">Home</a></li>
						<li class="breadcrumb-item">Sản phẩm và nhãn hiệu</li>
						<li class="breadcrumb-item"><a href="<%:Page.GetRouteUrl("Product", null) %>">Sản phẩm</a></li>
						<li class="breadcrumb-item active">
							<%#product.Name %></li>
					</ol>
				</div>
			</div>
		</div>
		<!-- /.container-fluid -->
	</section>
	<section>
		<div class="row">
			<div class="col-12">
				<div class="pt-3 pb-3 ml-3 mr-3 active-nav action-nav">
					<div class="text-right pl-5 pr-5">
						<a href="#" id="btnBack" runat="server" class="btn ml-3 mr-3" style="font-size: 1.5rem; text-transform: uppercase; font-weight: 501;"><i class="fas fa-arrow-left"></i>&nbsp;&nbsp;&nbsp;Trở lại</a>
						<button id="btnSave" runat="server" type="button" class="btn btn-flat btn-success pl-5 pr-5 pt-2 pb-2 ml-3 mr-3" style="font-size: 1.5rem; text-transform: uppercase; font-weight: 501;">
							Lưu</button>
					</div>
				</div>
			</div>
		</div>
	</section>
	<section class="content">
		<div class="container-fluid">
			<div class="row mt-5">
				<div class="col-md-12">
					<div class="form-group row">
						<asp:Label ID="lbTxtProductName" class="col-md-5 col-form-label text-right" Style="cursor: pointer; user-select: none;" runat="server" Text="Bật/tắt" AssociatedControlID="txtProductName"></asp:Label>
						<div class="col-md-7">
							<!-- add class p-switch -->
							<label class="switch">
								<input type="checkbox" runat="server" id="chkboxEnable" />
								<span class="slider"></span>
							</label>
						</div>
					</div>
					<div class="form-group row">
						<asp:Label ID="Label1" class="col-md-5 col-form-label text-right" Style="cursor: pointer; user-select: none;" runat="server" Text="Tên sản phẩm <label class='text-red'>*</label>" AssociatedControlID="txtProductName"></asp:Label>
						<div class="col-md-7">
							<asp:TextBox ID="txtProductName" runat="server" CssClass="form-control" placeholder="Tên sản phẩm" Width="35%"></asp:TextBox>
						</div>
					</div>
					<div class="form-group row">
						<asp:Label ID="lblProductPrice" class="col-md-5 col-form-label text-right" Style="cursor: pointer; user-select: none;" runat="server" Text="Giá <label class='text-red'>*</label>" AssociatedControlID="txtProductPrice"></asp:Label>
						<div class="col-md-7">
							<asp:TextBox ID="txtProductPrice" runat="server" CssClass="form-control" placeholder="Giá" Width="20%" Text="0"></asp:TextBox>
						</div>
					</div>
					<div class="form-group row">
						<asp:Label ID="lblSales" class="col-md-5 col-form-label text-right" Style="cursor: pointer; user-select: none;" runat="server" Text="Sale (%)" AssociatedControlID="txtSales"></asp:Label>
						<div class="col-md-7">
							<asp:TextBox ID="txtSales" runat="server" CssClass="form-control" placeholder="Nhập mức sale (%)" Width="16%"></asp:TextBox>
						</div>
					</div>
					<div class="form-group row">
						<asp:Label ID="lblTxtForGender" class="col-md-5 col-form-label text-right" Style="cursor: pointer; user-select: none;" runat="server" Text="Kho hàng" AssociatedControlID="txtForGender"></asp:Label>
						<div class="col-md-7">
							<div class="dropdown">
								<div class="select">
									<span>
										<%=product.IsOutOfStock == 1 ? "Còn hàng" : "Hết Hàng"%></span> <i class="fa fa-chevron-left"></i>
								</div>
								<asp:TextBox type="hidden" name="gender" ID="txtStock" runat="server" />
								<ul class="dropdown-menu">
									<li id="1">Còn hàng</li>
									<li id="0">Hết hàng</li>
								</ul>
							</div>
						</div>
					</div>
					<div class="form-group row">
						<asp:Label ID="lblOrigin" class="col-md-5 col-form-label text-right" Style="cursor: pointer; user-select: none;" runat="server" Text="Xuất sứ" AssociatedControlID="txtProductOrigin"></asp:Label>
						<div class="col-md-7">
							<asp:TextBox ID="txtProductOrigin" runat="server" CssClass="form-control" placeholder="Xuất sứ" Width="30%"></asp:TextBox>
						</div>
					</div>
					<div class="form-group row">
						<asp:Label ID="lblMachineType" class="col-md-5 col-form-label text-right" Style="cursor: pointer; user-select: none;" runat="server" Text="Kiểu máy" AssociatedControlID="txtProductMachineType"></asp:Label>
						<div class="col-md-7">
							<asp:TextBox ID="txtProductMachineType" runat="server" CssClass="form-control" placeholder="Kiểu máy" Width="30%"></asp:TextBox>
						</div>
					</div>
					<div class="form-group row">
						<asp:Label ID="lblForGender" class="col-md-5 col-form-label text-right" Style="cursor: pointer; user-select: none;" runat="server" Text="Giới tính" AssociatedControlID="txtForGender"></asp:Label>
						<div class="col-md-7">
							<div class="dropdown">
								<div class="select">
									<span>
										<%#product.ForGender == -1 ? "Chọn giới tính" : (product.ForGender == 1 ? "Nam" : "Nữ")%></span> <i class="fa fa-chevron-left"></i>
								</div>
								<asp:TextBox type="hidden" name="gender" ID="txtForGender" runat="server" value="-1" />
								<ul class="dropdown-menu">
									<li id="1">Nam</li>
									<li id="0">Nữ</li>
								</ul>
							</div>
						</div>
					</div>
					<div class="form-group row">
						<asp:Label ID="lblSize" class="col-md-5 col-form-label text-right" Style="cursor: pointer; user-select: none;" runat="server" Text="Kích cỡ" AssociatedControlID="txtSize"></asp:Label>
						<div class="col-md-7">
							<asp:TextBox ID="txtSize" runat="server" CssClass="form-control" placeholder="Kích cỡ" Width="20%"></asp:TextBox>
						</div>
					</div>
					<div class="form-group row">
						<asp:Label ID="lblHeight" class="col-md-5 col-form-label text-right" Style="cursor: pointer; user-select: none;" runat="server" Text="Độ dày" AssociatedControlID="txtHeight"></asp:Label>
						<div class="col-md-7">
							<asp:TextBox ID="txtHeight" runat="server" CssClass="form-control" placeholder="Độ dày" Width="20%"></asp:TextBox>
						</div>
					</div>
					<div class="form-group row">
						<asp:Label ID="lblShellMaterial" class="col-md-5 col-form-label text-right" Style="cursor: pointer; user-select: none;" runat="server" Text="Chất liệu vỏ" AssociatedControlID="txtShellMaterial"></asp:Label>
						<div class="col-md-7">
							<asp:TextBox ID="txtShellMaterial" runat="server" CssClass="form-control" placeholder="Chất liệu vỏ" Width="25%"></asp:TextBox>
						</div>
					</div>
					<div class="form-group row">
						<asp:Label ID="lblChainMaterial" class="col-md-5 col-form-label text-right" Style="cursor: pointer; user-select: none;" runat="server" Text="Chất liệu dây" AssociatedControlID="txtChainMaterial"></asp:Label>
						<div class="col-md-7">
							<asp:TextBox ID="txtChainMaterial" runat="server" CssClass="form-control" placeholder="Chất liệu dây" Width="25%"></asp:TextBox>
						</div>
					</div>
					<div class="form-group row">
						<asp:Label ID="lblGlassesMaterial" class="col-md-5 col-form-label text-right" Style="cursor: pointer; user-select: none;" runat="server" Text="Chất liệu kính" AssociatedControlID="txtGlassesMaterial"></asp:Label>
						<div class="col-md-7">
							<asp:TextBox ID="txtGlassesMaterial" runat="server" CssClass="form-control" placeholder="Chất liệu kính" Width="25%"></asp:TextBox>
						</div>
					</div>
					<div class="form-group row">
						<asp:Label ID="lblFunctions" class="col-md-5 col-form-label text-right" Style="cursor: pointer; user-select: none;" runat="server" Text="Chức năng" AssociatedControlID="txtFunctions"></asp:Label>
						<div class="col-md-7">
							<asp:TextBox ID="txtFunctions" runat="server" CssClass="form-control" placeholder="Chức năng" Width="35%"></asp:TextBox>
						</div>
					</div>
					<div class="form-group row">
						<asp:Label ID="lblWaterResist" class="col-md-5 col-form-label text-right" Style="cursor: pointer; user-select: none;" runat="server" Text="Độ chịu nước" AssociatedControlID="txtWaterResist"></asp:Label>
						<div class="col-md-7">
							<asp:TextBox ID="txtWaterResist" runat="server" CssClass="form-control" placeholder="Độ chịu nước" Width="15%"></asp:TextBox>
						</div>
					</div>
					<div class="form-group row">
						<asp:Label ID="lblInCategories" class="col-md-5 col-form-label text-right" Style="cursor: pointer; user-select: none; font-weight: 700" runat="server" Text="Danh mục"></asp:Label>
						<div class="col-md-7">
							<select class="form-control select2bs4 select2-green" style="width: 50%;" multiple="true" runat="server" id="txtInCategories" data-placeholder="Chọn danh mục">
							</select>
						</div>
					</div>
					<div class="form-group row">
						<asp:Label ID="lblBrand" class="col-md-5 col-form-label text-right" Style="cursor: pointer; user-select: none;" runat="server" Text="Nhãn hiệu" AssociatedControlID="txtBrand"></asp:Label>
						<div class="col-md-7">
							<div class="dropdown">
								<div class="select">
									<span>
										<%=product.Brand.Id == -1 ? "Chọn nhãn hiệu" : product.Brand.Name %></span> <i class="fa fa-chevron-left"></i>
								</div>
								<asp:TextBox type="hidden" name="gender" ID="txtBrand" runat="server" />
								<ul class="dropdown-menu">
									<li id="-1">Không nhãn hiệu</li>
									<% foreach (DTOs.Brand brand in lstBrand)
			{ %>
									<li id='<%=brand.Id %>'>
										<%=brand.Name%></li>
									<%} %>
								</ul>
							</div>
						</div>
					</div>
				</div>
			</div>
			<div class="row mt-5">
				<div class="col-md-12">
					<div class="card card-outline card-info collapsed-card">
						<div class="card-header insert-form">
							<h3 class="card-title">
								Hình ảnh</h3>
							<div class="card-tools">
								<button type="button" class="btn btn-tool btn-sm" data-card-widget="collapse" data-toggle="tooltip" title="Collapse">
									<i class="fas fa-plus"></i>
								</button>
								<button type="button" class="btn btn-tool btn-sm" data-card-widget="remove" data-toggle="tooltip" title="Remove">
									<i class="fas fa-times"></i>
								</button>
							</div>
						</div>
						<div class="card-body">
							<div class="uploadOuter">
								<asp:Label ID="Label2" runat="server" AssociatedControlID="productImage" CssClass="btn btn-primary cursor-pointer" Text="Tải ảnh lên" />
								<strong>HOẶC</strong> <span class="dragBox">Kéo 1 ảnh vào đây
									<asp:FileUpload ID="productImage" runat="server" onchange="dragNdrop(event)" ondragover="drag()" ondrop="drop()" class="cursor-pointer" changed="0" />
								</span>
							</div>
							<div id="preview">
								<% if (product.Image != "")
		   { %>
								<img src="<%=product.Image %>" alt="<%=product.Name %>" />
								<button data-id='<%=product.Id %>' id="delete_image" type="button" class="btn btn-flat" title="Xoá ảnh này">
									<i class="fas fa-trash"></i>
								</button>
								<%}
		   else
		   {%>
								<img src="/backend/assets/images/no-image.jpg" alt="Chưa cập nhật hình ảnh" />
								<%} %>
							</div>
						</div>
					</div>
				</div>
			</div>
			<div class="row mt-2">
				<div class="col-md-12">
					<div class="card card-outline card-info collapsed-card">
						<div class="card-header insert-form">
							<h3 class="card-title">
								Mô tả</h3>
							<div class="card-tools">
								<button type="button" class="btn btn-tool btn-sm" data-card-widget="collapse" data-toggle="tooltip" title="Collapse">
									<i class="fas fa-plus"></i>
								</button>
								<button type="button" class="btn btn-tool btn-sm" data-card-widget="remove" data-toggle="tooltip" title="Remove">
									<i class="fas fa-times"></i>
								</button>
							</div>
						</div>
						<div class="card-body">
							<div class="form-group">
								<label>
									Mô tả ngắn</label>
								<textarea id="txtProductShortDescription" runat="server" class="textarea" placeholder="Nhập vài dòng ngắn mô tả về sản phẩm" style="width: 100%; height: 200px; font-size: 14px; line-height: 18px; border: 1px solid #dddddd; padding: 10px;"></textarea>
							</div>
							<div class="form-group">
								<label>
									Mô tả đầy đủ</label>
								<textarea id="txtProductDescription" runat="server" class="textarea" placeholder="Nhập vài dòng mô tả về sản phẩm" style="width: 100%; height: 200px; font-size: 14px; line-height: 18px; border: 1px solid #dddddd; padding: 10px;"></textarea>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
	</section>


	
	

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="script_content" runat="server">
	<script src="<%:HttpContext.Current.Request.Url.Segments[0]+"backend/" %>assets/js/insert-form.js" type="text/javascript"></script>
	<script src="<%:HttpContext.Current.Request.Url.Segments[0]+"backend/" %>assets/js/dropdown_box.js" type="text/javascript"></script>
	<script src="<%:HttpContext.Current.Request.Url.Segments[0]+"backend/" %>assets/js/upload_img.js" type="text/javascript"></script>
	<script src="<%:HttpContext.Current.Request.Url.Segments[0]+"backend/" %>assets/js/fix-on-top.js" type="text/javascript"></script>
	<script src="<%:HttpContext.Current.Request.Url.Segments[0]+"backend/" %>assets/js/product.js" type="text/javascript"></script>
	<script src="<%:HttpContext.Current.Request.Url.Segments[0]+"backend/" %>assets/plugins/summernote/summernote-bs4.min.js" type="text/javascript"></script>
	<script src="<%:HttpContext.Current.Request.Url.Segments[0]+"backend/" %>assets/plugins/select2/js/select2.full.min.js" type="text/javascript"></script>
	<script type="text/javascript">
		// Summernote
		$('.textarea').summernote({
			placeholder: 'Nhập vài dòng mô tả về danh mục đi',
			tabsize: 4,
			height: 250
		});
		//Initialize Select2 Elements
		$('.select2bs4').select2({
			theme: 'bootstrap4'
		})
	</script>
</asp:Content>
