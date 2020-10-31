using BeFaster.Runner.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TDL.Client.Queue.Abstractions.Response;

namespace BeFaster.App.Solutions.CHK
{
    public class CheckoutSolution : ICheckout
    {
        private readonly List<Product> _products;
        private readonly List<SpecialOffer> _specialOffers;

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
                return 0;

            var regex = new Regex(@"^[a-zA-Z0-9\s,]*$");
            if (!regex.IsMatch(skus))
                return 0;            

            if (skus.Length == 1)            
                return GetPriceForIndividualItem(skus.ToLower());
            
            return ScanMultipleItemsAndAppyDiscount(skus.ToLower());
        }       

        private int GetPriceForIndividualItem(string sku)
        {
            var product = _products.Where(x => x.SKU.ToLower() == sku).FirstOrDefault();

            if (product == null)
                return 0;

            return product.Price;
        }

        private int ScanMultipleItemsAndAppyDiscount(string skus)
        {
            var splitItems = skus.ToLower().ToCharArray();
            var matchingProducts = new List<Product>();

            foreach (var item in splitItems)
            {
                var matchingProduct = _products.Where(x => x.SKU.ToLower() == item.ToString()).FirstOrDefault();
                
                if (matchingProduct != null)
                {
                    matchingProducts.Add(matchingProduct);
                }
            }           

            return GetTotalCostWithDiscountsApplied(matchingProducts);
        }

        private int GetTotalCostWithDiscountsApplied(List<Product> products) 
        {
            var totalCost = 0;
            var totalDiscount = 0;

            totalCost = products.Sum(x=> GetPriceForIndividualItem(x.SKU.ToLower()));
            totalDiscount = _specialOffers.Sum(specialOffer => CalculateDiscountForItem(specialOffer, products));

            return totalCost - totalDiscount;
        }

        private int CalculateDiscountForItem(SpecialOffer specialOffer, List<Product> products)
        {
            var count = products.Count(x=> x.SKU.ToLower() == specialOffer.SKU.ToLower());
            var discountForItem = (count / specialOffer.Quantity) * specialOffer.Value;

            return discountForItem;
        }
    }    
}



