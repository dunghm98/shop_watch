<%@ Page Title="" Language="C#" MasterPageFile="~/backend/App.Master" AutoEventWireup="true" CodeBehind="OpenBox.aspx.cs" Inherits="UI.backend.WebForm4" ValidateRequest="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head_title" runat="server">Quản lý video đập hộp
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head_script" runat="server">
	<script src="<%:HttpContext.Current.Request.Url.Segments[0]+"backend/" %>assets/js/insert-form.js" type="text/javascript"></script>
<script type="text/javascript">
	$(document).ready(function () {

		// Thêm video mới
		$(document).on('click', '.BtnNewVideo', function () {
			var txtContent = $('textarea[name="txtContent"]');
			if (txtContent.val() == '') {
				txtContent.addClass('error-input');
			} else {
				$.ajax({
						type: "post",
						url: "/backend/OpenBox.aspx/InsertNewRecord",
						data: JSON.stringify({
                            txtContent: txtContent.val(),
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
											window.location.href = '<%:Page.GetRouteUrl("OpenBoxs", null) %>';
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
		});
	});
</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_content" runat="server">
	<!-- Content Header (Page header) -->
	<section class="content-header">
		<div class="container-fluid">
			<div class="row mb-2">
				<div class="col-sm-6">
					<h1>Quản lý video đập hộp</h1>
				</div>
				<div class="col-sm-6">
					<ol class="breadcrumb float-sm-right">
						<li class="breadcrumb-item"><a href="<%:Page.GetRouteUrl("Dashboard", null) %>">Home</a></li>
						<li class="breadcrumb-item">Danh mục và tin tức</li>
						<li class="breadcrumb-item active">Quản lý video đập hộp</li>
					</ol>
				</div>
			</div>
		</div>
		<!-- /.container-fluid -->
	</section>
	<!-- DataSource -->
	<asp:SqlDataSource ID="sds_OpenBoxVideos" runat="server" ConnectionString="<%$ ConnectionStrings:myConnect %>" DeleteCommand="DELETE FROM [open_box_videos] WHERE [ID] = @ID" InsertCommand="INSERT INTO [open_box_videos] ([content], [created_at], [updated_at]) VALUES (@content, @created_at, @updated_at)" SelectCommand="SELECT * FROM [open_box_videos] ORDER BY [updated_at] DESC" UpdateCommand="UPDATE [open_box_videos] SET [content] = @content, [updated_at] = GETDATE() WHERE [ID] = @ID">
		<DeleteParameters>
			<asp:Parameter Name="ID" Type="Int32" />
		</DeleteParameters>
		<InsertParameters>
			<asp:Parameter Name="content" Type="String" />
			<asp:Parameter Name="created_at" Type="DateTime" />
			<asp:Parameter Name="updated_at" Type="DateTime" />
		</InsertParameters>
		<UpdateParameters>
			<asp:Parameter Name="content" Type="String" />
			<asp:Parameter Name="ID" Type="Int32" />
		</UpdateParameters>
	</asp:SqlDataSource>
	<!-- /.DataSource -->

	<section class="content">
		<div class="container-fluid">

		<div class="row">
			<div class="col-md-12">
				<div class="card card-primary collapsed-card">
					<div class="card-header insert-form">
						<h3 class="card-title">Thêm video mới
						</h3>
						<div class="card-tools">
							<button type="button" class="btn btn-primary btn-sm" data-card-widget="collapse" data-toggle="tooltip" title="Collapse">
								<i class="fas fa-plus"></i>
							</button>
						</div>
					</div>
					<div class="card-body">
						<div class="row">
							<div class="col-md-12">
								<div class="form-group">
									<label>
										Frame video</label>
									<textarea name="txtContent" class="form-control" rows="3" placeholder="Nhập iframe của video"></textarea>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-12">
								<p class="text-primary"><a href="https://youtu.be/OA91U0_U60Y" title="Hướng dẫn lấy iframe video youtube" target="_blank">Link video hướng dẫn thêm</a></p>
							</div>
						</div>
						<div class="row">
							<div class="col-md-12">
								<button type="button" class="btn btn-block btn-success BtnNewVideo">
									Thêm</button>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>

			<div class="row">
				<div class="col-md-12">
					<div class="card card-primary">
						<div class="card-header">
							<h3 class="card-title">
								Danh sách videos</h3>
							<div class="card-tools">
								<button type="button" class="btn btn-primary btn-sm" data-card-widget="collapse" data-toggle="tooltip" title="Collapse">
									<i class="fas fa-minus"></i>
								</button>
							</div>
						</div>
						<div class="card-body p-0">
							<asp:GridView ID="gv_vidList" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="sds_OpenBoxVideos" CssClass="table table-striped" BorderWidth="0px" AllowPaging="True" PageSize="5" PagerStyle-CssClass="paging">
								<HeaderStyle CssClass="data-table-vid" />
								<RowStyle CssClass="data-table-row content-row" />
								<Columns>
									<asp:TemplateField HeaderText="#">
										<ItemTemplate>
											<%# Eval("id").ToString() %>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Frame Video">
										<ItemTemplate>
											<%# Server.HtmlEncode(Eval("content").ToString()) %>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox TextMode="MultiLine" Rows="3" ID="tb_content" runat="server" CssClass="form-control form-control-sm" Text='<%# Bind("content") %>' />
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
</asp:Content>
