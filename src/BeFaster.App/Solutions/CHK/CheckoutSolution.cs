using BeFaster.Runner.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeFaster.App.Solutions.CHK
{
    public class CheckoutSolution : ICheckout
    {
        private static List<Product> _products;

        public CheckoutSolution()
        {
            _products = new List<Product>()
            {
                new Product{ SKU = "A", Price = 50},
                new Product{ SKU = "B", Price = 30},
                new Product{ SKU = "C", Price = 20},
                new Product{ SKU = "D", Price = 15}
            };
        }

        public int ComputePrice(string skus)
        {
            if (string.IsNullOrEmpty(skus))
                throw new Exception("Error no SKU's set");

            if (skus.Length == 1)
            {
                return ScanSingleItem(skus);
            }

            return ScanMultipleItemsAndAppyDiscount();
        }       

        private static int ScanSingleItem(string skus)
        {
            var product = _products.Where(x => x.SKU == skus).FirstOrDefault();

            return product.Price;
        }

        private int ScanMultipleItemsAndAppyDiscount()
        {
            throw new NotImplementedException();
        }
    }    
}
