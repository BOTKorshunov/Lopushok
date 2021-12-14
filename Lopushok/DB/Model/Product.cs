using Lopushok.DB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lopushok.DB
{
    partial class Product
    {
        public string CorrectImage
        {
            get
            {
                if (String.IsNullOrEmpty(Image) || String.IsNullOrWhiteSpace(Image))
                    return "/products/picture.png";
                else
                    return Image;
            }
        }

        public string RequiredMaterails
        {
            get
            {
                string materials = null;

                if (ProductMaterial.Count == 0)
                    materials += "Нет";

                foreach(var productMaterial in ProductMaterial)
                {
                    materials += productMaterial.Material.Title;

                    if (productMaterial != ProductMaterial.Last())
                    {
                        materials += ", ";
                    }
                }

                return materials;
            }
        }

        public decimal ProductPrice
        {
            get
            {
                decimal price = 0;
                foreach(var productMaterial in ProductMaterial)
                {
                    price += productMaterial.Material.Cost;
                }

                return price;
            }
        }
    }
}
