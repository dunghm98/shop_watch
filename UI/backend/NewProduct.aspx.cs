using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTOs;
using BUSs;

namespace UI.backend
{
    public partial class WebForm8 : System.Web.UI.Page
    {
        Auth authUser = new Auth();
        BUS bus = new BUS();
        List<Category> lstCat = new List<Category>();
        public List<Brand> lstBrand = new List<Brand>();
        Helper helper = new Helper();

        protected void Page_Load(object sender, EventArgs e)
        {
            btnBack.Attributes["href"] = Request.UrlReferrer != null ? Request.UrlReferrer.ToString() : "/admin/manage/product";
            this.fetchCategories();
            this.fetchBrands();
            if (!IsPostBack) 
            { 
                
            }
            btnSave.ServerClick += new EventHandler(btnSave_ServerClick);
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
        protected void fetchBrands()
        {
            try
            {
                lstBrand= bus.fetchBrands();
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
                    bool isNum = double.TryParse(txtProductPrice.Text, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out _txtProductPrice);
                    if (isNum)
                    {
                        isNum = float.TryParse(txtSales.Text.Trim(), out _txtSales);
                        if (isNum)
                        {
                            Product product = new Product();
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
                            for (int i = 0; i < txtInCategories.Items.Count; i++) {
                                if (txtInCategories.Items[i].Selected) {
                                    product.Categories.Add(new Category(int.Parse(txtInCategories.Items[i].Value)));
                                }
                            }
                            product.Brand = new Brand(int.Parse(txtBrand.Text));
                            product.ShortDescription = txtProductShortDescription.Value;
                            product.Description = txtProductDescription.Value;
                            string imgPath = productImage.FileName != "" ? "/backend/assets/images/products/" + System.IO.Path.GetFileNameWithoutExtension(productImage.FileName) + "-" + DateTime.Now.ToString("ddMMyyyyhhmmss") + System.IO.Path.GetExtension(productImage.FileName) : "";
                            product.Image = imgPath;
                            if (bus.NewProduct(product))
                            {
                                // Nếu người dùng up ảnh thì up lên server
                                if (imgPath != "")
                                {
                                    //imgPath = AppDomain.CurrentDomain.BaseDirectory + "/backend/" + imgPath;
                                    imgPath = AppDomain.CurrentDomain.BaseDirectory + imgPath;
                                    productImage.PostedFile.SaveAs(imgPath);
                                }

                                Response.Redirect("/admin/manage/product");
                            }
                        }
                        throw new Exception("Sale hãy nhập số");
                    }
                    throw new Exception("Giá hãy nhập số");
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