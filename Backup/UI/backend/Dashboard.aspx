<%@ Page Title="" Language="C#" MasterPageFile="~/backend/App.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="UI.backend.WebForm1" %>

<%@ MasterType VirtualPath="~/backend/App.Master" %>
<asp:Content ID="content_headTitle" ContentPlaceHolderID="head_title" runat="server">
	Dashboard
</asp:Content>
<asp:Content ID="content_headScript" ContentPlaceHolderID="head_script" runat="server">
</asp:Content>
<asp:Content ID="conent_mainContent" ContentPlaceHolderID="main_content" runat="server">
	<section class="content-header">
		<div class="container-fluid">
			<div class="row mb-2">
				<div class="col-sm-6">
					<h1>Dashboard</h1>
				</div>
				<div class="col-sm-6">
					<ol class="breadcrumb float-sm-right">
						<li class="breadcrumb-item"><a href="<%:Page.GetRouteUrl("Dashboard", null) %>">Home</a></li>
						<li class="breadcrumb-item">Dashboard</li>
					</ol>
				</div>
			</div>
		</div>
		<!-- /.container-fluid -->
	</section>
	<section class="content">
		<div class="container-fluid">
			<div class="row">
				<div class="col-lg-3 col-md-3 col-sm-12">
					<div class="small-box bg-info">
						<div class="inner">
							<h3><%=productTotal %></h3>
							<p>
								Sản phẩm trong kho</p>
						</div>
						<div class="icon">
							<i class="icon ion-md-card"></i>
						</div>
						<a href="/admin/manage/product" class="small-box-footer">Quản lý <i class="fas fa-arrow-circle-right"></i></a>
					</div>
				</div>
			</div>
		</div>
	</section>
</asp:Content>
