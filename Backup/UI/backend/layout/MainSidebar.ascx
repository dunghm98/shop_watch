<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MainSidebar.ascx.cs" Inherits="UI.backend.layout.MainSidebar" %>
<aside class="main-sidebar sidebar-dark-primary elevation-4">
	<!-- Brand Logo -->
	<a href="<%: Page.GetRouteUrl("Dashboard", null) %>" class="brand-link" style="height: 120px; padding-top: 0;">
		<img id="html_brandLogo" src="../assets/images/logo-shopwatch.png" alt="Shop Watch" class="brand-image img-circle" runat="server" style="width: 100%; clear: both; max-height: 150px; margin: 0 auto;" />
	</a>
	<!-- Sidebar -->
	<div class="sidebar">
		<!-- Sidebar user panel (optional) -->
		<div class="user-panel mt-3 pb-3 mb-3 d-flex">
			<ul class="nav nav-pills nav-sidebar flex-column" data-widget="treeview" role="menu" data-accordion="false" style="width: 100%">
				<li class="nav-item has-treeview" runat="server" id="liChangePass"><a href="#" class="nav-link">
					<img id="html_userIcon" src="../assets/images/user.png" class="img-circle elevation-2" alt="User Image" runat="server" />
					<p id="lb_userName" class="pl-2" runat="server">
					</p>
				</a>
					<ul class="nav nav-treeview">
						<li class="nav-item"><a href="#" class="nav-link" id="btnChangePass" runat="server"><i class="fas fa-key nav-icon"></i>
							<p>
								Đổi mật khẩu</p>
						</a></li>
						<li class="nav-item"><a href="javascript:void(0);" class="nav-link" runat="server" id="A2"><i class="nav-icon fas fa-sign-out-alt"></i>
							<asp:Button ID="btnLogout" runat="server" Text="Đăng xuất" OnClick="btnLogout_Click" UseSubmitBehavior="false"></asp:Button>
						</a></li>
					</ul>
				</li>
			</ul>
		</div>
		<!-- Sidebar Menu -->
		<nav class="mt-2">
			<ul class="nav nav-pills nav-sidebar flex-column" data-widget="treeview" role="menu" data-accordion="false">
				<!-- Add icons to the links using the .nav-icon class
               with font-awesome or any other icon font library -->
				<li class="nav-item"><a href="#" class="nav-link" runat="server" id="dashboardCss"><i class="nav-icon fas fa-tachometer-alt"></i>
					<p>
						Dashboard
					</p>
				</a></li>
				<!-- Shop info setting -->
				<li class="nav-item"><a href="#" class="nav-link" runat="server" id="shopInfo_htmlID"><i class="nav-icon fas fa-list-ul"></i>
					<p>
						Thông tin về shop
					</p>
				</a></li>
				<li class="nav-item has-treeview" runat="server" id="danhMucTinTuc_htmlID"><a href="#" class="nav-link"><i class="nav-icon fas fa-copy"></i>
					<p>
						Danh mục và tin tức <i class="fas fa-angle-left right"></i>
					</p>
				</a>
					<ul class="nav nav-treeview">
						<li class="nav-item"><a href="#" class="nav-link" runat="server" id="openBox_htmlID"><i class="far fa-circle nav-icon"></i>
							<p>
								Video đập hộp</p>
						</a></li>
						<li class="nav-item"><a href="#" class="nav-link" id="category_htmlID" runat="server"><i class="far fa-circle nav-icon"></i>
							<p>
								Danh mục</p>
						</a></li>
						<li class="nav-item"><a href="#" class="nav-link"><i class="far fa-circle nav-icon"></i>
							<p>
								Sub menu</p>
						</a></li>
						<li class="nav-item"><a href="#" class="nav-link"><i class="far fa-circle nav-icon"></i>
							<p>
								Submenu</p>
						</a></li>
					</ul>
				</li>
				<li class="nav-item has-treeview" runat="server" id="productBrand_htmlID"><a href="#" class="nav-link"><i class="nav-icon fas fa-chart-pie"></i>
					<p>
						Sản phẩm & Nhãn hiệu <i class="right fas fa-angle-left"></i>
					</p>
				</a>
					<ul class="nav nav-treeview">
						<li class="nav-item"><a href="#" class="nav-link" runat="server" id="products_htmlID"><i class="far fa-circle nav-icon"></i>
							<p>
								Sản phẩm</p>
						</a></li>
						<li class="nav-item"><a href="#" class="nav-link" runat="server" id="brands_htmlID"><i class="far fa-circle nav-icon"></i>
							<p>
								Nhãn hiệu</p>
						</a></li>
					</ul>
				</li>
			</ul>
		</nav>
		<!-- /.sidebar-menu -->
	</div>
	<!-- /.sidebar -->
</aside>
