using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTOs
{
    public class Product
    {
        public int Id { get; set; }
        public bool IsEnable { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public string Origin { get; set; }
        public string MachineType { get; set; }
        public int ForGender { get; set; }
        public string Size { get; set; }
        public string Height { get; set; }
        public string ShellMaterial { get; set; }
        public string ChainMaterial { get; set; }
        public string GlassesMaterial { get; set; }
        public string Functions { get; set; }
        public string WaterResistLv { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public int IsOutOfStock { get; set; }
        public float Sale { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public Brand Brand { get; set; }

        private List<Category> categories;

        public List<Category> Categories
        {
            get { return categories; }
            set { categories = value; }
        }

        public Product() {
            this.Brand = new Brand();
            this.Categories = new List<Category>();
        }

    }
}
