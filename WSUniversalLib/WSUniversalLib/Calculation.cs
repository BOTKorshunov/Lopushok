using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSUniversalLib
{
    public class Calculation
    {
        public static int GetQuantityForProduct(int productType, int materialType, int count, float width, float length)
        {
            if (count <= 0 || width <= 0 || length <= 0)
                return -1;

            float coeficientTypeProduct;
            switch(productType)
            {
                case 1: coeficientTypeProduct = 1.1f; break;
                case 2: coeficientTypeProduct = 2.5f; break;
                case 3: coeficientTypeProduct = 8.43f; break;
                default: return -1;
            }

            float percentMaterial;
            switch (materialType)
            {
                case 1: percentMaterial = 0.3f / 100f; break;
                case 2: percentMaterial = 0.12f / 100f; break;
                default: return -1;
            }

            return (int)Math.Ceiling(width * length * count * coeficientTypeProduct / (1 - percentMaterial));
        }
    }
}
