<%@ Page Title="" Language="C#" MasterPageFile="~/frontend/App.Master" AutoEventWireup="true" CodeBehind="homepage.aspx.cs" Inherits="UI.frontend.WebForm1" %>
<asp:Content ID="homepage" runat="server" ContentPlaceHolderID="main_ct">
            <!-- start home slider -->
        <!-- end home slider -->
		<!-- unit banner area start -->
		<div class="unit-banner-area">
			<div class="container">
				<div class="row bn-un">
					<div class="col-md-4 col-sm-4 col-xs-6">
						<!-- single banner start -->
						<div class="single-banner">
							<a href="#"><img src="../img/anh-danh-muc.png" alt="" /></a>
						</div>
						<!-- single banner end -->
					</div>
					<div class="col-md-4 col-sm-4 col-xs-6">
						<!-- single banner start -->
						<div class="single-banner">
							<a href="#"><img src="../img/anh-danh-muc.png" alt="" /></a>
						</div>
						<!-- single banner end -->
					</div>
					<div class="col-md-4 col-sm-4 hidden-xs">
						<!-- single banner start -->
						<div class="single-banner">
							<a href="#"><img src="../img/anh-danh-muc.png" alt="" /></a>
						</div>
						<!-- single banner end -->
					</div>
				</div>
			</div>
		</div>
		<!-- unit banner area end -->
		<!-- product section start -->
		<!-- product section end -->
		<!-- perfect service area start -->
		<!-- perfect service area end -->
			<!-- product section start -->
		<div class="our-product-area new-product">
				<div class="container">
					<div class="area-title">
						<h2>ĐỒNG HỒ CASIO</h2>
					</div>
					<!-- our-product area start -->
                    
                    <div class="row">
						<div class="col-md-12">
							<div class="row">
								<div class="features-curosel">
                                    <% foreach (DTOs.Product product in lstCasioProducts)
                                        { %>
                                    <div class="col-lg-12 col-md-12">
										<div class="single-product first-sale">
											<span class="sale-text">Sale</span>
											<div class="product-img">
												<a href='/product/<%= product.Id%>'>
													<img class="primary-image" src='<%=product.Image%>' alt="" />
												</a>
												<div class="actions">
													<div class="action-buttons">
														<div class="add-to-links">
															<div class="add-to-wishlist">
																<a href="#" title="Add to Wishlist"><i class="fa fa-heart"></i></a>
															</div>
															<div class="compare-button">
																<a href="#" title="Add to Cart"><i class="icon-bag"></i></a>
															</div>									
														</div>
														<div class="quickviewbtn">
															<a href="#" title="Add to Compare"><i class="fa fa-retweet"></i></a>
														</div>
													</div>
												</div>
												<div class="price-box">
													<span class="new-price"><%=product.Price %></span>
												</div>
											</div>
											<div class="product-content">
												<h2 class="product-name"><a href="#"><%=product.Name%></a></h2>
												<p><%=product.ShortDescription %></p>
											</div>
										</div>
									</div>
                                    <%} %>
                                </div>
							</div>	
						</div>
					</div>
					<!-- our-product area end -->	
				</div>
			</div>
			<!-- product section end -->
			<!-- product section start -->
			<!-- product section start -->
		<div class="our-product-area new-product">
				<div class="container">
					<div class="area-title">
						<h2>ĐỒNG HỒ SEIKO</h2>
					</div>
					<!-- our-product area start -->
                   
                    <div class="row">
						<div class="col-md-12">
							<div class="row">
								<div class="features-curosel">
                                    <% foreach (DTOs.Product product in lstSeikoProducts)
                                        { %>
                                    <div class="col-lg-12 col-md-12">
										<div class="single-product first-sale">
											<span class="sale-text">Sale</span>
											<div class="product-img">
												<a href='/product/<%= product.Id%>'>
													<img class="primary-image" src='<%=product.Image%>' alt="" />
												</a>
												<div class="actions">
													<div class="action-buttons">
														<div class="add-to-links">
															<div class="add-to-wishlist">
																<a href="#" title="Add to Wishlist"><i class="fa fa-heart"></i></a>
															</div>
															<div class="compare-button">
																<a href="#" title="Add to Cart"><i class="icon-bag"></i></a>
															</div>									
														</div>
														<div class="quickviewbtn">
															<a href="#" title="Add to Compare"><i class="fa fa-retweet"></i></a>
														</div>
													</div>
												</div>
												<div class="price-box">
													<span class="new-price"><%=product.Price %></span>
												</div>
											</div>
											<div class="product-content">
												<h2 class="product-name"><a href="#"><%=product.Name%></a></h2>
												<p><%=product.ShortDescription %></p>
											</div>
										</div>
									</div>
                                    <%} %>
                                </div>
							</div>	
						</div>
					</div>
					<!-- our-product area end -->	
				</div>
			</div>
			<!-- product section end -->
    <!-- product section start -->
		<div class="our-product-area new-product">
				<div class="container">
					<div class="area-title">
						<h2>ĐỒNG HỒ OP</h2>
					</div>
					<!-- our-product area start -->
                   
                    <div class="row">
						<div class="col-md-12">
							<div class="row">
								<div class="features-curosel">
                                    <% foreach (DTOs.Product product in lstOpProducts)
                                        { %>
                                    <div class="col-lg-12 col-md-12">
										<div class="single-product first-sale">
											<span class="sale-text">Sale</span>
											<div class="product-img">
												<a href='/product/<%= product.Id%>'>
													<img class="primary-image" src='<%=product.Image%>' alt="" />
												</a>
												<div class="actions">
													<div class="action-buttons">
														<div class="add-to-links">
															<div class="add-to-wishlist">
																<a href="#" title="Add to Wishlist"><i class="fa fa-heart"></i></a>
															</div>
															<div class="compare-button">
																<a href="#" title="Add to Cart"><i class="icon-bag"></i></a>
															</div>									
														</div>
														<div class="quickviewbtn">
															<a href="#" title="Add to Compare"><i class="fa fa-retweet"></i></a>
														</div>
													</div>
												</div>
												<div class="price-box">
													<span class="new-price"><%=product.Price %></span>
												</div>
											</div>
											<div class="product-content">
												<h2 class="product-name"><a href="#"><%=product.Name%></a></h2>
												<p><%=product.ShortDescription %></p>
											</div>
										</div>
									</div>
                                    <%} %>
                                </div>
							</div>	
						</div>
					</div>
					<!-- our-product area end -->	
				</div>
			</div>
			<!-- product section end -->
    <!-- product section start -->
		<div class="our-product-area new-product">
				<div class="container">
					<div class="area-title">
						<h2>ĐỒNG HỒ CITIZEN</h2>
					</div>
					<!-- our-product area start -->
                   
                    <div class="row">
						<div class="col-md-12">
							<div class="row">
								<div class="features-curosel">
                                    <% foreach (DTOs.Product product in lstCitizenProducts)
                                        { %>
                                    <div class="col-lg-12 col-md-12">
										<div class="single-product first-sale">
											<span class="sale-text">Sale</span>
											<div class="product-img">
												<a href='/product/<%= product.Id%>'>
													<img class="primary-image" src='<%=product.Image%>' alt="" />
												</a>
												<div class="actions">
													<div class="action-buttons">
														<div class="add-to-links">
															<div class="add-to-wishlist">
																<a href="#" title="Add to Wishlist"><i class="fa fa-heart"></i></a>
															</div>
															<div class="compare-button">
																<a href="#" title="Add to Cart"><i class="icon-bag"></i></a>
															</div>									
														</div>
														<div class="quickviewbtn">
															<a href="#" title="Add to Compare"><i class="fa fa-retweet"></i></a>
														</div>
													</div>
												</div>
												<div class="price-box">
													<span class="new-price"><%=product.Price %></span>
												</div>
											</div>
											<div class="product-content">
												<h2 class="product-name"><a href="#"><%=product.Name%></a></h2>
												<p><%=product.ShortDescription %></p>
											</div>
										</div>
									</div>
                                    <%} %>
                                </div>
							</div>	
						</div>
					</div>
					<!-- our-product area end -->	
				</div>
			</div>
			<!-- product section end -->
    <!-- product section start -->
		<div class="our-product-area new-product">
				<div class="container">
					<div class="area-title">
						<h2>ĐỒNG HỒ ORIENT</h2>
					</div>
					<!-- our-product area start -->
                   
                    <div class="row">
						<div class="col-md-12">
							<div class="row">
								<div class="features-curosel">
                                    <% foreach (DTOs.Product product in lstOrientProducts)
                                        { %>
                                    <div class="col-lg-12 col-md-12">
										<div class="single-product first-sale">
											<span class="sale-text">Sale</span>
											<div class="product-img">
												<a href='/product/<%= product.Id%>'>
													<img class="primary-image" src='<%=product.Image%>' alt="" />
												</a>
												<div class="actions">
													<div class="action-buttons">
														<div class="add-to-links">
															<div class="add-to-wishlist">
																<a href="#" title="Add to Wishlist"><i class="fa fa-heart"></i></a>
															</div>
															<div class="compare-button">
																<a href="#" title="Add to Cart"><i class="icon-bag"></i></a>
															</div>									
														</div>
														<div class="quickviewbtn">
															<a href="#" title="Add to Compare"><i class="fa fa-retweet"></i></a>
														</div>
													</div>
												</div>
												<div class="price-box">
													<span class="new-price"><%=product.Price %></span>
												</div>
											</div>
											<div class="product-content">
												<h2 class="product-name"><a href="#"><%=product.Name%></a></h2>
												<p><%=product.ShortDescription %></p>
											</div>
										</div>
									</div>
                                    <%} %>
                                </div>
							</div>	
						</div>
					</div>
					<!-- our-product area end -->	
				</div>
			</div>
			<!-- product section end -->
		<!-- testimonial area start -->
		<!-- testimonial area end -->
		<!-- latestpost area start -->
		<div class="latest-post-area">
			<div class="container">
				<div class="area-title">
					<h2>Kiến Thức Đồng Hồ</h2>
				</div>
				<div class="row">
					<div class="all-singlepost">
						<!-- single latestpost start -->
						<div class="col-md-4 col-sm-4 col-xs-12">
							<div class="single-post">
								<div class="post-thumb">
									<a href="#">
										<img src="img/post/post-1.jpg" alt="" />
									</a>
								</div>
								<div class="post-thumb-info">
									<div class="post-time">
										<a href="#">Beauty</a>
										<span>/</span>
										<span>Post by</span>
										<span>BootExperts</span>
									</div>
									<div class="postexcerpt">
										<p>Mirum est notare quam littera gothica, quam nunc putamus parum claram, anteposuerit litterarum formas...</p>
										<a href="#" class="read-more">Read more</a>
									</div>
								</div>
							</div>
						</div>
						<!-- single latestpost end -->
						<!-- single latestpost start -->
						<div class="col-md-4 col-sm-4 col-xs-12">
							<div class="single-post">
								<div class="post-thumb">
									<a href="#">
										<img src="img/post/post-2.jpg" alt="" />
									</a>
								</div>
								<div class="post-thumb-info">
									<div class="post-time">
										<a href="#">Fashion</a>
										<span>/</span>
										<span>Post by</span>
										<span>BootExperts</span>
									</div>
									<div class="postexcerpt">
										<p>Fusce ac odio odio. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus...</p>
										<a href="#" class="read-more">Read more</a>
									</div>
								</div>
							</div>
						</div>
						<!-- single latestpost end -->
						<!-- single latestpost start -->
						<div class="col-md-4 col-sm-4 col-xs-12">
							<div class="single-post">
								<div class="post-thumb">
									<a href="#">
										<img src="img/post/post-3.jpg" alt="" />
									</a>
								</div>
								<div class="post-thumb-info">
									<div class="post-time">
										<a href="#">Brunch Network</a>
										<span>/</span>
										<span>Post by</span>
										<span>BootExperts</span>
									</div>
									<div class="postexcerpt">
										<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt...</p>
										<a href="#" class="read-more">Read more</a>
									</div>
								</div>
							</div>
						</div>
						<!-- single latestpost end -->
					</div>
				</div>
			</div>
		</div>
		<!-- latestpost area end -->
</asp:Content>