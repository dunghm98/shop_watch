<%@ Page Title="" Language="C#" MasterPageFile="~/backend/App.Master" AutoEventWireup="true" CodeBehind="Brand.aspx.cs" Inherits="UI.backend.WebForm7" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head_title" runat="server">Nhãn hiệu
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head_script" runat="server">
	<link href="<%=serverPath %>assets/css/fix-on-top.css" rel="stylesheet" type="text/css" />
	<!-- Summernote -->
	<link href="<%=serverPath %>assets/plugins/summernote/summernote-bs4.css" rel="stylesheet" type="text/css" />
	<link href="<%=serverPath %>assets/css/brand_style.css" rel="stylesheet" type="text/css" />
	<link href="<%=serverPath %>assets/css/data-tablecss.css" rel="stylesheet" type="text/css" />
	<link href="<%=serverPath %>assets/css/search-box.css" rel="stylesheet" type="text/css" />
	<link href="<%=serverPath %>assets/plugins/pretty-checkbox.min.css" rel="stylesheet" type="text/css" />
	<link href="<%=serverPath %>assets/css/brand.css" rel="stylesheet" type="text/css" />
	<link href="<%=serverPath %>assets/css/upload_img.css" rel="stylesheet" type="text/css" />
	<link href="<%=serverPath %>assets/plugins/animate.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_content" runat="server">
	<!-- Content Header (Page header) -->
	<section class="content-header">
		<div class="container-fluid">
			<div class="row mb-2">
				<div class="col-sm-6">
					<h1>Quản lý nhãn hiệu</h1>
				</div>
				<div class="col-sm-6">
					<ol class="breadcrumb float-sm-right">
						<li class="breadcrumb-item"><a href="<%:Page.GetRouteUrl("Dashboard", null) %>">Home</a></li>
						<li class="breadcrumb-item">Sản phẩm và nhãn hiệu</li>
						<li class="breadcrumb-item active">Quản lý nhãn hiệu</li>
					</ol>
				</div>
			</div>
		</div>
		<!-- /.container-fluid -->
	</section>

	<section class="content">
		<div class="container-fluid">
			<div class="row">
				<div class="col-md-12">
					<div class="pt-3 pb-3 action-nav">
						<div class="text-right">
							<button type="button" id="btnNewBrand" class="btn btn-flat btn-success mr-5 pt-2 pb-2  pl-5 pr-5" style="font-weight: 501; text-transform: uppercase; font-size: 1.5rem;" runat="server">
								Thêm nhãn hiệu mới</button>
						</div>
					</div>
				</div>
			</div>
			<div class="row mt-3 mb-3">
				<div class="col-md-6 col-sm-12">
					<div class="btn-group">
						<button type="button" class="btn btn-default btn-flat">
							Hành động</button>
						<button type="button" class="btn btn-default btn-flat dropdown-toggle dropdown-icon" data-toggle="dropdown">
							<span class="sr-only">Toggle Dropdown</span>
							<div class="dropdown-menu" role="menu">
								<span class="dropdown-item" onclick="DeleteSelection()">Xoá</span>
							</div>
						</button>
					</div>
					<label class="ml-1 font-weight-normal">
						(Đã chọn&nbsp;<label scope="row-selected">0</label>&nbsp;dòng)</label>
				</div>
				<div class="col-md-6 col-sm-12 text-right pagination">
					<div class="search-form mr-3">
						<input id="searchBox" type="text" class="form-control" placeholder="Tìm" onfocus="onFocusSearchBox();" onblur="onBlurSearchBox();">
						<div class="input-group-append">
							<span class="input-group-text" id="loaderSpinner"><i class="fab fa-searchengin"></i></span>
						</div>
					</div>
					<select class="select2-white page-size-select" onchange="pageSizeChanging();">
						<option value="5">5</option>
						<option value="10">10</option>
						<option value="20">20</option>
						<option value="50">50</option>
						<option value="100">100</option>
					</select>
					<button type="button" class="btn btn-default btn-flat page-btn" onclick="pageControl(0);">
						<i class="fas fa-chevron-left"></i>
					</button>
					<div>
						<input type="text" class="form-control page-input ml-3" id="currentPageInput" onchange="pageChanging();" />/
						<input type="text" class="form-control page-input mr-3" disabled id="totalPageInput" />
					</div>
					<button type="button" class="btn btn-default btn-flat page-btn" onclick="pageControl(1);">
						<i class="fas fa-chevron-right"></i>
					</button>
				</div>
			</div>
			<div class="row mt-3 mb-3">
				<div class="col-12">
					<table class="table data-table">
						<thead>
							<tr>
								<th>
									<div class="pretty p-svg p-plain p-bigger p-smooth mr-0">
										<input type="checkbox" id="chkAll" />
										<div class="state">
											<img class="svg" src="<%=serverPath %>assets/images/icons/check-square.png" width="18px" />
											<label style="width: 18px; height: 18px" />
										</div>
									</div>
								</th>
								<th>
									id
								</th>
								<th>
									Tên nhãn hiệu
								</th>
								<th>
									Logo
								</th>
								<th>
									KOL
								</th>
								<th>
									Tác vụ
								</th>
							</tr>
						</thead>
						<tbody scope="data-content">
						</tbody>
					</table>
				</div>
			</div>

		</div>
	</section>
	<div class="modal fade" id="modal-default">
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header">
					<h4 class="modal-title">
						Ảnh lớn</h4>
					<button type="button" class="close" data-dismiss="modal" aria-label="Close">
						<span aria-hidden="true">&times;</span>
					</button>
				</div>
				<div class="modal-body">
					<div class="uploadOuter">
						<label for="productImage" class="btn btn-primary cursor-pointer">
							Tải ảnh lên</label>
						<strong>HOẶC</strong> <span class="dragBox">Kéo 1 ảnh vào đây
							<input type="file" id="productImage" onchange="dragNdrop(event)" ondragover="drag()" ondrop="drop()" class="cursor-pointer" changed="0" accept="image/*" />
						</span>
					</div>
					<div id="preview">
					</div>
				</div>
				<div class="modal-footer justify-content-between">
					<button type="button" class="btn btn-default" data-dismiss="modal">
						Đóng</button>
					<button type="button" class="btn btn-success" id="updateImg">
						Cập nhật</button>
				</div>
			</div>
			<!-- /.modal-content -->
		</div>
		<!-- /.modal-dialog -->
	</div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="script_content" runat="server">
	<script src="<%=serverPath %>assets/js/brand.js" type="text/javascript"></script>
	<script src="<%=serverPath %>assets/js/data-table.js" type="text/javascript"></script>
	<script src="<%=serverPath %>assets/plugins/bs-custom-file-input/bs-custom-file-input.min.js" type="text/javascript"></script>
	<script src="<%=serverPath %>assets/js/insert-form.js" type="text/javascript"></script>
	<script src="<%=serverPath %>assets/plugins/summernote/summernote-bs4.min.js" type="text/javascript"></script>
	<script src="<%=serverPath %>assets/js/search-box.js" type="text/javascript"></script>
	<script src="<%=serverPath %>assets/js/upload_img.js" type="text/javascript"></script>
	<script type="text/javascript">
		bsCustomFileInput.init();
		// Summernote
		$('.textarea').summernote({
			placeholder: 'Nhập vài dòng mô tả về nhãn hiệu đi',
			tabsize: 4,
			height: 100
		});
	</script>
</asp:Content>
