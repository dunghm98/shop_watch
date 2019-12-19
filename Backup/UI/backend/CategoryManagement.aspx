<%@ Page Title="" Language="C#" MasterPageFile="~/backend/App.Master" AutoEventWireup="true" CodeBehind="CategoryManagement.aspx.cs" Inherits="UI.backend.WebForm5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head_title" runat="server">
	Quản lý danh mục
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head_script" runat="server">
	<link href="<%:HttpContext.Current.Request.Url.Segments[0]+"backend/" %>assets/plugins/pretty-checkbox.min.css" rel="stylesheet" type="text/css" />
	<!-- Bootstrap Switch -->
	<script src="<%:HttpContext.Current.Request.Url.Segments[0]+"backend/" %>assets/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
	<!-- Summernote -->
	<link href="<%:HttpContext.Current.Request.Url.Segments[0]+"backend/" %>assets/plugins/summernote/summernote-bs4.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_content" runat="server">
	<!-- Content Header (Page header) -->
	<section class="content-header">
		<div class="container-fluid">
			<div class="row mb-2">
				<div class="col-sm-6">
					<h1>Quản lý danh mục</h1>
				</div>
				<div class="col-sm-6">
					<ol class="breadcrumb float-sm-right">
						<li class="breadcrumb-item"><a href="<%:Page.GetRouteUrl("Dashboard", null) %>">Home</a></li>
						<li class="breadcrumb-item">Danh mục và tin tức</li>
						<li class="breadcrumb-item active">Quản lý danh mục</li>
					</ol>
				</div>
			</div>
		</div>
		<!-- /.container-fluid -->
	</section>
	<asp:SqlDataSource ID="CategoryDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:myConnect %>" DeleteCommand="DELETE FROM [categories] WHERE [ID] = @ID" SelectCommand="" >
		<DeleteParameters>
			<asp:Parameter Name="ID" Type="Int32" />
		</DeleteParameters>
	</asp:SqlDataSource>
	<section class="content">
		<div class="container-fluid">
			<div class="row">
				<div class="col-md-12">
					<div class="card card-outline card-info collapsed-card" id="insertForm" runat="server">
						<div class="card-header insert-form">
							<h3 class="card-title">
								Thêm danh mục mới
							</h3>
							<!-- tools box -->
							<div class="card-tools">
								<button type="button" class="btn btn-tool btn-sm" data-card-widget="collapse" data-toggle="tooltip" title="Collapse">
									<i class="fas fa-plus" id="btnCollapse" runat="server"></i>
								</button>
								<button type="button" class="btn btn-tool btn-sm" data-card-widget="remove" data-toggle="tooltip" title="Remove">
									<i class="fas fa-times"></i>
								</button>
							</div>
							<!-- /. tools -->
						</div>
						<!-- /.card-header -->
						<div class="card-body pad">
							<div class="row">
								<div class="col-md-1" style="display: flex; justify-content: center; align-items: center;">
									<div class="pretty p-switch p-slim m-0">
										<input type="checkbox" name="is_visible" id="is_visible" runat="server" checked="checked" />
										<div class="state p-success">
											<label>
												Hiển thị?</label>
										</div>
									</div>
								</div>
								<div class="col-md-5">
									<!-- <input type="checkbox" name="my-checkbox" checked data-bootstrap-switch data-off-color="danger" data-on-color="success" title="Bật tắt danh mục"> -->
									<input type="text" class="form-control" id="category_name" name="name" runat="server" placeholder="Tên danh mục" />
								</div>
								<div class="col-md-3" style="height: 1px;">
									<div class="form-group">
										<label>
											Danh mục cha</label>
										<select class="form-control ml-2 cbParent" style="width: 70%; display: inline;" runat="server" id="parent_id" name="parent_id">
											<option selected="selected">-----</option>
										</select>
									</div>
								</div>
								<div class="col-md-3">
									<input type="text" class="form-control category_order" id="category_order" name="category_order" runat="server" placeholder="Nhập thứ tự xuất hiện" />
								</div>
							</div>
							<div class="row">
								<div class="col-md-12">
									<div class="form-group mt-2">
										<label>
											Nhập vài dòng mô tả về danh mục</label>
										<textarea id="category_description" name="category_description" runat="server" class="textarea" placeholder="Nhập vài dòng mô tả về danh mục" style="width: 100%; height: 200px; font-size: 14px; line-height: 18px; border: 1px solid #dddddd; padding: 10px;"></textarea>
									</div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-12">
									<button type="button" class="btn btn-block btn-success" runat="server" id="btnNewCategory" formmethod="post">
										Thêm</button>
								</div>
							</div>
						</div>
					</div>
				</div>
				<!-- /.col-->
			</div>
			<div class="row">
				<div class="col-md-12">
					<div class="card card-outline card-info">
						<div class="card-header">
							<h3 class="card-title">
								Danh sách danh mục</h3>
							<!-- tools box -->
							<div class="card-tools">
								<button type="button" class="btn btn-tool btn-sm" data-card-widget="collapse" data-toggle="tooltip" title="Collapse">
									<i class="fas fa-minus" id="I1" runat="server"></i>
								</button>
								<button type="button" class="btn btn-tool btn-sm" data-card-widget="remove" data-toggle="tooltip" title="Remove">
									<i class="fas fa-times"></i>
								</button>
							</div>
						</div>
						<div class="card-body p-0">
							<asp:GridView ID="gv_vidList" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" CssClass="table table-striped" BorderWidth="0px" AllowPaging="True" PageSize="5" PagerStyle-CssClass="paging" onrowdatabound="gv_vidList_RowDataBound" onrowupdating="gv_vidList_RowUpdating" onpageindexchanging="gv_vidList_PageIndexChanging" onrowcancelingedit="gv_vidList_RowCancelingEdit" onrowediting="gv_vidList_RowEditing" onrowdeleting="gv_vidList_RowDeleting">
								<HeaderStyle CssClass="data-table-vid data-table-category" />
								<RowStyle CssClass="data-table-row content-row data-table-category" />
								<Columns>
									<asp:TemplateField HeaderText="#">
										<ItemTemplate>
											<%# Eval("id").ToString() %>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Bật/Tắt">
										<ItemTemplate>
											<div class="pretty p-switch p-slim m-0">
												<input type="checkbox" class="onOffChkbox" name="is_visible" id="is_visible" runat="server" checked='<%# int.Parse(Eval("isvisible").ToString()) == 1%>' data-id='<%# Eval("id") %>' />
												<div class="state p-success">
													<label>
														Hiển thị?</label>
												</div>
											</div>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Tên danh mục">
										<ItemTemplate>
											<%# Eval("name").ToString() %>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox CssClass="form-control" id="category_name" runat="server" placeholder="Tên danh mục" Text='<%# Bind("name") %>' />
										</EditItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Mô tả">
										<ItemTemplate>
											<div style="max-width: 500px; white-space: nowrap; overflow: hidden; text-overflow: ellipsis;" title='<%# Server.HtmlEncode(Eval("description").ToString()) %>'>
												<%# Server.HtmlEncode(Eval("description").ToString()) %>
											</div>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox ID="category_description" name="category_description" runat="server" CssClass="textarea" placeholder="Nhập vài dòng mô tả về danh mục" TextMode="MultiLine" Text='<%# Bind("description") %>'></asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Danh mục cha">
										<ItemTemplate>
											
											<%# Eval("parentID") %>
										</ItemTemplate>
										<EditItemTemplate>
										<asp:HiddenField ID="hf_id" runat="server" Value='<%# Bind("parentId") %>' />
											<asp:DropDownList ID="DropDownList1" runat="server" Style="width: 70%; display: inline;" CssClass="form-control ml-2 cbParent">
											</asp:DropDownList>
										</EditItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Thứ tự">
										<ItemTemplate>
											<%# Eval("order") %>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox ID="category_order" runat="server" CssClass="form-control" Text='<%# Bind("order") %>' placeholder="Thứ tự"></asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateField>
									<asp:CommandField ShowEditButton="true" CancelText='<button type="button" class="btn btn-flat btn-warning text-white"><i class="fas fa-undo"></i>&nbsp; Huỷ</button>' UpdateText='<button type="button" class="btn btn-flat btn-success"><i class="far fa-save"></i>&nbsp;Lưu</button>' EditText='<button type="button" class="btn btn-flat btn-primary"><i class="fas fa-edit    "></i>&nbsp; Sửa</button>' HeaderText="Sửa" />
									<asp:TemplateField HeaderText="Xoá">
										<ItemTemplate>
											<span onclick="return confirm('Bạn muốn xoá Video này không?')">
												<asp:LinkButton ID="lnkDelete" runat="Server" Text='<button type="button" class="btn btn-flat btn-danger"><i class="fas fa-trash    "></i>&nbsp;&nbsp; Xoá</button>' ForeColor="Red" CommandName="Delete"></asp:LinkButton>
											</span>
										</ItemTemplate>
									</asp:TemplateField>
								</Columns>
								<EmptyDataTemplate>
									<div class="text-center" style="padding: 150px 0px;">
										<i class="fas fa-exclamation-triangle" style="font-size: 3rem;"></i>
										<div>
											<h1>Không có dữ liệu</h1>
										</div>
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
<asp:Content ID="Content4" ContentPlaceHolderID="script_content" runat="server">
	<script src="<%:HttpContext.Current.Request.Url.Segments[0]+"backend/" %>assets/js/insert-form.js" type="text/javascript"></script>
	<script src="<%:HttpContext.Current.Request.Url.Segments[0]+"backend/" %>assets/plugins/summernote/summernote-bs4.min.js" type="text/javascript"></script>
	<script type="text/javascript">
		// Initial switch
		$("input[data-bootstrap-switch]").each(function () {
			$(this).bootstrapSwitch('state', $(this).prop('checked'));
		});
		// Summernote
		$('.textarea').summernote({
			placeholder: 'Nhập vài dòng mô tả về danh mục đi',
			tabsize: 4,
			height: 350
		});
		// combobox change
		$(document).on('change', '.cbParent', function () {
			var parentId = $(this).val();
				$.ajax({
					type: "post",
					url: "/backend/CategoryManagement.aspx/FindParentOrder",
					data: JSON.stringify({
								id: parentId,
							}),
					contentType: "application/json; charset=utf-8",
					dataType: "json",
					success: function (data) {
					if (data.d.status_code == 200) {
						$('.category_order').val(parseInt(data.d.order) + 1); } else {
							alert(data.d.message);
						}
					},
					error: function (data) {
						alert(data.responseJSON.Message);
					}
				});
		});
		// Baatj Tawst
		$(document).on("change", ".onOffChkbox", function () {
			var recordID = $(this).attr("data-id");
			var state = $(this).is(':checked');
			$.ajax({
					type: "post",
					url: "/backend/CategoryManagement.aspx/SetState",
					data: JSON.stringify({
								id: recordID,
								state: state,
							}),
					contentType: "application/json; charset=utf-8",
					dataType: "json",
					success: function(data) {
						if(data.d.status_code != 200) {
							errorAlert(data.d.message);
						} else {
							smallTopConnerAlert(data.d.message);
						}
					},
					error: function (data) {
						alert(data.responseJSON.Message);
					}
			});
		});
	</script>
</asp:Content>
