using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.classes
{
    class Products
    {
        public int id_product { get; set; }
        public string product_name { get; set; }
        public int price { get; set; }
        public Products() { }
        public Products(int id_product, string product_name, int price)
        {
            this.id_product = id_product;
            this.product_name = product_name;
            this.price = price;
        }
        public Products(string product_name, int price)
        {
            this.product_name = product_name;
            this.price = price;
        }
        public override string ToString()
        {
            return "id_product: " + id_product + "\nproduct_name: " + product_name + "\nprice: " + price + "\n";
        }
    }
}
