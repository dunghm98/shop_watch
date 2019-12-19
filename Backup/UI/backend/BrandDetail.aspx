<%@ Page Title="" Language="C#" MasterPageFile="~/backend/App.Master" AutoEventWireup="true" CodeBehind="BrandDetail.aspx.cs" Inherits="UI.backend.WebForm11" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head_title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head_script" runat="server">
	<link href="<%=serverPath %>assets/css/fix-on-top.css" rel="stylesheet" type="text/css" />
	<link href="<%=serverPath %>assets/css/upload_img.css" rel="stylesheet" type="text/css" />
	<!-- Summernote -->
	<link href="<%=serverPath %>assets/plugins/summernote/summernote-bs4.css" rel="stylesheet" type="text/css" />
	<link href="<%=serverPath %>assets/plugins/animate.min.css" rel="stylesheet" type="text/css" />
	<link href="<%=serverPath %>assets/css/upload_img.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_content" runat="server">
	<section class="content-header">
		<div class="container-fluid">
			<div class="row mb-2">
				<div class="col-sm-6">
					<h1>Nhãn hiệu - <%=brand.Name %></h1>
				</div>
				<div class="col-sm-6">
					<ol class="breadcrumb float-sm-right">
						<li class="breadcrumb-item"><a href="<%:Page.GetRouteUrl("Dashboard", null) %>">Home</a></li>
						<li class="breadcrumb-item">Sản phẩm và nhãn hiệu</li>
						<li class="breadcrumb-item"><a href="<%:Page.GetRouteUrl("Brand", null) %>">Nhãn hiệu</a></li>
						<li class="breadcrumb-item active">
							<%=brand.Name %></li>
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
						<asp:Label ID="lblBrandName" class="col-md-5 col-form-label text-right" Style="cursor: pointer; user-select: none;" runat="server" Text="Tên nhãn hiệu <label class='text-red'>*</label>" AssociatedControlID="txtBrandName"></asp:Label>
						<div class="col-md-7">
							<asp:TextBox ID="txtBrandName" runat="server" CssClass="form-control" placeholder="Tên nhãn hiệu" Width="35%"></asp:TextBox>
						</div>
					</div>
					<div class="form-group row">
						<asp:Label ID="lblBrandKol" class="col-md-5 col-form-label text-right" Style="cursor: pointer; user-select: none;" runat="server" Text="Độ nổi tiếng <label class='text-red'>*</label>" AssociatedControlID="txtBrandKol"></asp:Label>
						<div class="col-md-7">
							<asp:TextBox ID="txtBrandKol" runat="server" CssClass="form-control" placeholder="Độ nổi tiếng (càng nhỏ càng lớn)" Width="25%"></asp:TextBox>
						</div>
					</div>
				</div>
			</div>
			<div class="row mt-5">
				<div class="col-md-12">
					<div class="card card-outline card-info collapsed-card">
						<div class="card-header insert-form">
							<h3 class="card-title">
								Logo</h3>
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
								<asp:Label ID="lblBrandLogo" runat="server" AssociatedControlID="brandLogo" CssClass="btn btn-primary cursor-pointer" Text="Tải ảnh lên" />
								<strong>HOẶC</strong> <span class="dragBox">Kéo 1 ảnh vào đây
									<asp:FileUpload ID="brandLogo" runat="server" onchange="dragNdrop(event)" ondragover="drag()" ondrop="drop()" class="cursor-pointer" />
								</span>
							</div>
							<div id="preview">
								<% if (brand.Logo != "")
		   { %>
								<img src="<%=brand.Logo %>" alt="<%=brand.Name %>" />
								<button data-id='<%=brand.Id %>' id="delete_image" type="button" class="btn btn-flat" title="Xoá ảnh này">
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
									Giới thiệu về nhãn hiệu</label>
								<textarea id="txtBrandDescription" runat="server" class="textarea" placeholder="Nhập vài dòng giới thiệu nhãn hiệu" style="width: 100%; height: 200px; font-size: 14px; line-height: 18px; border: 1px solid #dddddd; padding: 10px;"></textarea>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
	</section>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="script_content" runat="server">
	<script src="<%=serverPath %>assets/js/insert-form.js" type="text/javascript"></script>
	<script src="<%=serverPath %>assets/js/upload_img.js" type="text/javascript"></script>
	<script src="<%=serverPath %>assets/js/fix-on-top.js" type="text/javascript"></script>
	<script src="<%=serverPath %>assets/plugins/summernote/summernote-bs4.min.js" type="text/javascript"></script>
	<script src="<%=serverPath %>assets/js/search-box.js" type="text/javascript"></script>
	<script src="<%=serverPath %>assets/js/brand.js" type="text/javascript"></script>
	<script type="text/javascript">
		$('.textarea').summernote({
			placeholder: 'Nhập vài dòng mô tả về nhãn hiệu đi',
			tabsize: 4,
			height: 250
		});
	</script>
</asp:Content>
