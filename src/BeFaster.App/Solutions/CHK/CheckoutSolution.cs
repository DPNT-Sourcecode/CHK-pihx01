using BeFaster.Runner.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeFaster.App.Solutions.CHK
{
    public class CheckoutSolution : ICheckout
    {
        private List<Product> _products;
        private List<SpecialOffer> _specialOffers;

        public CheckoutSolution()
        {
            _products = new List<Product>()
            {
                new Product{ SKU = "A", Price = 50},
                new Product{ SKU = "B", Price = 30},
                new Product{ SKU = "C", Price = 20},
                new Product{ SKU = "D", Price = 15}
            };

            _specialOffers = new List<SpecialOffer>()
            {
                new SpecialOffer{ SKU= "A", Quantity=3, Value = 20 },
                new SpecialOffer{ SKU= "B", Quantity=2, Value = 15 }
            };
        }

        public int ComputePrice(string skus)
        {
            if (string.IsNullOrEmpty(skus))
                throw new Exception("Error no SKU's set");

            if (skus.Length == 1)
            {
                return GetPriceForIndividualItem(skus);
            }

            return ScanMultipleItemsAndAppyDiscount(skus);
        }       

        private int GetPriceForIndividualItem(string sku)
        {
            var product = _products.Where(x => x.SKU == sku).FirstOrDefault();

            return product.Price;
        }

        private int ScanMultipleItemsAndAppyDiscount(string skus)
        {
            var matchingProducts = _products.Where(x => x.SKU == skus).ToList();

            return GetTotalCostWithDiscountsApplied(matchingProducts);
        }

        private int GetTotalCostWithDiscountsApplied(List<Product> products) 
        {
            var totalCost = 0;
            var totalDiscount = 0;

            totalCost = products.Sum(x=> GetPriceForIndividualItem(x.SKU));
            totalDiscount = _specialOffers.Sum(specialOffer => CalculateDiscountForItem(specialOffer, products));

            return totalCost - totalDiscount;
        }

        private int CalculateDiscountForItem(SpecialOffer specialOffer, List<Product> products)
        {
            var count = products.Count(x=> x.SKU == specialOffer.SKU);
            var discountForItem = (count / specialOffer.Quantity) * specialOffer.Value;

            return discountForItem;
        }
    }    
}



