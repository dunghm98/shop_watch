using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BUSs;
using DTOs;
using System.Globalization;

namespace UI.backend
{
    public partial class WebForm9 : System.Web.UI.Page
    {
        Auth authUser = new Auth();
        BUS bus = new BUS();
        List<Category> lstCat = new List<Category>();
        public List<Brand> lstBrand = new List<Brand>();
        Helper helper = new Helper();
        public Product product = new Product();
        int productId = -1;

        protected void Page_Load(object sender, EventArgs e)
        {
            authUser.checkAuth();
            btnBack.Attributes["href"] = "/admin/manage/product";
            btnSave.ServerClick += new EventHandler(btnSave_ServerClick);
            #region FetchProductList
            bool isProductId = int.TryParse(RouteData.Values["Product"].ToString(), out productId);
            if (isProductId)
            {
                product = bus.GetProduct(productId);
            }
            else
            {
                this.helper.errorAlert(this, "404");
            }
            if (!IsPostBack)
            {
                this.fetchBrands();
                this.fetchCategories();
                chkboxEnable.Checked = product.IsEnable;
                txtProductName.Text = product.Name;
                txtProductPrice.Text = Convert.ToString(product.Price, CultureInfo.InvariantCulture); // use CultureInfo.InvariantCulture instead of "," when converting double to string
                txtSales.Text = product.Sale.ToString();
                txtStock.Text = product.IsOutOfStock.ToString();
                txtProductOrigin.Text = product.Origin;
                txtProductMachineType.Text = product.MachineType;
                txtForGender.Text = product.ForGender.ToString();
                txtSize.Text = product.Size;
                txtHeight.Text = product.Height;
                txtShellMaterial.Text = product.ShellMaterial;
                txtChainMaterial.Text = product.ChainMaterial;
                txtGlassesMaterial.Text = product.GlassesMaterial;
                txtFunctions.Text = product.Functions;
                txtWaterResist.Text = product.WaterResistLv;
                txtBrand.Text = product.Brand.Id.ToString();
                txtProductShortDescription.Value = product.ShortDescription;
                txtProductDescription.Value = product.Description;
                // set selected categories
                for (int i = 0; i < txtInCategories.Items.Count; i++)
                {
                    foreach (Category cat in product.Categories)
                    {
                        if (int.Parse(txtInCategories.Items[i].Value) == cat.Id)
                        {
                            txtInCategories.Items[i].Selected = true;
                        }
                    }
                }
            }
            #endregion
        }

        protected void fetchBrands()
        {
            try
            {
                lstBrand = bus.fetchBrands();
            }
            catch (Exception e)
            {
                this.helper.errorAlert(this, e.Message);
            }
        }
        protected void fetchCategories()
        {
            try
            {
                lstCat = bus.fetchLstCategory(-1, "");
                foreach (Category cat in lstCat)
                {
                    ListItem item = new ListItem(cat.Name, cat.Id.ToString());
                    txtInCategories.Items.Add(item);
                }
            }
            catch (Exception e)
            {
                this.helper.errorAlert(this, e.Message);
            }
        }
        protected void btnSave_ServerClick(object sender, EventArgs args)
        {
            try
            {
                if (txtProductName.Text.Trim() != "" && txtProductPrice.Text.Trim() != "")
                {
                    double _txtProductPrice;
                    float _txtSales;
                    txtProductPrice.Text = txtProductPrice.Text.Replace(",", ".");
                    bool isNum = double.TryParse(txtProductPrice.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out _txtProductPrice);
                    if (isNum)
                    {
                        isNum = float.TryParse(txtSales.Text.Trim(), out _txtSales);
                        if (isNum)
                        {
                            Product product = new Product();
                            product.Id = productId;
                            product.IsEnable = chkboxEnable.Checked;
                            product.Name = txtProductName.Text.Trim();
                            product.Price = _txtProductPrice;
                            product.Sale = _txtSales;
                            product.IsOutOfStock = int.Parse(txtStock.Text);
                            product.Origin = txtProductOrigin.Text.Trim();
                            product.MachineType = txtProductMachineType.Text.Trim();
                            product.ForGender = int.Parse(txtForGender.Text);
                            product.Size = txtSize.Text.Trim();
                            product.Height = txtHeight.Text.Trim();
                            product.ShellMaterial = txtShellMaterial.Text.Trim();
                            product.ChainMaterial = txtChainMaterial.Text.Trim();
                            product.GlassesMaterial = txtGlassesMaterial.Text.Trim();
                            product.Functions = txtFunctions.Text.Trim();
                            product.WaterResistLv = txtWaterResist.Text.Trim();
                            for (int i = 0; i < txtInCategories.Items.Count; i++)
                            {
                                if (txtInCategories.Items[i].Selected)
                                {
                                    product.Categories.Add(new Category(int.Parse(txtInCategories.Items[i].Value)));
                                }
                            }
                            product.Brand = new Brand(int.Parse(txtBrand.Text));
                            product.ShortDescription = txtProductShortDescription.Value;
                            product.Description = txtProductDescription.Value;
                            string imgPath = productImage.FileName != "" ? "/backend/assets/images/products/" + System.IO.Path.GetFileNameWithoutExtension(productImage.FileName) + "-" + DateTime.Now.ToString("ddMMyyyyhhmmss") + System.IO.Path.GetExtension(productImage.FileName) : "";
                            product.Image = imgPath;
                            if (bus.UpdateProduct(product))
                            {
                                // Nếu người dùng up ảnh thì up lên server
                                if (imgPath != "")
                                {
                                    imgPath = AppDomain.CurrentDomain.BaseDirectory + imgPath;
                                    productImage.PostedFile.SaveAs(imgPath);
                                }

                                Response.Redirect("/admin/manage/product");
                            }
                        }
                        throw new Exception("Sale hãy nhập số");
                    }
                    throw new Exception("Gi hãy nhập số");
                }
                else
                {
                    throw new Exception("Không được để trống tên sản phẩm và giá");
                }
            }
            catch (Exception e)
            {
                this.helper.errorAlert(this, e.Message);
            }
        }
    }
}